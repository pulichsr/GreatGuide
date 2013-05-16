namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IRepositoryLocator
  {
    T LocateRepository<T>();
  }
}