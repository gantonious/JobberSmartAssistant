﻿using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Jobber.Sdk.Models.Financials
{
    public class TransactionCollection
    {
        [JsonProperty("transactions")]
        public IEnumerable<Transaction> Transactions { get; set; }

        public decimal GetTotal()
        {
            return Transactions
                .Where(transaction => transaction.IsInvoice())
                .Sum(transaction => transaction.GetAmountValue());
        }
    }
}