using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Globalization;

namespace DevAchievements.Infrastructure.Web
{
	/// <summary>
	/// Classe base para controllers.
	/// </summary>
	/// <remarks>
	/// Convenções:
	/// 1. Deverá existir duas views
	///        1.1. Index.cshtml, onde será renderiza a grid de pesquisa.
	///        1.2. CreateEdit.cshtml, onde será renderizda o cadastro.
	///    2. As actions implementadas automaticamente são: Index, Create, Edit, Remove e Search.    
	/// </remarks>
	/// <typeparam name="TEntity">O entity que a controller trabalha.</typeparam>
	/// <typeparam name="TId">O tipo de id que o entity usa.</typeparam>
	public abstract class ControllerBase<TEntity, TId> : Controller
	{

		#region Properties

		/// <summary>
		/// Obtém um valor que indica se action corrente está com erro.
		/// </summary>
		protected bool HasError { get; private set; }

		#endregion

		#region Actions

		/// <summary>
		/// Action de entrada para a listagem.
		/// </summary>
		/// <returns>O resultado da ação.</returns>
		[HttpGet]
		public virtual ActionResult Index ()
		{
			return View (CreateNewEntity ());
		}

		/// <summary>
		/// Action de entrada para a criação.
		/// </summary>
		/// <returns>O resultado da ação.</returns>
		[HttpGet]
		public virtual ActionResult Create ()
		{
			return View ("CreateEdit", CreateNewEntity ());
		}

		/// <summary>
		/// Action para receber o post de criação.
		/// </summary>
		/// <param name="entity">A entity a ser criada.</param>
		/// <returns>O resultado da ação.</returns>
		[HttpPost]
		public virtual ActionResult Create (TEntity entity)
		{
			return SaveEntityAction (entity);
		}

		/// <summary>
		/// Action de entrada para a edição.
		/// </summary>
		/// <param name="id">O id da entity a ser editada.</param>
		/// <returns>O resultado da ação.</returns>
		[HttpGet]
		public virtual ActionResult Edit (TId id)
		{
			return View ("CreateEdit", GetEntityById (id));
		}

		/// <summary>
		/// Action para receber o post de edição.
		/// </summary>
		/// <param name="entity">A entity a ser atualizada.</param>        
		/// <returns>O resultado da ação.</returns>
		[HttpPost]
		public virtual ActionResult Edit (TEntity entity)
		{
			return SaveEntityAction (entity);
		}

		/// <summary>
		/// Action para exclusão de uma entity.
		/// </summary>
		/// <param name="id">O id da entity a ser excluída.</param>
		[HttpGet]
		public virtual ActionResult Remove (TId id)
		{
			return this.Call (() => {                
				DeleteEntityById (id);
				DependencyService.Create<IUnitOfWork> ().Commit ();
				
				return Json (new { deleted = true }, JsonRequestBehavior.AllowGet);
			}, 
			(ex) => {
					return Json (new { deleted = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
			});
		}

		/// <summary>
		/// Action de pesquisa utilizada pelo componente Grid (JS).
		/// </summary>
		/// <returns>O resultado da ação.</returns>        
		public virtual JsonResult Search ()
		{
			return new GridResult<TEntity, TId> (GetEntities (), GetEntityId, GetGridValues);
		}

		#endregion

		#region Abstract methods

		/// <summary>
		/// Cria uma nova instância da entidade.
		/// </summary>
		/// <returns>A nova instância da entity.</returns>
		protected abstract TEntity CreateNewEntity ();

		/// <summary>
		/// Obtém o valor do id da entity informada.
		/// </summary>
		/// <param name="entity">A entity.</param>
		/// <returns>O valor do id.</returns>
		protected abstract TId GetEntityId (TEntity entity);

		/// <summary>
		/// Obtém uma entity pelo id.
		/// </summary>
		/// <param name="id">O id da entity desejada.</param>
		/// <returns>A entity se existir, no contrário nulo.</returns>
		protected abstract TEntity GetEntityById (TId id);

		/// <summary>
		/// Exclui a entity com o id informado.
		/// </summary>
		/// <param name="id">O id da entity a ser excluido.</param>
		protected abstract void DeleteEntityById (TId id);

		/// <summary>
		/// Salva a entity informada.
		/// </summary>
		/// <param name="entity">A entity a ser salva.</param>
		/// <returns>A entity com o novo id, no caso de criação.</returns>
		protected abstract TEntity SaveEntity (TEntity entity);

		/// <summary>
		/// Obtém todas as instâncias da entity.
		/// </summary>
		/// <returns>A lista de entity.</returns>
		[SuppressMessage ("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected abstract IEnumerable<TEntity> GetEntities ();

		/// <summary>
		/// Obtém os valores da entity que devem ser utilizados na grid
		/// </summary>
		/// <param name="entity">A entity.</param>
		/// <returns>Os valores a serem utilizados.</returns>
		protected abstract IEnumerable<object> GetGridValues (TEntity entity);

		#endregion

		#region Private methods

		/// <summary>
		/// Action padrão para save.
		/// </summary>
		/// <param name="entity">A entity a ser salva.</param>
		/// <returns>O resultado da ação.</returns>
		private ActionResult SaveEntityAction (TEntity entity)
		{
			return this.Call (
				() => {
					var savedEntity = SaveEntity (entity);
					DependencyService.Create<IUnitOfWork> ().Commit ();
					this.SetSuccessMessage ("RecordSaved".Translate ());
					return RedirectToAction ("Edit", new { id = GetEntityId (savedEntity) });
				},
				(ex) => {
					return View ("CreateEdit", entity);
				});            
		}

		private void RegisterError (Exception ex)
		{
			HasError = true;
			this.SetErrorMessage (ex);
		}

		#endregion

	}
}