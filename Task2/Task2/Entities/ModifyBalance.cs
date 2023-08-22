using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Entities
{
    // расширеный класс баланса для аналогчного отображения с экселем 
    public class ModifyBalance
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }

        public decimal? IbAssets { get; set; }

        public decimal? IbPassive { get; set; }

        public decimal? Debit { get; set; }

        public decimal? Credit { get; set; }
        public decimal? ObAssets { get; set; }
        public decimal? ObPassive { get; set; }

        public ModifyBalance(Balance row)
        {
            Id = row.Id;
            AccountNumber = row.AccountNumber;
            IbAssets = row.IbAssets;
            IbPassive = row.IbPassive;
            Debit = row.Debit;
            Credit = row.Credit;
            if (IbAssets != 0 && IbPassive == 0)
            {
                ObAssets = IbAssets + Debit - Credit;
                ObPassive = 0;
            }
            if (IbPassive != 0 && IbAssets == 0)
            {
                ObPassive = IbPassive + Debit - Credit;
                ObAssets = 0;
            }
            if (IbAssets == 0 && IbPassive == 0)
            {
                ObPassive = 0;
                ObAssets = 0;
            }
        }

        public ModifyBalance(ModifyBalance row)
        {
            Id = row.Id;
            AccountNumber = row.AccountNumber;
            IbAssets = row.IbAssets;
            IbPassive = row.IbPassive;
            Debit = row.Debit;
            Credit = row.Credit;
            ObAssets = row.ObAssets;
            ObPassive = row.ObPassive;
        }
        public ModifyBalance(string groupNumber, IGrouping<string,ModifyBalance> balances)
        {
            Id = 0;
            IbAssets = 0;
            IbPassive = 0;
            Debit = 0;
            Credit = 0;
            ObAssets = 0;
            ObPassive = 0;
            AccountNumber = Int32.Parse(groupNumber);
            foreach (var balance in balances)
            {
                IbAssets += balance.IbAssets;
                IbPassive += balance.IbPassive;
                Debit += balance.Debit;
                Credit += balance.Credit;
                ObAssets += balance.ObAssets;
                ObPassive += balance.ObPassive;
            }
        }
    }
}
