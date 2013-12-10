using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Logging;

namespace DevAchievements.Infrastructure.Web
{
    /// <summary>
    /// Extensions methods para controllers.
    /// </summary>
    public static class ControllerExtensions
    {
        #region Methods
        /// <summary>
        /// Define a mensagem de erro da ação corrente.
        /// </summary>
        /// <param name="controller">A controller.</param>
        /// <param name="exception">A exception.</param>
        public static void SetErrorMessage(this ControllerBase controller, Exception exception)
        {
            controller.TempData["Error"] = exception.Message;
			LogService.Error("{0}{3}{1}{3}{2}", exception.Message, exception.StackTrace, exception.GetBaseException().Message, Environment.NewLine);
        }

        /// <summary>
        /// Define a mensagem de erro da ação corrente.
        /// </summary>
        /// <param name="controller">A controller.</param>
        /// <param name="message">A mensagem de erro.</param>
        public static void SetErrorMessage(this ControllerBase controller, string message)
        {
            controller.TempData["Error"] = message;
        }

        /// <summary>
        /// Define a mensagem de sucesso da ação corrente.
        /// </summary>
        /// <param name="controller">A controller.</param>
        /// <param name="message">A mensagem de sucesso.</param>
        public static void SetSuccessMessage(this ControllerBase controller, string message)
        {
            controller.TempData["Success"] = message;
        }

        /// <summary>
        /// Realiza a chamada a ação informada e caso ocorra alguma exceção faz o tratamento padrão de erros para controllers.
        /// </summary>
        /// <param name="controller">A controller alvo.</param>
        /// <param name="action">A ação a ser executada.</param>        
        /// <param name="errorCallback">A action que será chamada caso ocorra um erro na execução de action.</param>
        /// <returns>A ActionResult resultante de action ou errorCallback.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "O método é exatamente para garantir que qualquer exceção seja tratada adequada para ser apresentada ao usuário.")]
        public static ActionResult Call(this ControllerBase controller, Func<ActionResult> action, Func<Exception, ActionResult> errorCallback = null)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                SetErrorMessage(controller, ex);

                if (errorCallback != null)
                {
                    return errorCallback(ex);
                }

                return null;
            }
        }

        /// <summary>
        /// Realiza a chamada a ação informada e caso ocorra alguma exceção faz o tratamento padrão de erros para controllers.
        /// </summary>
         /// <param name="controller">A controller alvo.</param>
        /// <param name="action">A ação a ser executada.</param>        
        /// <param name="errorCallback">A action que será chamada caso ocorra um erro na execução de action.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "O método é exatamente para garantir que qualquer exceção seja tratada adequada para ser apresentada ao usuário.")]
        public static void Call(this ControllerBase controller, Action action, Action<Exception> errorCallback = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                SetErrorMessage(controller, ex);

                if (errorCallback != null)
                {
                    errorCallback(ex);
                }
            }
        }
        #endregion
    }
}
