using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DevAchievements.Infrastructure.Web
{
    /// <summary>
    /// Implementação de ControllerBase utilizando Funcs para uso mais fluente.
    /// </summary>
    /// <typeparam name="TEntity">O entity que a controller trabalha.</typeparam>
    /// <typeparam name="TId">O tipo de id que o entity usa.</typeparam>
    public abstract class FuncControllerBase<TEntity, TId> : ControllerBase<TEntity, TId>
    {
        #region Properties
        /// <summary>
        /// Obtém ou define como criar uma nova instância da entity.
        /// </summary>
        protected Func<TEntity> CreateNewEntityFunc { get; set; }

        /// <summary>
        /// Obtém ou define como consultar o valor do id da entity informada.
        /// </summary>
        protected Func<TEntity, TId> GetEntityIdFunc { get; set; }

        /// <summary>
        /// Obtém ou define como consultar uma entity pelo id.
        /// </summary>
        protected Func<TId, TEntity> GetEntityByIdFunc { get; set; }

        /// <summary>
        /// Obtém ou define como excluir a entity com o id informado.
        /// </summary>
        protected Action<TId> DeleteEntityByIdFunc { get; set; }

        /// <summary>
        /// Obtém ou define como salvar a entity informada.
        /// </summary>
        protected Func<TEntity, TEntity> SaveEntityFunc { get; set; }

        /// <summary>
        /// Obtém ou define como consultar todas as instâncias da entity.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected Func<IEnumerable<TEntity>> GetEntitiesFunc { get; set; }

        /// <summary>
        /// Obtém ou define como quais são os valores da entity que devem ser utilizados na grid.
        /// </summary>        
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected Func<TEntity, IEnumerable<object>> GetGridValuesFunc { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Cria uma nova instância da entity.
        /// </summary>
        /// <returns>A nova instância da entity.</returns>
        protected override TEntity CreateNewEntity()
        {
            return CreateNewEntityFunc();
        }

        /// <summary>
        /// Obtém o valor do id da entity informada.
        /// </summary>
        /// <param name="entity">A entity.</param>
        /// <returns>O valor do id.</returns>
        protected override TId GetEntityId(TEntity entity)
        {
            return GetEntityIdFunc(entity);
        }

        /// <summary>
        /// Obtém uma entity pelo id.
        /// </summary>
        /// <param name="id">O id da entity desejada.</param>
        /// <returns>A entity se existir, no contrário nulo.</returns>
        protected override TEntity GetEntityById(TId id)
        {
            return GetEntityByIdFunc(id);
        }

        /// <summary>
        /// Exclui a entity com o id informado.
        /// </summary>
        /// <param name="id">O id da entity a ser excluido.</param>
        protected override void DeleteEntityById(TId id)
        {
            DeleteEntityByIdFunc(id);
        }

        /// <summary>
        /// Salva a entity informada.
        /// </summary>
        /// <param name="entity">A entity a ser salva.</param>
        /// <returns>A entity com o novo id, no caso de criação.</returns>
        protected override TEntity SaveEntity(TEntity entity)
        {
            return SaveEntityFunc(entity);
        }

        /// <summary>
        /// Obtém todas as instâncias da entity.
        /// </summary>
        /// <returns>A lista de entity.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected override IEnumerable<TEntity> GetEntities()
        {
            return GetEntitiesFunc();
        }

        /// <summary>
        /// Obtém os valores da entity que devem ser utilizados na grid
        /// </summary>
        /// <param name="entity">A entity.</param>
        /// <returns>Os valores a serem utilizados.</returns>
        protected sealed override IEnumerable<object> GetGridValues(TEntity entity)
        {
            return GetGridValuesFunc(entity);
        }
        #endregion
    }
}