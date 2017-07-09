using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.IO;
using System.Collections;
using System.Collections.Specialized;

namespace MB.TheBeerHouse
{
   public static class Helpers
   {
      private static string[] _countries = new string[] { 
         "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", 
         "Angola", "Anguilla", "Antarctica", "Antigua And Barbuda", "Argentina", 
         "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan",
		   "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus",
		   "Belgium", "Belize", "Benin", "Bermuda", "Bhutan",
		   "Bolivia", "Bosnia Hercegovina", "Botswana", "Bouvet Island", "Brazil",
		   "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Byelorussian SSR",
		   "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands",
		   "Central African Republic", "Chad", "Chile", "China", "Christmas Island",
		   "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Cook Islands",
		   "Costa Rica", "Cote D'Ivoire", "Croatia", "Cuba", "Cyprus",
		   "Czech Republic", "Czechoslovakia", "Denmark", "Djibouti", "Dominica",
		   "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador",
		   "England", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia",
		   "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France",
		   "Gabon", "Gambia", "Georgia", "Germany", "Ghana",
		   "Gibraltar", "Great Britain", "Greece", "Greenland", "Grenada",
		   "Guadeloupe", "Guam", "Guatemela", "Guernsey", "Guiana",
		   "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard Islands",
		   "Honduras", "Hong Kong", "Hungary", "Iceland", "India",
		   "Indonesia", "Iran", "Iraq", "Ireland", "Isle Of Man",
		   "Israel", "Italy", "Jamaica", "Japan", "Jersey",
		   "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, South",
		   "Korea, North", "Kuwait", "Kyrgyzstan", "Lao People's Dem. Rep.", "Latvia",
		   "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein",
		   "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar",
		   "Malawi", "Malaysia", "Maldives", "Mali", "Malta",
		   "Mariana Islands", "Marshall Islands", "Martinique", "Mauritania", "Mauritius",
		   "Mayotte", "Mexico", "Micronesia", "Moldova", "Monaco",
		   "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar",
		   "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles",
		   "Neutral Zone", "New Caledonia", "New Zealand", "Nicaragua", "Niger",
		   "Nigeria", "Niue", "Norfolk Island", "Northern Ireland", "Norway",
		   "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea",
		   "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland",
		   "Polynesia", "Portugal", "Puerto Rico", "Qatar", "Reunion",
		   "Romania", "Russian Federation", "Rwanda", "Saint Helena", "Saint Kitts",
		   "Saint Lucia", "Saint Pierre", "Saint Vincent", "Samoa", "San Marino",
		   "Sao Tome and Principe", "Saudi Arabia", "Scotland", "Senegal", "Seychelles",
		   "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
		   "Somalia", "South Africa", "South Georgia", "Spain", "Sri Lanka",
		   "Sudan", "Suriname", "Svalbard", "Swaziland", "Sweden",
		   "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikista", "Tanzania",
		   "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago",
		   "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu",
		   "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States",
		   "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City State", "Venezuela",
		   "Vietnam", "Virgin Islands", "Wales", "Western Sahara", "Yemen",
		   "Yugoslavia", "Zaire", "Zambia", "Zimbabwe"};

      /// <summary>
      /// Returns an array with all countries
      /// </summary>     
      public static StringCollection GetCountries()
      {
         StringCollection countries = new StringCollection();
         countries.AddRange(_countries);
         return countries;
      }
      public static SortedList GetCountries(bool insertEmpty)
      {
         SortedList countries = new SortedList();
         if (insertEmpty)
            countries.Add("", "Please select one...");
         foreach (String country in _countries)
            countries.Add(country, country);
         return countries;
      }

      /// <summary>
      /// Returns an array with the names of all local Themes
      /// </summary>
      public static string[] GetThemes()
      {
         if (HttpContext.Current.Cache["SiteThemes"] != null)
         {
            return (string[])HttpContext.Current.Cache["SiteThemes"];
         }
         else
         {
            string themesDirPath = HttpContext.Current.Server.MapPath("~/App_Themes");
            // get the array of themes folders under /app_themes
            string[] themes = Directory.GetDirectories(themesDirPath);
            for (int i = 0; i <= themes.Length - 1; i++)
	            themes[i] = Path.GetFileName(themes[i]);
            // cache the array with a dependency to the folder
            CacheDependency dep = new CacheDependency(themesDirPath);
            HttpContext.Current.Cache.Insert("SiteThemes", themes, dep);
            return themes;
         }
      }

      /// <summary>
      /// Adds the onfocus and onblur attributes to all input controls found in the specified parent,
      /// to change their apperance with the control has the focus
      /// </summary>
      public static void SetInputControlsHighlight(Control container, string className, bool onlyTextBoxes)
      {
         foreach (Control ctl in container.Controls)
         {
            if ((onlyTextBoxes && ctl is TextBox) || ctl is TextBox || ctl is DropDownList ||
	            ctl is ListBox || ctl is CheckBox || ctl is RadioButton || 
	            ctl is RadioButtonList || ctl is CheckBoxList)
            {
               WebControl wctl = ctl as WebControl;
               wctl.Attributes.Add("onfocus", string.Format("this.className = '{0}';", className));
               wctl.Attributes.Add("onblur", "this.className = '';");
            }
            else
            {
               if (ctl.Controls.Count > 0)
                  SetInputControlsHighlight(ctl, className, onlyTextBoxes);
            }
         }
      }


      /// <summary>
      /// Converts the input plain-text to HTML version, replacing carriage returns
      /// and spaces with <br /> and &nbsp;
      /// </summary>
      public static string ConvertToHtml(string content)
      {
         content = HttpUtility.HtmlEncode(content);
         content = content.Replace("  ", "&nbsp;&nbsp;").Replace(
            "\t", "&nbsp;&nbsp;&nbsp;").Replace("\n", "<br>");
         return content;
      }
   }
}
