using System;
using System.Collections.Generic;

namespace Nucleo.GoodGuide.Bll
{
  public interface ISanityChecker
  {
    Boolean Check(List<string> ErrorList);
  }

  public class SanityCheckers : List<ISanityChecker>,ISanityChecker
  {
    public Boolean Check(List<string> ErrorList)
    {
      Boolean SanityOk = true;
      for (Int32 CheckerNo = 0; CheckerNo < ErrorList.Count; CheckerNo++)
        if (this[CheckerNo].Check(ErrorList) == false)
          SanityOk = false;

      return SanityOk;
    }
  }
}
