using System;

namespace Mediachase.Ibn.Web.Drawing.Gantt
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
		public static DateTime GetFirstWeekOfYear(int year)
		{
			// get the date for the 4-Jan for this year
			DateTime date = new DateTime(year, 1, 4);

			// get the ISO day number for this date 1==Monday, 7==Sunday
			int dayNumber = (int)date.DayOfWeek; // 0==Sunday, 6==Saturday
			if (dayNumber == 0)
			{
				dayNumber = 7;
			}

			// return the date of the Monday that is less than or equal
			// to this date
			return date.AddDays(1 - dayNumber);
		}

		/// <summary>
		/// Gets the week number.
		/// </summary>
		/// <param name="dt">The dt.</param>
		/// <returns></returns>
		public static int GetWeekNumber(DateTime date)
		{
			DateTime dateOfFirstWeekOfYear;
			int isoYear = date.Year;
			if (date >= new DateTime(isoYear, 12, 29))
			{
				dateOfFirstWeekOfYear = GetFirstWeekOfYear(isoYear + 1);
				if (date < dateOfFirstWeekOfYear)
				{
					dateOfFirstWeekOfYear = GetFirstWeekOfYear(isoYear);
				}
				else
				{
					isoYear++;
				}
			}
			else
			{
				dateOfFirstWeekOfYear = GetFirstWeekOfYear(isoYear);
				if (date < dateOfFirstWeekOfYear)
				{
					dateOfFirstWeekOfYear = GetFirstWeekOfYear(--isoYear);
				}
			}

			return ((date - dateOfFirstWeekOfYear).Days / 7 + 1);
		}

		/// <summary>
		/// Gets the year week number [Year] * 100 + [WeekNumber].
		/// </summary>
		/// <param name="dt">The dt.</param>
		/// <returns></returns>
		public static int GetYearWeekNumber(DateTime date)
		{
			DateTime dateOfFirstWeekOfYear;
			int isoYear = date.Year;

			if (date >= new DateTime(isoYear, 12, 29))
			{
				dateOfFirstWeekOfYear = GetFirstWeekOfYear(isoYear + 1);
				if (date < dateOfFirstWeekOfYear)
				{
					dateOfFirstWeekOfYear = GetFirstWeekOfYear(isoYear);
				}
				else
				{
					isoYear++;
				}
			}
			else
			{
				dateOfFirstWeekOfYear = GetFirstWeekOfYear(isoYear);
				if (date < dateOfFirstWeekOfYear)
				{
					dateOfFirstWeekOfYear = GetFirstWeekOfYear(--isoYear);
				}
			}

			return (isoYear * 100) + ((date - dateOfFirstWeekOfYear).Days / 7 + 1);
		}
	}
}
