using System;
using System.Linq;
using System.Globalization;
using System.Linq.Expressions;
using Spider.Common;

namespace Spider.Types
{
	[Serializable]
	public class Period
	{
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		
		public string FromStr { get { return From.ToString(ConstData.DB_DATE_TIME); } }
		public string ToStr { get { return To.ToString(ConstData.DB_DATE_TIME); } }

		public bool IsFromOrToExists { get { return ((From != new DateTime()) || (To != new DateTime())); } }
		public bool IsSetEntirePeriod { get { return ((From == new DateTime(1990, 1, 1, 0, 0, 0)) && (To == new DateTime(2999, 12, 31, 23, 59, 59))); } }

		public Period(DateTime from = new DateTime(), DateTime to = new DateTime())
		{
			From = from;
			To = to;
		}

		public void SetDateFromString(string from = "", string to = "")
		{
			if(!String.IsNullOrEmpty(from))
			{
				try	{ From = DateTime.ParseExact(from, ConstData.DATE, CultureInfo.InvariantCulture); } catch {};
			}

			if(!String.IsNullOrEmpty(to))
			{
				try
				{
					To = DateTime.ParseExact(to, ConstData.DATE, CultureInfo.InvariantCulture);

					TimeSpan ts = new TimeSpan(23, 59, 59);
					To = To.Date + ts;

				}catch {}
			}
		}

		public bool IsInThisPeriod(DateTime? src)
		{
			if(src.HasValue)
				return IsInThisPeriod((DateTime)src);
			else
				return false;
		}

		public bool IsInThisPeriod(DateTime src)
		{
			bool ans = false;
			
			if((this.From != new DateTime() || this.To != new DateTime())
			&& (this.From == new DateTime() || this.From <= src)
			&& (this.To == new DateTime() || this.To >= src))
			{
				ans = true;
			}

			return ans;
		}

		public void SetEntirePeriod()
		{
			From = new DateTime(1990, 1, 1, 0, 0, 0);
			To = new DateTime(2999, 12, 31, 23, 59, 59);
		}

		/// <summary>
		/// set From 00:00:00 and To 23:59:59 if value is not new DateTime()
		/// </summary>
		public void SetEntireHourPeriod()
		{
			if(this.From != new DateTime())
				this.From = new DateTime(this.From.Year, this.From.Month, this.From.Day, 0, 0, 0);

			if(this.To != new DateTime())
				this.To = new DateTime(this.To.Year, this.To.Month, this.To.Day, 23, 59, 59);
		}
	}
}
