using Euro2016.VisualComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Euro2016
{
    /// <summary>
    /// A pair of values (a more convenient form of KeyValuePair).
    /// </summary>
    /// <typeparam name="T">the data type of the values (unrestricted)</typeparam>
    public class Pair<T>
    {
        private KeyValuePair<T, T> pair;

        public Pair(T normal, T highlighted)
        {
            this.pair = new KeyValuePair<T, T>(normal, highlighted);
        }

        public T Normal
        {
            get { return this.pair.Key; }
            set { this.pair = new KeyValuePair<T, T>(value, this.pair.Value); }
        }

        public T Highlighted
        {
            get { return this.pair.Value; }
            set { this.pair = new KeyValuePair<T, T>(this.pair.Key, value); }
        }

        public T this[bool highlighted]
        { get { return this.GetValue(highlighted); } }

        public T GetValue(bool highlighted)
        {
            return highlighted ? this.Highlighted : this.Normal;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}", this.Normal.ToString(), this.Highlighted.ToString());
        }
    }
    /// <summary>
    /// A pair of values (a more convenient form of KeyValuePair) identical to class Pair, except more football-match relevant way.
    /// </summary>
    /// <typeparam name="T">the data type of the values (still unrestricted)</typeparam>
    public class PairT<T>
    {
        private KeyValuePair<T, T> pair;

        public PairT(T home, T away)
        {
            this.pair = new KeyValuePair<T, T>(home, away);
        }

        public T Home
        {
            get { return this.pair.Key; }
            set { this.pair = new KeyValuePair<T, T>(value, this.pair.Value); }
        }

        public T Away
        {
            get { return this.pair.Value; }
            set { this.pair = new KeyValuePair<T, T>(this.pair.Key, value); }
        }

        public T this[bool home]
        { get { return this.GetValue(home); } }

        public T GetValue(bool home)
        {
            return home ? this.Home : this.Away;
        }
    }

    /// <summary>
    /// Defines a list of objects that are descended from ObjectWithID, containing only unique IDs.
    /// </summary>
    public class ListOfIDObjects<TYPE> : List<TYPE> where TYPE : ObjectWithID
    {
        /// <summary>Adds the given item to this list if it is unique (that is, if there is no other item in this list with the same ID).</summary>
        public new void Add(TYPE item)
        {
            if (this.GetIndexOfItemByID(item.ID) == -1)
                base.Add(item);
        }

        /// <summary>Iterates over the given list of items and calls this.Add(item) for each one.</summary>
        public new void AddRange(IEnumerable<TYPE> items)
        {
            foreach (TYPE item in items)
                this.Add(item);
        }

        /// <summary>Searches the current list for an item with the given ID, and return its index if it is found, or -1 otherwise.</summary>
        public int GetIndexOfItemByID(string id)
        {
            for (int iItem = 0; iItem < this.Count; iItem++)
                if (this[iItem].ID.Equals(id))
                    return iItem;
            return -1;
        }

        /// <summary>Searches the current list for an item with the given item's ID, and return its index if it is found, or -1 otherwise.</summary>
        public int GetIndexOfItem(TYPE item)
        {
            return this.GetIndexOfItemByID(item.ID);
        }

        /// <summary>Searches the current list for an item with the given ID, and returns it if it is found, or null otherwise.</summary>
        public TYPE GetItemByID(string id)
        {
            int index = this.GetIndexOfItemByID(id);
            return index != -1 ? this[index] : null;
        }

        /// <summary>Searches the current list for an item with the given item's ID, and returns it if it is found, or null otherwise.</summary>
        public TYPE GetItem(TYPE item)
        {
            return item != null ? this.GetItemByID(item.ID) : null;
        }

        public string GetUniqueNumericID(int startAt, int step, int length)
        {
            while (this.GetIndexOfItemByID(string.Format("{0:D" + length + "}", startAt)) != -1)
                startAt += step;
            return string.Format("{0:D" + length + "}", startAt);
        }

        /// <summary>Generates and returns a new list with all of this list's items added to it.</summary>
        public ListOfIDObjects<TYPE> GetDeepCopy()
        {
            ListOfIDObjects<TYPE> result = new ListOfIDObjects<TYPE>();
            foreach (TYPE item in this)
                result.Add(item);
            return result;
        }

        /// <summary>Performs a swap between the items at the given indexes.</summary>
        public void SwapItemsAtPositions(int indexA, int indexB)
        {
            if (indexA >= 0 && indexA < this.Count && indexB >= 0 && indexB < this.Count)
            {
                TYPE aux = this[indexA];
                this[indexA] = this[indexB];
                this[indexB] = aux;
            }
        }
    }

    /// <summary>
    /// Utility functions and extension methods.
    /// </summary>
    public static class Utils
    {
        internal const string NullString = "null";
        internal const string DefaultSeparator = ";";
        internal static readonly Random Random = new Random();

        public static bool IsNumber(string text)
        {
            double number;
            return double.TryParse(text, out number);
        }

        public static void AddAttribute(this XmlNode node, XmlDocument doc, string key, string value)
        {
            XmlAttribute attribute = doc.CreateAttribute(key);
            attribute.Value = value != null ? value : Utils.NullString;
            node.Attributes.Append(attribute);
        }

        public static void AddAttribute(this XmlNode node, XmlDocument doc, string key, int value)
        { node.AddAttribute(doc, key, value.ToString()); }

        public static void AddAttribute(this XmlNode node, XmlDocument doc, string key, ulong? value)
        { node.AddAttribute(doc, key, value.HasValue ? value.Value.ToString() : null); }

        public static void AddAttribute(this XmlNode node, XmlDocument doc, string key, DateTime? value)
        { node.AddAttribute(doc, key, value.HasValue ? value.Value.ToString() : null); }

        public static void AddAttribute(this XmlNode node, XmlDocument doc, Pair<ColorResource> value)
        {
            node.AddAttribute(doc, "normal", ColorTranslator.ToHtml(value.Normal.Color));
            node.AddAttribute(doc, "highlighted", ColorTranslator.ToHtml(value.Highlighted.Color));
        }

        public static string DecodeNullableString(string text)
        { return text == null || text.Equals(Utils.NullString) ? null : text; }

        public static DateTime? DecodeNullableDateTime(string text)
        { return text.Equals(Utils.NullString) ? (DateTime?) null : DateTime.Parse(text); }

        public static ulong? DecodeNullableUnsignedLong(string text)
        { return text.Equals(Utils.NullString) ? (ulong?) null : ulong.Parse(text); }

        public static Point MinimumPointValues(Point a, Point b)
        { return new Point(a.X < b.X ? a.X : b.X, a.Y < b.Y ? a.Y : b.Y); }

        public static Point MaximumPointValues(Point a, Point b)
        { return new Point(a.X > b.X ? a.X : b.X, a.Y > b.Y ? a.Y : b.Y); }

        public static string Plural(string singularForm, long quantity, bool includeQuantity)
        {
            string form = quantity == 1 ? singularForm : singularForm + "s";
            return includeQuantity ? Utils.FormatNumber(quantity) + " " + form : form;
        }

        public static string PluralIfNotZero(string singularForm, long quantity, bool includeQuantity)
        {
            if (quantity == 0)
                return "";
            return Utils.Plural(singularForm, quantity, includeQuantity);
        }

        public static Size ScaleRectangle(int width, int height, int maxWidth, int maxHeight)
        {
            var ratioX = (double) maxWidth / width;
            var ratioY = (double) maxHeight / height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (width * ratio);
            var newHeight = (int) (height * ratio);

            return new Size(newWidth, newHeight);
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight, InterpolationMode mode, bool disposeOldImage)
        {
            Size newSize = ScaleRectangle(image.Width, image.Height, maxWidth, maxHeight);
            Image newImage = new Bitmap(newSize.Width, newSize.Height);
            Graphics g = Graphics.FromImage(newImage);
            g.InterpolationMode = mode;
            g.DrawImage(image, 0, 0, newSize.Width, newSize.Height);
            if (disposeOldImage)
                image.Dispose();
            return newImage;
        }

        public static Image GetScaledImageOrScaledDefault(string imagePath, int maxWidth, int maxHeight, InterpolationMode mode, Image defaultImg)
        {
            try
            { return Utils.ScaleImage(new Bitmap(imagePath), maxWidth, maxHeight, mode, true); }
            catch (Exception)
            { return Utils.ScaleImage(defaultImg, maxWidth, maxHeight, mode, false); }
        }

        public static string FormatNumber(long number)
        {
            return number.ToString("#,##0");
        }

        public static string FormatDuration(TimeSpan duration)
        {
            return (int) duration.TotalMinutes + ":" + duration.Seconds.ToString("D2");
        }

        public static void ApplyAlphaMask(Bitmap bmp, Bitmap alphaMaskImage)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            BitmapData dataAlphaMask = alphaMaskImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try
            {
                BitmapData data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                try
                {
                    unsafe // using pointer requires the unsafe keyword
                    {
                        byte* pData0Mask = (byte*) dataAlphaMask.Scan0;
                        byte* pData0 = (byte*) data.Scan0;

                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                byte* pData = pData0 + (y * data.Stride) + (x * 4);
                                byte* pDataMask = pData0Mask + (y * dataAlphaMask.Stride) + (x * 4);

                                byte maskBlue = pDataMask[0];
                                byte maskGreen = pDataMask[1];
                                byte maskRed = pDataMask[2];

                                // the closer the color is to black the more opaque it will be.
                                byte alpha = (byte) (255 - (maskRed + maskBlue + maskGreen) / 3);

                                // respect the original alpha value
                                byte originalAlpha = pData[3];
                                pData[3] = (byte) (((float) (alpha * originalAlpha)) / 255f);
                            }
                        }
                    }
                }
                finally
                {
                    bmp.UnlockBits(data);
                }
            }
            finally
            {
                alphaMaskImage.UnlockBits(dataAlphaMask);
            }
        }

        public static Image ConvertToGrayscale(Image original)
        {
            //create a blank bitmap the same size as original and get a graphics object 
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][] {
                    new float[] { .3f, .3f, .3f, 0, 0 },
                    new float[] { .59f, .59f, .59f, 0, 0 },
                    new float[] { .11f, .11f, .11f, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { 0, 0, 0, 0, 1 } });

            //create some image attributes and set the color matrix attribute
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image using the grayscale color matrix
            g.DrawImage(original,
                new Rectangle(0, 0, original.Width, original.Height),
                0, 0, original.Width, original.Height,
                GraphicsUnit.Pixel,
                attributes);

            //dispose the Graphics object and return the result
            g.Dispose();
            return newBitmap;
        }

        public static void CheckItemAndUncheckAllOthers<TYPE>(this ICollection<TYPE> controls, TYPE controlToCheck) where TYPE : MyEuroBaseControl
        {
            foreach (TYPE control in controls)
                control.Checked = control.Equals(controlToCheck);
        }

        public static void SizeAndPositionControlsInPanel<TYPE>(System.Windows.Forms.Panel container, IList<TYPE> controls, bool horizontally, int padding) where TYPE : Control
        {
            int newControlSize = (int) (((horizontally ? container.Width : container.Height) - (controls.Count - 1) * padding) / (double) controls.Count);
            for (int index = 0, lastPos = 0; index < controls.Count; index++, lastPos += newControlSize + padding)
            {
                controls[index].Parent = container;
                if (horizontally)
                    controls[index].SetBounds(lastPos, 0, newControlSize, container.Height);
                else
                    controls[index].SetBounds(0, lastPos, container.Width, newControlSize);
            }
        }

        public static void RemoveAllClickEvents(this Control control)
        {
            FieldInfo f1 = typeof(Control).GetField("EventMouseWheel", BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(control);
            PropertyInfo pi = control.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList) pi.GetValue(control, null);
            list.RemoveHandler(obj, list[obj]);
        }

        public static void SwapItemsAtPositions<TYPE>(this List<TYPE> list, int posA, int posB)
        {
            if (posA >= 0 && posA < list.Count && posB >= 0 && posB < list.Count)
            {
                TYPE aux = list[posA];
                list[posA] = list[posB];
                list[posB] = aux;
            }
        }

        public static string GetListString(this List<string> list, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in list)
                sb.Append(item).Append(separator);
            if (sb.Length > 0)
                sb = sb.Remove(sb.Length - separator.Length, separator.Length);
            return sb.ToString();
        }

        /// <summary>Calculates duration between two dates in the typical format for player's ages, in years and additional days.</summary>
        public static Tuple<int, int> CalculateAgeInYearsAndDays(DateTime birthDate, DateTime nowDate)
        {
            int years = nowDate.Year - birthDate.Year;
            if (birthDate > nowDate.AddYears(-years))
                years--;
            int days = (int) nowDate.Subtract(birthDate.AddYears(years)).TotalDays;
            return new Tuple<int, int>(years, days);
        }

        /// <summary>Calculates the (approximate) average age of the players within this list.</summary>
        public static Tuple<int, int> GetAverageAge(this ListOfIDObjects<Player> players)
        {
            double totalDays = 0;
            foreach (Player player in players)
                totalDays += player.Age.Item1 * 365.25 + player.Age.Item2;
            totalDays /= players.Count;
            return new Tuple<int, int>((int) (totalDays / 365.25), (int) (totalDays % 365.25));
        }

        /// <summary>Formats the age in years and days to be nice and clear, with either short-form ('y' and 'd') or long-form ('years' and 'days') descriptors.</summary>
        public static string FormatAge(this Tuple<int, int> age, bool longFormDescriptors)
        {
            StringBuilder result = new StringBuilder();
            if (!longFormDescriptors)
                result.Append(age.Item1 > 0 ? age.Item1 + "y" : "").Append(' ').Append(age.Item2 > 0 ? age.Item2 + "d" : "");
            else
                result.Append(Utils.PluralIfNotZero("year", age.Item1, true)).Append(' ').Append(Utils.PluralIfNotZero("day", age.Item2, true));
            return result.ToString().Trim();
        }

        /// <summary>Sorts the players in this list in the given order and by the given criteria (the column index parameter corresponds to the columns in PlayerViewBase.ColumnCaptions).</summary>
        public static void SortPlayers(this ListOfIDObjects<Player> players, int sortByColumn, bool descending)
        {
            for (int i = 0; i < players.Count - 1; i++)
                for (int j = i + 1; j < players.Count; j++)
                {
                    Player iP = players[i], jP = players[j];
                    bool isAscending = false;

                    switch (sortByColumn)
                    {
                        case 0: // number
                            isAscending = iP.Number < jP.Number;
                            break;
                        case 1: // player name flag, but should never come in with this value
                        case 2: // player name
                            isAscending = iP.Name.CompareTo(jP.Name) < 0;
                            break;
                        case 3: // playing position
                            isAscending = iP.PlayerPosition < jP.PlayerPosition;
                            break;
                        case 4: // date of birth
                            isAscending = iP.BirthDate.CompareTo(jP.BirthDate) > 0;
                            break;
                        case 5: // age, should still sort by exact birth date rather than more approximate year age
                            isAscending = iP.BirthDate.CompareTo(jP.BirthDate) < 0;
                            break;
                        case 6: // caps
                            isAscending = iP.Caps < jP.Caps;
                            break;
                        case 7: // goals
                            isAscending = iP.Goals < jP.Goals;
                            break;
                        case 8: // club flag, but should never come in with this value
                        case 9: // club, sort by club country name first and then by club name
                            int clubCountryNameCompare = iP.Club.Country.Names.Away.CompareTo(jP.Club.Country.Names.Away);
                            isAscending = clubCountryNameCompare == 0 ? (iP.Club.Name.CompareTo(jP.Club.Name) < 0) : clubCountryNameCompare < 0;
                            break;
                        default:
                            break;
                    }

                    if (isAscending == descending)
                        players.SwapItemsAtPositions(i, j);
                }
        }

        /// <summary>Sorts the teams in this list in the given order and by the given criteria (the column index parameter corresponds to the columns in TeamStatsViewBase.ColumnCaptions).</summary>
        public static void SortTeams(this List<Team> teams, Database database, int sortByColumn, bool descending)
        {
            if (teams.Count < 2)
                return;

            // teams, matches, playedMatches, matchesScoreboard, averageStartTime, averageDuration, clubsAndLeagues, homePlay            
            List<Tuple<Team, ListOfIDObjects<Match>, ListOfIDObjects<Match>, MatchScoreboard, TimeSpan, TimeSpan, Tuple<int, int>, Tuple<int>>> list =
                new List<Tuple<Team, ListOfIDObjects<Match>, ListOfIDObjects<Match>, MatchScoreboard, TimeSpan, TimeSpan, Tuple<int, int>, Tuple<int>>>();

            for (int i = 0; i < teams.Count; i++)
            {
                ListOfIDObjects<Match> matches = database.Matches.GetMatchesBy(teams[i]);
                ListOfIDObjects<Match> playedMatches = matches.GetMatchesBy(true);
                MatchScoreboard matchesScoreboard = playedMatches.GetAllGoals(teams[i]);
                TimeSpan averageStartTime = playedMatches.GetAverageStartTime();
                TimeSpan averageDuration = playedMatches.GetAverageDuration();
                Tuple<int, int> clubsAndLeagues = teams[i].GetClubAndLeagueCount();
                int homePlay = teams[i].Players.Count(p => p.Club.Country.Equals(p.Nationality.Country));

                list.Add(new Tuple<Team, ListOfIDObjects<Match>, ListOfIDObjects<Match>, MatchScoreboard, TimeSpan, TimeSpan, Tuple<int, int>, Tuple<int>>(
                    teams[i], matches, playedMatches, matchesScoreboard, averageStartTime, averageDuration, clubsAndLeagues, new Tuple<int>(homePlay)));
            }

            for (int i = 0; i < list.Count - 1; i++)
                for (int j = i + 1; j < list.Count; j++)
                {
                    Tuple<Team, ListOfIDObjects<Match>, ListOfIDObjects<Match>, MatchScoreboard, TimeSpan, TimeSpan, Tuple<int, int>, Tuple<int>> A = list[i], B = list[j];
                    bool isAscending = false;

                    switch (sortByColumn)
                    {
                        case 2: // name
                            isAscending = A.Item1.Country.Names.Away.CompareTo(B.Item1.Country.Names.Away) < 0;
                            break;
                        case 3: // age
                            isAscending = A.Item1.Players.GetAverageAge().Item1 < B.Item1.Players.GetAverageAge().Item1
                                ? true
                                : A.Item1.Players.GetAverageAge().Item1 == B.Item1.Players.GetAverageAge().Item1 && A.Item1.Players.GetAverageAge().Item2 < B.Item1.Players.GetAverageAge().Item2;
                            break;
                        case 4: // time
                            isAscending = A.Item5.CompareTo(B.Item5) < 0;
                            break;
                        case 5: // duration
                            isAscending = A.Item6.CompareTo(B.Item6) < 0;
                            break;
                        case 6: // goals
                            isAscending = A.Item4.FinalScoreWithoutPenalties.GoalDifference < B.Item4.FinalScoreWithoutPenalties.GoalDifference
                                ? true
                                : (A.Item4.FinalScoreWithoutPenalties.GoalDifference == B.Item4.FinalScoreWithoutPenalties.GoalDifference && A.Item4.FinalScoreWithoutPenalties.Home < B.Item4.FinalScoreWithoutPenalties.Home
                                    ? true
                                    : A.Item4.FinalScoreWithoutPenalties.GoalDifference == B.Item4.FinalScoreWithoutPenalties.GoalDifference && A.Item4.FinalScoreWithoutPenalties.Home == B.Item4.FinalScoreWithoutPenalties.Home && A.Item4.FinalScoreWithoutPenalties.Away > B.Item4.FinalScoreWithoutPenalties.Away);
                            break;
                        case 7: // attendance
                            isAscending = A.Item3.GetAttendance() < B.Item3.GetAttendance();
                            break;
                        case 8: // phase
                            int compared = database.CompareCategoryTo(database.TournamentResultOfTeam(A.Item1), database.TournamentResultOfTeam(B.Item1));
                            isAscending = compared > 0
                                ? true : (compared == 0 && A.Item4.FinalScoreWithoutPenalties.GoalDifference < B.Item4.FinalScoreWithoutPenalties.GoalDifference
                                    ? true : (A.Item4.FinalScoreWithoutPenalties.GoalDifference == B.Item4.FinalScoreWithoutPenalties.GoalDifference && A.Item4.FinalScoreWithoutPenalties.Home < B.Item4.FinalScoreWithoutPenalties.Home
                                        ? true
                                        : A.Item4.FinalScoreWithoutPenalties.GoalDifference == B.Item4.FinalScoreWithoutPenalties.GoalDifference && A.Item4.FinalScoreWithoutPenalties.Home == B.Item4.FinalScoreWithoutPenalties.Home && A.Item4.FinalScoreWithoutPenalties.Away > B.Item4.FinalScoreWithoutPenalties.Away));
                            break;
                        case 9: // clubs
                            isAscending = A.Item7.Item1 < B.Item7.Item1;
                            break;
                        case 10: // leagues
                            isAscending = A.Item7.Item2 < B.Item7.Item2;
                            break;
                        case 11: // home play
                            isAscending = A.Rest.Item1 < B.Rest.Item1;
                            break;
                        default:
                            break;
                    }

                    if (isAscending == descending)
                    {
                        list.SwapItemsAtPositions(i, j);
                        teams.SwapItemsAtPositions(i, j);
                    }
                }
        }

        /// <summary>Sets the time offset value for each of the matches in this list.</summary>
        public static void SetTimeOffset(this ListOfIDObjects<Match> matches, double timeOffset)
        {
            foreach (Match match in matches)
                match.WhenOffset = match.When.AddHours(timeOffset);
        }

        /// <summary>Sorts the matches in this list in chronological order.</summary>
        public static void SortMatchesChronologically(this ListOfIDObjects<Match> matches)
        {
            for (int iM = 0; iM < matches.Count - 1; iM++)
                for (int jm = iM + 1; jm < matches.Count; jm++)
                    if (matches[iM].When.CompareTo(matches[jm].When) > 0)
                        matches.SwapItemsAtPositions(iM, jm);
        }

        /// <summary>Searches the group matches of this list and finds the one played between the given teams, then returns the WhichTeamWon property value of the FinalScore of that match, 
        /// for the order in which the two team parameters were passed (-1 for team A, 0 for draw, 1 for team B). Note that if the match has not been played or cannot be found, the returned value will be -2.</summary>
        public static int WhoWonGroupMatchBetween(this List<Match> matches, Team teamA, Team teamB)
        {
            foreach (Match match in matches)
                if (match.IsGroupMatch)
                    if (match.Teams.Home.Equals(teamA) && match.Teams.Away.Equals(teamB))
                        return match.Scoreboard.Played ? match.Scoreboard.FullScore.WhichTeamWon : -2;
                    else if (match.Teams.Home.Equals(teamB) && match.Teams.Away.Equals(teamA))
                        return match.Scoreboard.Played ? -match.Scoreboard.FullScore.WhichTeamWon : -2;
            return -2;
        }

        /// <summary>Calculates the average start time for this collection of matches.</summary>
        public static TimeSpan GetAverageStartTime(this ListOfIDObjects<Match> matches)
        {
            TimeSpan result = new TimeSpan();
            if (matches.Count == 0)
                return result;
            foreach (Match match in matches)
                result = result.Add(match.When.TimeOfDay);
            return new TimeSpan((long) ((double) result.Ticks / matches.Count));
        }

        /// <summary>Calculates the average start time for this collection of matches.</summary>
        public static TimeSpan GetAverageDuration(this ListOfIDObjects<Match> matches)
        {
            TimeSpan result = new TimeSpan(0);
            if (matches.Count == 0)
                return result;
            foreach (Match match in matches)
                result = result.Add(match.MatchDuration);
            return new TimeSpan((long) ((double) result.Ticks / matches.Count));
        }

        /// <summary>Calculates the maximum attendance for this collection of matches.</summary>
        public static int GetAttendance(this ListOfIDObjects<Match> matches)
        {
            int result = 0;
            foreach (Match match in matches)
                result += match.Where.Capacity;
            return result;
        }

        /// <summary>Calculates the number of clubs and leagues respectively that the players of this team play in.</summary>
        public static Tuple<int, int> GetClubAndLeagueCount(this Team team)
        {
            List<string> clubs = new List<string>(), leagues = new List<string>();
            foreach (Player player in team.Players)
            {
                if (!clubs.Contains(player.Club.Name + ":" + player.Club.Country.Names.Away))
                    clubs.Add(player.Club.Name + ":" + player.Club.Country.Names.Away);
                if (!leagues.Contains(player.Club.Country.Names.Away))
                    leagues.Add(player.Club.Country.Names.Away);
            }
            return new Tuple<int, int>(clubs.Count, leagues.Count);
        }

        /// <summary>Generates a quasi-random match scoreboard.</summary>
        public static MatchScoreboard GetRandomResult(bool canEndInDraw)
        {
            List<HalfScoreboard> halves = new List<HalfScoreboard>();
            halves.Add(new HalfScoreboard(Utils.Random.Next(5), Utils.Random.Next(5)));
            halves.Add(new HalfScoreboard(Utils.Random.Next(5), Utils.Random.Next(5)));
            MatchScoreboard result = new MatchScoreboard(halves);
            if (canEndInDraw || result.FullScore.WhichTeamWon != 0)
                return result;
            halves.Add(new HalfScoreboard(Utils.Random.Next(3), Utils.Random.Next(3)));
            halves.Add(new HalfScoreboard(Utils.Random.Next(3), Utils.Random.Next(3)));
            result.SetHalves(halves);
            if (result.FullScore.WhichTeamWon != 0)
                return result;
            int home = 0, away = 0;
            while (home == away)
            {
                home = Utils.Random.Next(6);
                away = Utils.Random.Next(6);
            }
            halves.Add(new HalfScoreboard(home, away));
            result.SetHalves(halves);
            return result;
        }

        /// <summary>Generates and returns a match scoreboard with all the goals from the given team's matches added up.</summary>
        public static MatchScoreboard GetAllGoals(this ListOfIDObjects<Match> matches, Team team)
        {
            List<HalfScoreboard> resultHalves = new List<HalfScoreboard>();
            foreach (Match match in matches)
                if (team == null || ((match.Teams.Home != null && match.Teams.Home.Equals(team)) || (match.Teams.Away != null && match.Teams.Away.Equals(team))))
                    for (int iHalf = 0; iHalf < 5; iHalf++)
                        if (iHalf < match.Scoreboard.Halves.Count)
                        {
                            if (resultHalves.Count <= iHalf)
                                resultHalves.Add(new HalfScoreboard());
                            HalfScoreboard half = team == null || match.Teams.Home.Equals(team)
                                ? match.Scoreboard.Halves[iHalf]
                                : new HalfScoreboard(match.Scoreboard.Halves[iHalf].Away, match.Scoreboard.Halves[iHalf].Home);
                            resultHalves[iHalf].AddHalfScoreboard(half);
                        }
            return new MatchScoreboard(resultHalves);
        }

        /// <summary>Counts the wins, ties and losses in the given match list from the perspective of the 'home' team.</summary>
        public static Tuple<int, int, int> GetResults(this ListOfIDObjects<Match> matches)
        {
            int wins = 0, ties = 0, losses = 0;
            foreach (Match match in matches)
                if (match.Scoreboard.Played)
                {
                    if (match.Scoreboard.FullScore.HomeWin)
                        wins++;
                    else if (match.Scoreboard.FullScore.Tie)
                        ties++;
                    else if (match.Scoreboard.FullScore.AwayWin)
                        losses++;
                }
            return new Tuple<int, int, int>(wins, ties, losses);
        }

        /// <summary>Searches this list of table lines' for the given team, and returns its index if it is found, or -1 otherwise.</summary>
        public static int IndexOfTeam(this List<TableLine> lines, Team team)
        {
            for (int index = 0; index < lines.Count; index++)
                if (lines[index].Team != null && lines[index].Team.Equals(team))
                    return index;
            return -1;
        }

        /// <summary>Searches this list of groups for the one that contains in its table lines the given team.</summary>
        public static Group GetGroupContainingTeam(this ListOfIDObjects<Group> groups, Team team)
        {
            foreach (Group group in groups)
                foreach (TableLine tableLine in group.TableLines)
                    if (tableLine.Team.Equals(team))
                        return group;
            return null;
        }

        /// <summary>Searches this list of clubs for the club with the given name and in the given country, and returns it if it is found, or null otherwise.</summary>
        public static Club GetClubByNameAndCountry(this ListOfIDObjects<Club> clubs, string name, Country country)
        {
            foreach (Club club in clubs)
                if (club.Name.Equals(name) && club.Country.Equals(country))
                    return club;
            return null;
        }

        /// <summary>Searches this list of matches and returns a sublist containing all items that are relevant to the given parameter. 
        /// The parameter can be an instance of Venue, Team, Group, DateTime, string (category), bool (played) or int (number of halves).</summary>
        public static ListOfIDObjects<Match> GetMatchesBy(this ListOfIDObjects<Match> matches, object whatever)
        {
            ListOfIDObjects<Match> result = new ListOfIDObjects<Match>();

            if (whatever is Venue)
            {
                Venue venue = whatever as Venue;
                foreach (Match match in matches)
                    if (match.Where.Equals(venue))
                        result.Add(match);
            }
            else if (whatever is Team)
            {
                Team team = whatever as Team;
                foreach (Match match in matches)
                    if ((match.Teams.Home != null && match.Teams.Home.Equals(team)) || (match.Teams.Away != null && match.Teams.Away.Equals(team)))
                        result.Add(match);
            }
            else if (whatever is Group)
            {
                Group group = whatever as Group;
                foreach (Match match in matches)
                    if (match.IsGroupMatch && match.Category.Split(':')[1].Equals(group.ID))
                        result.Add(match);
            }
            else if (whatever is DateTime)
            {
                DateTime date = (DateTime) whatever;
                foreach (Match match in matches)
                    if (match.When.Date.Equals(date.Date))
                        result.Add(match);
            }
            else if (whatever is string)
            {
                string category = whatever as string;
                foreach (Match match in matches)
                    if (match.Category.Contains(category))
                        result.Add(match);
            }
            else if (whatever is bool)
            {
                bool played = (bool) whatever;
                foreach (Match match in matches)
                    if (match.Scoreboard.Played == played)
                        result.Add(match);
            }
            else if (whatever is int)
            {
                int nHalves = (int) whatever;
                foreach (Match match in matches)
                    if (match.Scoreboard.Halves.Count == nHalves)
                        result.Add(match);
            }

            return result;
        }

        /// <summary>Formats the category of a match to make it look nice and clear.</summary>
        public static string FormatMatchCategory(string category)
        {
            string[] parts = category.Split(':');
            switch (parts[0])
            {
                case "G":
                    return "Group " + parts[1];
                case "KO":
                    switch (parts[1])
                    {
                        case "8":
                            return "Eighth-final";
                        case "4":
                            return "Quarter-final";
                        case "2":
                            return "Semi-final";
                        case "1":
                            return "Grand final";
                        default:
                            return "Unknown knockout";
                    }
                default:
                    return "unknown";
            }
        }

        /// <summary>Determines the app-convened color for a given team's country, depending on the team's result in the tournament. To be used on the SVG map.</summary>
        public static Color TournamentResultColorForTeam(string matchCategory)
        {
            string[] matchCategoryParts = matchCategory.Split(':');
            switch (matchCategoryParts[0])
            {
                case "G":
                    return ColorTranslator.FromHtml("#27A39D");
                case "KO":
                    switch (matchCategoryParts[1])
                    {
                        case "8":
                            return ColorTranslator.FromHtml("#46A327");
                        case "4":
                            return ColorTranslator.FromHtml("#D1E61C");
                        case "2":
                            return ColorTranslator.FromHtml("#F59611");
                        case "1":
                            return ColorTranslator.FromHtml("#F51111");
                        default:
                            return Color.White;
                    }
                default:
                    switch (matchCategory)
                    {
                        case "unknownCountry":
                            return ColorTranslator.FromHtml("#000000");
                        case "notUEFA":
                            return ColorTranslator.FromHtml("#C4C4C4");
                        case "notQualified":
                            return ColorTranslator.FromHtml("#707070");
                        default:
                            return Color.White;
                    }
            }
        }
    }

    /// <summary>
    /// Contains static data that must be initialized immediately after the app starts, such as images and fonts.
    /// </summary>
    public static class StaticData
    {
        public static SortedDictionary<string, Bitmap> Images { get; private set; }
        public static PrivateFontCollection PVC { get; private set; }
        public static int FontExoLight_Index { get; private set; }
        public static int FontExo_Index { get; private set; }
        public static int FontExoBold_Index { get; private set; }

        static StaticData()
        {
            StaticData.Images = new SortedDictionary<string, Bitmap>();
        }

        public static string LoadData()
        {
            try
            {
                StaticData.Images.Add(Paths.LogoImageFile, new Bitmap(Paths.LogoImageFile));
                StaticData.Images.Add(Paths.UnknownTeamImageFile, new Bitmap(Paths.UnknownTeamImageFile));
                StaticData.Images.Add(Paths.KnockoutImageFile, new Bitmap(Paths.KnockoutImageFile));

                StaticData.PVC = new PrivateFontCollection();
                StaticData.PVC.AddFontFile(Paths.FontsFolder + "exo2-xlite.ttf");
                StaticData.FontExoLight_Index = 0;
                StaticData.PVC.AddFontFile(Paths.FontsFolder + "exo2.ttf");
                StaticData.FontExo_Index = 0;
                StaticData.PVC.AddFontFile(Paths.FontsFolder + "exo2-bold.ttf");
                StaticData.FontExoBold_Index = 0;

                return "";
            }
            catch (Exception E)
            {
                return "ERROR: StaticImages.LoadImages()\n\n" + E.ToString();
            }
        }
    }

    /// <summary>
    /// Contains folder and file paths (relative to the app executable) that are needed for the application, as well as related utility methods.
    /// </summary>
    public static class Paths
    {
        public static readonly string ProgramFilesFolder = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName + "/program-files/";
        public static readonly string ResourcesFolder = ProgramFilesFolder + "resources/";
        public static readonly string FontsFolder = ResourcesFolder + "fonts/";
        public static readonly string FlagsFolder = ProgramFilesFolder + "flags/";
        public static readonly string CitiesFolder = ProgramFilesFolder + "cities/";
        public static readonly string StadiumLocationsFolder = ProgramFilesFolder + "stadium-locations/";
        public static readonly string StadiumOutsidesFolder = ProgramFilesFolder + "stadiums-outside/";
        public static readonly string StadiumInsidesFolder = ProgramFilesFolder + "stadiums-inside/";

        public static readonly string DatabaseFile = ProgramFilesFolder + "database.xml";
        public static readonly string DatabasePlayersFile = ProgramFilesFolder + "database_players.xml";
        public static readonly string DatabasePlayersInputFile = ProgramFilesFolder + "players_input.txt";
        public static readonly string LogoImageFile = ResourcesFolder + "logo.png";
        public static readonly string UnknownTeamImageFile = ResourcesFolder + "unknownTeam.png";
        public static readonly string KnockoutImageFile = ResourcesFolder + "knockout.png";
        public static readonly string SvgMapFile = ResourcesFolder + "svgMap.svg";

        public static readonly string[] Folders = { ProgramFilesFolder, ResourcesFolder, FlagsFolder, CitiesFolder, StadiumLocationsFolder, StadiumOutsidesFolder, StadiumInsidesFolder };
        public static readonly string[] Files = { DatabaseFile, DatabasePlayersFile, LogoImageFile, UnknownTeamImageFile, KnockoutImageFile, SvgMapFile };

        /// <summary>Checks that all files and folders in the respective static string lists of the Paths class exist.</summary>
        /// <returns>an empty string if execution ended successfully, or the error description otherwise</returns>
        public static string CheckPaths(bool tryToCreateMissingFolders)
        {
            string phase = "initializing";
            try
            {
                phase = "checking folders";

                foreach (string folder in Folders)
                {
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    if (!Directory.Exists(folder))
                        throw new Exception("Folder '" + folder + "' does not exist!");
                }

                foreach (string file in Files)
                    if (!File.Exists(file))
                        throw new Exception("File '" + file + "' does not exist!");

                return "";
            }
            catch (Exception E)
            {
                return "ERROR: Paths.CheckPaths(), phase '" + phase + "'\n\n" + E.ToString();
            }
        }
    }
}