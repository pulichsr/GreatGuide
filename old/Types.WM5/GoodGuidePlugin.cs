using System;
using Nucleo.Events;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.Types
{
  public class GoodGuidePlugin : Plugin
  {
    public Boolean IsLoggingEnabled
    {
      get
      {
        if (LoggingServices == null)
          return false;

        return LoggingServices.Enabled;
      }
      set
      {
        if (LoggingServices == null)
          return;

        LoggingServices.Enabled = value;
      }
    }

    public virtual void Initialise(NamedParameters initialisationParameters)
    {
      #region RepositoryLocator
      NamedParameter mpRepositoryLocator = initialisationParameters["RepositoryLocator"];
      if (mpRepositoryLocator == null)
        throw new InvalidOperationException(string.Format("RepositoryLocator initialisation parameter is undefined in {0}", GetType().Name));
      RepositoryLocator = (IRepositoryLocator)mpRepositoryLocator.Value;
      #endregion

      #region EventBroker
      NamedParameter mpEventBroker = initialisationParameters["EventBroker"];
      if (mpEventBroker == null)
        throw new InvalidOperationException(string.Format("EventBroker initialisation parameter is undefined in {0}",GetType().Name));
      EventBroker = (EventBroker)mpEventBroker.Value;
      #endregion

      #region NamedParameterRepository
      NamedParameter mpNamedParameterRepository = initialisationParameters[ConfigurationParameters.System.NamedParameterRepository];
      if (mpNamedParameterRepository != null)
      {
        object Value = mpNamedParameterRepository.Value;
        if (Value != null)
          ParameterRepository = (INamedParameterRepository)Value;
      }
      #endregion

      #region Logger
      NamedParameter mpLogger = initialisationParameters[ConfigurationParameters.System.Logger];
      if (mpLogger == null)
        throw new InvalidOperationException(string.Format("Logger initialisation parameter is undefined in {0}", GetType().Name));

      object value = mpLogger.Value;
      if (value == null)
        throw new InvalidOperationException(string.Format("Logger initialisation parameter has null value in {0}", GetType().Name));

      Logger = (ILogger)value;
      LoggingServices = new LoggingServices(Logger);
      #endregion

      EventBroker.Register(this);
    }
    public virtual void Finalise()
    {
    }

    protected IRepositoryLocator RepositoryLocator;
    protected INamedParameterRepository ParameterRepository;
    protected EventBroker EventBroker;
    protected ILogger Logger;
    protected LoggingServices LoggingServices;
  }
}
