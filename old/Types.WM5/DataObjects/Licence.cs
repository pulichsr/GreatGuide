using System;
using System.Collections.Generic;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  [Serializable]
  public class Licence: ILicence
  {
    public enum StatusCodes
    {
      Undefined = -1,
      Valid = 0,
      FileNotFound,
      MatchingMacAddressNotFound,
      DecodeError,
      FileReadError,
      Expired,
    }

    public const string PassPhrase = "***briansegal***greatguide***";
    public const string SaltValue = "greatguide";
    public const string InitVector = "***segalbrian***";

    public string MacAddress
    {
      get { return macAddress; }
      set { macAddress = value; }
    }
    public DateTime Expiry
    {
      get { return expiry; }
      set { expiry = value.Date; }
    }

    public StatusCodes StatusCode
    {
      get
      {
        if (IsValid == false)
          return StatusCodes.Expired;

        return StatusCodes.Valid;
      }
    }
    public Boolean IsValid
    {
      get { return IsValidForDate(DateTime.Today); }
    }

    public Boolean IsValidForDate(DateTime date)
    {
      return Expiry.Date >= date.Date;
    }

    private string macAddress;
    private DateTime expiry;

  }

  [Serializable]
  public class Licences: List<Licence>
  {}
}