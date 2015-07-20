using System;
using System.Collections.Generic;
using System.Text;

namespace Mediachase.IBN.Business
{
	/// <summary>
	/// Specifies ISO 8601 week number
	/// </summary>
	public static class Iso8601WeekNumber
	{
		// OZ:
		//		  � ���������  ISO, ���,  ����������� �  ������ ������  ISO, �����
		//        ����������  ��  ������������  ����.   ��������,  1  ������  1988
		//        ��������  ��  53-�  ������  ISO  ���  1987  ����.  ������ ������
		//        ���������� � ������������ � ������������� ������������.
		//
		//            *  ���� 1 ������ ������ �� �������, ������� ��� �����������,
		//               ��  ������,  ����������  1  ������,  ���������  ���������
		//               �������  �����������  ����,  ������  ��� ����������� ����
		//               ���� ������ ����������� ����������� ����.
		//
		//
		//            *  ���� 1 ������ ������  �� �����������, �������, �����  ���
		//               �������, ��  ��� ������  ��������� ������  ������� ������
		//               ����, ������ ��� ����������� ���� ���� ������ �����������
		//               ������ ����.
		//
		//        ��������,  1  ������  1991  ������  �� �������, ������� ������ �
		//        ������������,  31  �������  1990  ��  �����������, 6 ������ 1991
		//        ���������  �������   1.  
		//
		// More Info:
		// http://www.boyet.com/Articles/PublishedArticles/CalculatingtheISOweeknumb.html

		/// <summary>
		/// Gets the first week.
		/// </summary>
		/// <param name="Year">The year.</param>
		/// <returns></returns>
		public static DateTime GetFirstWeekOfYear(int Year)
		{
			// get the date for the 4-Jan for this year
			DateTime dt = new DateTime(Year, 1, 4);

			// get the ISO day number for this date 1==Monday, 7==Sunday
			int dayNumber = (int)dt.DayOfWeek; // 0==Sunday, 6==Saturday
			if (dayNumber == 0)
			{
				dayNumber = 7;
			}

			// return the date of the Monday that is less than or equal
			// to this date
			return dt.AddDays(1 - dayNumber);
		}

		/// <summary>
		/// Gets the week number.
		/// </summary>
		/// <param name="dt">The dt.</param>
		/// <returns></returns>
		public static int GetWeekNumber(DateTime dt)
		{
			DateTime week1;
			int IsoYear = dt.Year;
			if (dt >= new DateTime(IsoYear, 12, 29))
			{
				week1 = GetFirstWeekOfYear(IsoYear + 1);
				if (dt < week1)
				{
					week1 = GetFirstWeekOfYear(IsoYear);
				}
				else
				{
					IsoYear++;
				}
			}
			else
			{
				week1 = GetFirstWeekOfYear(IsoYear);
				if (dt < week1)
				{
					week1 = GetFirstWeekOfYear(--IsoYear);
				}
			}

			return ((dt - week1).Days / 7 + 1);
		}

		/// <summary>
		/// Gets the year week number [Year] * 100 + [WeekNumber].
		/// </summary>
		/// <param name="dt">The dt.</param>
		/// <returns></returns>
		public static int GetYearWeekNumber(DateTime dt)
		{
			DateTime week1;
			int IsoYear = dt.Year;

			if (dt >= new DateTime(IsoYear, 12, 29))
			{
				week1 = GetFirstWeekOfYear(IsoYear + 1);
				if (dt < week1)
				{
					week1 = GetFirstWeekOfYear(IsoYear);
				}
				else
				{
					IsoYear++;
				}
			}
			else
			{
				week1 = GetFirstWeekOfYear(IsoYear);
				if (dt < week1)
				{
					week1 = GetFirstWeekOfYear(--IsoYear);
				}
			}

			return (IsoYear * 100) + ((dt - week1).Days / 7 + 1);
		}
	}
}
