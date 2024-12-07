using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okala.Exchange.Core.ContributorAggregate;

namespace Okala.Exchange.Core.CoinAggregate;
public class CurrencyRate
{
  public Currency Currency { get;private set; }
  public decimal Rate { get;private set; }

  public CurrencyRate(Currency currency, decimal rate)
  {
    Currency = currency;
    Rate = rate;
  }
#pragma warning disable CS8618 
  private CurrencyRate(){}
#pragma warning restore CS8618 


  internal static CurrencyRate Create(Currency currency, decimal rate)=>new CurrencyRate(currency,rate);
}
