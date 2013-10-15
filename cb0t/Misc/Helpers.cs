﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace cb0t
{
    class Helpers
    {
        public static uint UserIdent { get; set; }

        static Helpers()
        {
            UserIdent = 0;
        }

        public static String AresColorToHTMLColor(byte c)
        {
            switch (c)
            {
                case 1: return "#000000";
                case 0: return "#FFFFFF";
                case 8: return "#FFFF00";
                case 11: return "#00FFFF";
                case 12: return "#0000FF";
                case 2: return "#000080";
                case 6: return "#800080";
                case 9: return "#00FF00";
                case 13: return "#FF00FF";
                case 14: return "#808080";
                case 15: return "#C0C0C0";
                case 7: return "#FFA500";
                case 5: return "#800000";
                case 10: return "#008080";
                case 3: return "#008000";
                case 4: return "#FF0000";
                default: return null;
            }
        }

        public static String StripColors(String input)
        {
            if (Regex.IsMatch(input, @"\x03|\x05", RegexOptions.IgnoreCase))
                input = Regex.Replace(input, @"(\x03|\x05)[0-9]{2}", "");

            input = input.Replace("\x06", "");
            input = input.Replace("\x07", "");
            input = input.Replace("\x09", "");
            input = input.Replace("\x02", "");
            input = input.Replace("­", "");

            return input;
        }

        public static String FormatAresColorCodes(String text)
        {
            String result = text;
            result = result.Replace("\x00023", "\x0003");
            result = result.Replace("\x00025", "\x0005");
            result = result.Replace("\x00026", "\x0006");
            result = result.Replace("\x00027", "\x0007");
            result = result.Replace("\x00029", "\x0009");
            return result;
        }

        public static bool IsHexCode(String hex)
        {
            if (hex.Substring(0, 1) != "#")
                return false;

            String str = hex.Substring(1);
            String r_str = str.Substring(0, 2);
            String g_str = str.Substring(2, 2);
            String b_str = str.Substring(4, 2);

            byte r, g, b;

            if (byte.TryParse(r_str, NumberStyles.HexNumber, null, out r))
                if (byte.TryParse(g_str, NumberStyles.HexNumber, null, out g))
                    if (byte.TryParse(b_str, NumberStyles.HexNumber, null, out b))
                        return true;

            return false;
        }

        public static String Timestamp
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                DateTime dt = DateTime.Now;
                sb.Append(dt.Hour < 10 ? ("0" + dt.Hour) : dt.Hour.ToString());
                sb.Append(":");
                sb.Append(dt.Minute < 10 ? ("0" + dt.Minute) : dt.Minute.ToString());
                sb.Append(" ");
                return sb.ToString();
            }
        }

        public static String CountryCodeToString(byte country)
        {
            switch (country)
            {
                case 1:
                    return "Afghanistan";
                case 2:
                    return "Albania";
                case 3:
                    return "Algeria";
                case 4:
                    return "Andorra";
                case 5:
                    return "Angola";
                case 6:
                    return "Anguilla";
                case 7:
                    return "Antarctica";
                case 8:
                    return "Antigua and Barbuda";
                case 9:
                    return "Argentina";
                case 10:
                    return "Armenia";
                case 11:
                    return "Aruba";
                case 12:
                    return "Australia";
                case 13:
                    return "Austria";
                case 14:
                    return "Azerbaijan";
                case 15:
                    return "Bahamas";
                case 16:
                    return "Bahrain";
                case 17:
                    return "Bangladesh";
                case 18:
                    return "Barbados";
                case 19:
                    return "Belarus";
                case 20:
                    return "Belgium";
                case 21:
                    return "Belize";
                case 22:
                    return "Berin";
                case 23:
                    return "Bermuda";
                case 24:
                    return "Bhutan";
                case 25:
                    return "Bolivia";
                case 26:
                    return "Bosnia and Herzegovina";
                case 27:
                    return "Botswana";
                case 28:
                    return "Brazil";
                case 29:
                    return "Brunei";
                case 30:
                    return "Bulgaria";
                case 31:
                    return "Burkina Faso";
                case 32:
                    return "Burundi";
                case 33:
                    return "Cambodia";
                case 34:
                    return "Cameroon";
                case 35:
                    return "Canada";
                case 36:
                    return "Cape Verde";
                case 37:
                    return "Cayman Islands";
                case 38:
                    return "Central African Republic";
                case 39:
                    return "Chad";
                case 40:
                    return "Chile";
                case 41:
                    return "China";
                case 42:
                    return "Christmas Islands";
                case 43:
                    return "Cocos Islands";
                case 44:
                    return "Colombia";
                case 45:
                    return "Comoros";
                case 46:
                    return "Congo";
                case 47:
                    return "Congo";
                case 48:
                    return "Cook Islands";
                case 49:
                    return "Costa Rica";
                case 50:
                    return "Croatia";
                case 51:
                    return "Cuba";
                case 52:
                    return "Cyprus";
                case 53:
                    return "Czech Republic";
                case 54:
                    return "Denmark";
                case 55:
                    return "Fjibouti";
                case 56:
                    return "Dominica";
                case 57:
                    return "Dominican Republic";
                case 58:
                    return "Dutch antilles";
                case 59:
                    return "East Timor";
                case 60:
                    return "Ecuador";
                case 61:
                    return "Egypt";
                case 62:
                    return "El Salvador";
                case 63:
                    return "Equatorial Guinea";
                case 64:
                    return "Entea";
                case 65:
                    return "Estonia";
                case 66:
                    return "Ethiopia";
                case 67:
                    return "Falkland Islands";
                case 68:
                    return "Faroe Islands";
                case 69:
                    return "Fiji Islands";
                case 70:
                    return "Finland";
                case 71:
                    return "France";
                case 72:
                    return "French Polynesia";
                case 73:
                    return "Gabon";
                case 74:
                    return "Gambia";
                case 75:
                    return "Gaza";
                case 76:
                    return "Georgia";
                case 77:
                    return "Germany";
                case 78:
                    return "Ghana";
                case 79:
                    return "Gibraltar";
                case 80:
                    return "Greece";
                case 81:
                    return "Greenland";
                case 82:
                    return "Grenada";
                case 83:
                    return "Guadalupe";
                case 84:
                    return "Guatemala";
                case 85:
                    return "Guernsey";
                case 86:
                    return "Guinea";
                case 87:
                    return "Guinea-Bissau";
                case 88:
                    return "Guyana";
                case 89:
                    return "Guyana";
                case 90:
                    return "Haiti";
                case 91:
                    return "Honduras";
                case 92:
                    return "Hong Kong";
                case 93:
                    return "Hungary";
                case 94:
                    return "Iceland";
                case 95:
                    return "India";
                case 96:
                    return "Indonesia";
                case 97:
                    return "Iran";
                case 98:
                    return "Iraq";
                case 99:
                    return "Ireland";
                case 100:
                    return "Isle of Man";
                case 101:
                    return "Israel";
                case 102:
                    return "Italy";
                case 103:
                    return "Ivory Coast";
                case 104:
                    return "Jamaica";
                case 105:
                    return "Japan";
                case 106:
                    return "Jersey";
                case 107:
                    return "Jordan";
                case 108:
                    return "Kazakhstan";
                case 109:
                    return "Kenya";
                case 110:
                    return "Kiribati";
                case 111:
                    return "Kuwwait";
                case 112:
                    return "Kyrgyzstan";
                case 113:
                    return "Laos";
                case 114:
                    return "Latvia";
                case 115:
                    return "Lebanon";
                case 116:
                    return "Lesotho";
                case 117:
                    return "Liberia";
                case 118:
                    return "Libya";
                case 119:
                    return "Liechtenstein";
                case 120:
                    return "Lithuania";
                case 121:
                    return "Luxembourg";
                case 122:
                    return "Macao";
                case 123:
                    return "Macedonia";
                case 124:
                    return "Madagascar";
                case 125:
                    return "Malawi";
                case 126:
                    return "Malaysia";
                case 127:
                    return "Maldives";
                case 128:
                    return "Mali";
                case 129:
                    return "Malta";
                case 130:
                    return "Marshall Islands";
                case 131:
                    return "Martinique";
                case 132:
                    return "Mauritania";
                case 133:
                    return "Mauritius";
                case 134:
                    return "Mayotte";
                case 135:
                    return "Mexico";
                case 136:
                    return "Micronesia";
                case 137:
                    return "Moldova";
                case 138:
                    return "Monaco";
                case 139:
                    return "Mongolia";
                case 140:
                    return "Montserrat";
                case 141:
                    return "Morocco";
                case 142:
                    return "Mozambique";
                case 143:
                    return "Myanmar";
                case 144:
                    return "Namibia";
                case 145:
                    return "Nauru";
                case 146:
                    return "Nepal";
                case 147:
                    return "Netherlands";
                case 148:
                    return "New Caledonia";
                case 149:
                    return "New Zealand";
                case 150:
                    return "Nicaragua";
                case 151:
                    return "Niger";
                case 152:
                    return "Nigeria";
                case 153:
                    return "Niue";
                case 154:
                    return "Norfolk Island";
                case 155:
                    return "North Korea";
                case 156:
                    return "Norway";
                case 157:
                    return "Oman";
                case 158:
                    return "Pakistan";
                case 159:
                    return "Palau";
                case 160:
                    return "Panama";
                case 161:
                    return "Papua New Guinea";
                case 162:
                    return "Paraguay";
                case 163:
                    return "Peru";
                case 164:
                    return "Phillippines";
                case 165:
                    return "Pitcairn Island";
                case 166:
                    return "Poland";
                case 167:
                    return "Portugal";
                case 168:
                    return "Puerto Rico";
                case 169:
                    return "Qatar";
                case 170:
                    return "Reunion";
                case 171:
                    return "Romania";
                case 172:
                    return "Russia";
                case 173:
                    return "Rwanda";
                case 174:
                    return "Samoa";
                case 175:
                    return "San Marino";
                case 176:
                    return "Sao Tome and Principe";
                case 177:
                    return "Saudi Arabia";
                case 178:
                    return "Senegal";
                case 179:
                    return "Seychelles";
                case 180:
                    return "Sierra Leone";
                case 181:
                    return "Singapore";
                case 182:
                    return "Slovakia";
                case 183:
                    return "Slovenia";
                case 184:
                    return "Solomon Island";
                case 185:
                    return "Somalia";
                case 186:
                    return "South Africa";
                case 187:
                    return "South Georgia Island";
                case 188:
                    return "South Korea";
                case 189:
                    return "Spain";
                case 190:
                    return "Sri Lanka";
                case 191:
                    return "St Helens";
                case 192:
                    return "St Kitts and Nevis";
                case 193:
                    return "St Lucia";
                case 194:
                    return "St Pierre and Miquelon";
                case 195:
                    return "St Vicent";
                case 196:
                    return "Sudan";
                case 197:
                    return "Suriname";
                case 198:
                    return "Svalbard";
                case 199:
                    return "Swaziland";
                case 200:
                    return "Sweden";
                case 201:
                    return "Switzerland";
                case 202:
                    return "Syria";
                case 203:
                    return "Taiwan";
                case 204:
                    return "Tajikistan";
                case 205:
                    return "Tanzania";
                case 206:
                    return "Thailand";
                case 207:
                    return "Togo";
                case 208:
                    return "Tokelau";
                case 209:
                    return "Tonga";
                case 210:
                    return "Trinidad and Tobago";
                case 211:
                    return "Tunisia";
                case 212:
                    return "Turkey";
                case 213:
                    return "Turkmenistan";
                case 214:
                    return "Turks and Caicos Islands";
                case 215:
                    return "Tuvalu";
                case 216:
                    return "Uganda";
                case 217:
                    return "Ukraine";
                case 218:
                    return "United Arab Emirates";
                case 219:
                    return "United Kingdom";
                case 220:
                    return "United States";
                case 221:
                    return "Uruguay";
                case 222:
                    return "Uzbekistan";
                case 223:
                    return "Vanuatu";
                case 224:
                    return "Venezuela";
                case 225:
                    return "Vietnam";
                case 226:
                    return "Virgin Islands";
                case 227:
                    return "Wallis and Futuna";
                case 228:
                    return "West Bank";
                case 229:
                    return "Western Sahara";
                case 230:
                    return "Yemen";
                case 231:
                    return "Yugoslavia";
                case 232:
                    return "Zambia";
                case 233:
                    return "Zimbabwe";
            }

            return "?";
        }
    }
}
