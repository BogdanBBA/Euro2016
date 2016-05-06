using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Euro2016
{
    /// <summary>
    /// Defines an object with a string ID.
    /// </summary>
    public abstract class ObjectWithID
    {
        /// <summary>Gets and privately sets the ID of the object. Should be unique.</summary>
        public string ID { get; private set; }

        /// <summary>Constructs a new ObjectWithID object from the given string ID parameter.</summary>
        public ObjectWithID(string id)
        {
            this.ID = id;
        }

        /// <summary>Generates an XmlNode from this object.</summary>
        /// <param name="doc">the XmlDocument used to create the element</param>
        /// <param name="name">the name of the XmlNode to be created</param>
        public XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = doc.CreateElement(name);
            node.AddAttribute(doc, "ID", this.ID);
            return node;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ObjectWithID))
                return false;
            return this.ID.Equals((obj as ObjectWithID).ID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.ID;
        }
    }

    /// <summary>
    /// Defines information related to a match venue.
    /// </summary>
    public class Venue : ObjectWithID
    {
        /// <summary>Gets and privately sets the name of the venue.</summary>
        public string Name { get; private set; }
        /// <summary>Gets and privately sets the city in which the venue is located.</summary>
        public string City { get; private set; }
        /// <summary>Gets and privately sets the geographic coordinates of the venue.</summary>
        public PointF Location { get; private set; }
        /// <summary>Gets and privately sets the year in which the venue was opened (not in which construction began).</summary>
        public int YearOpened { get; private set; }
        /// <summary>Gets and privately sets the seating capacity of the venue.</summary>
        public int Capacity { get; private set; }

        /// <summary>Constructs a new Venue object from the given parameters.</summary>
        /// <param name="id">the ID of the venue</param>
        /// <param name="name">the name of the venue</param>
        /// <param name="city">the city in which the venue is located</param>
        /// <param name="location">the geographic coordinates of the venue</param>
        /// <param name="yearOpened">the year in which the venue was opened</param>
        /// <param name="capacity">the seating capacity of the venue</param>
        public Venue(string id, string name, string city, PointF location, int yearOpened, int capacity)
            : base(id)
        {
            this.Name = name;
            this.City = city;
            this.Location = location;
            this.YearOpened = yearOpened;
            this.Capacity = capacity;
        }

        /// <summary>Parses the given XmlNode into a new Venue object.</summary>
        public static Venue Parse(XmlNode node)
        {
            string id = node.Attributes["ID"].Value;
            string name = node.Attributes["name"].Value;
            string city = node.Attributes["city"].Value;
            string[] locationParts = node.Attributes["location"].Value.Split(',');
            PointF location = new PointF(float.Parse(locationParts[0]), float.Parse(locationParts[1]));
            int year = Int32.Parse(node.Attributes["year"].Value);
            int capacity = Int32.Parse(node.Attributes["capacity"].Value);
            return new Venue(id, name, city, location, year, capacity);
        }

        /// <summary>Generates an XmlNode from this object.</summary>
        /// <param name="doc">the XmlDocument used to create the element</param>
        /// <param name="name">the name of the XmlNode to be created</param>
        public new XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = base.ToXml(doc, name);
            node.AddAttribute(doc, "name", this.Name);
            node.AddAttribute(doc, "city", this.City);
            node.AddAttribute(doc, "location", this.Location.X + "," + this.Location.Y);
            node.AddAttribute(doc, "year", this.YearOpened);
            node.AddAttribute(doc, "capacity", this.Capacity);
            return node;
        }

        public override string ToString()
        {
            return string.Format("{0}. {1}, {2} ({3})", this.ID, this.Name, this.City, this.Capacity);
        }
    }

    /// <summary>
    /// Defines information related to a country used within the app. Does not need to be a country with a team participating in the final tournament, or even part of UEFA.
    /// </summary>
    public class Country : ObjectWithID
    {
        /// <summary>Gets and privately sets the name pair (native and english languages) of the country.</summary>
        public PairT<string> Names { get; private set; }
        /// <summary>Gets and privately sets whether the country is part of UEFA.</summary>
        public bool UefaCountry { get; private set; }
        /// <summary>Gets and privately sets the full-size flag image for this country.</summary>
        public Bitmap FlagOriginal { get; private set; }
        /// <summary>Gets and privately sets the flag image (resized from the original to fit in 160 x 100px) for this country.</summary>
        public Bitmap Flag100px { get; private set; }
        /// <summary>Gets and privately sets the flag image (resized from the original to fit in 64 x 40px) for this country.</summary>
        public Bitmap Flag40px { get; private set; }
        /// <summary>Gets and privately sets the flag image (resized from the original to fit in 32 x 20px) for this country.</summary>
        public Bitmap Flag20px { get; private set; }

        /// <summary>Constructs a new Country object from the given parameters.</summary>
        /// <param name="id">the ID of the country</param>
        /// <param name="names">the name pair of the country</param>
        /// <param name="uefa">whether the country is part of UEFA</param>
        /// <param name="flagOriginal">the full-size flag image for this country</param>
        public Country(string id, PairT<string> names, bool uefa, Bitmap flagOriginal)
            : base(id)
        {
            this.Names = names;
            this.UefaCountry = uefa;
            this.FlagOriginal = flagOriginal;
            this.Flag100px = (Bitmap) Utils.ScaleImage(this.FlagOriginal, 160, 100, InterpolationMode.HighQualityBicubic, false);
            this.Flag40px = (Bitmap) Utils.ScaleImage(this.Flag100px, 64, 40, InterpolationMode.HighQualityBicubic, false);
            this.Flag20px = (Bitmap) Utils.ScaleImage(this.Flag40px, 32, 20, InterpolationMode.HighQualityBicubic, false);
        }

        /// <summary>Parses the given XmlNode into a new Country object.</summary>
        public static Country Parse(XmlNode node)
        {
            string id = node.Attributes["ID"].Value;
            string[] names = node.Attributes["names"].Value.Split('|');
            bool uefa = bool.Parse(node.Attributes["uefa"].Value);
            string flagPath = Paths.FlagsFolder + id + ".png";
            Bitmap flag = File.Exists(flagPath) ? new Bitmap(flagPath) : StaticData.Images[Paths.UnknownTeamImageFile];
            return new Country(id, new PairT<string>(names[0], names[1]), uefa, flag);
        }

        /// <summary>Generates an XmlNode from this object.</summary>
        /// <param name="doc">the XmlDocument used to create the element</param>
        /// <param name="name">the name of the XmlNode to be created</param>
        public new XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = base.ToXml(doc, name);
            node.AddAttribute(doc, "names", this.Names.Home + "|" + this.Names.Away);
            node.AddAttribute(doc, "uefa", this.UefaCountry.ToString());
            return node;
        }

        public override string ToString()
        {
            return base.ToString() + ". " + this.Names;
        }
    }

    /// <summary>
    /// Defines information related to a club.
    /// </summary>
    public class Club : ObjectWithID
    {
        /// <summary>Gets and privately sets the name of the club.</summary>
        public string Name { get; private set; }
        /// <summary>Gets and privately sets the country of the club.</summary>
        public Country Country { get; private set; }
        /// <summary>Gets and internally sets the player list of the club.</summary>
        public ListOfIDObjects<Player> Players { get; internal set; }

        /// <summary>Constructs a new Club object from the given parameters.</summary>
        /// <param name="id">the name of the club</param>
        /// <param name="name">the country of the club</param>
        /// <param name="country">the player list of the club</param>
        public Club(string id, string name, Country country)
            : base(id)
        {
            this.Name = name;
            this.Country = country;
        }

        /// <summary>Parses the given XmlNode into a new Club object.</summary>
        public static Club Parse(XmlNode node, ListOfIDObjects<Country> countries)
        {
            string id = node.Attributes["ID"].Value;
            string name = node.Attributes["name"].Value;
            Country country = countries.GetItemByID(node.Attributes["countryID"].Value);
            return new Club(id, name, country);
        }

        /// <summary>Generates an XmlNode from this object.</summary>
        /// <param name="doc">the XmlDocument used to create the element</param>
        /// <param name="name">the name of the XmlNode to be created</param>
        public new XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = base.ToXml(doc, name);
            node.AddAttribute(doc, "name", this.Name);
            node.AddAttribute(doc, "countryID", this.Country.ID);
            return node;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} ({2} players)", this.Name, this.Country.ID, this.Players.Count);
        }
    }

    /// <summary>
    /// Defines summary information related to a player.
    /// </summary>
    public class Player : ObjectWithID
    {
        /// <summary>Defines possible playing positions for a player.</summary>
        public enum PlayingPosition { GK, DF, MF, FW };
        /// <summary>Defines the colors of the player playing positions.</summary>
        public static readonly Color[] PlayingPositionColors = { ColorTranslator.FromHtml("#FF8400"), ColorTranslator.FromHtml("#5BBD00"), ColorTranslator.FromHtml("#0071BD"), ColorTranslator.FromHtml("#BD0035") };

        /// <summary>Gets or privately sets the name of the player.</summary>
        public string Name { get; private set; }
        /// <summary>Gets or privately sets the shirt number of the player.</summary>
        public int Number { get; private set; }
        /// <summary>Gets or privately sets the playing position of the player.</summary>
        public PlayingPosition PlayerPosition { get; private set; }
        /// <summary>Gets or privately sets the birth date of the player.</summary>
        public DateTime BirthDate { get; private set; }
        /// <summary>Gets or privately sets the caps (number of selections in the national team) of the player.</summary>
        public int Caps { get; private set; }
        /// <summary>Gets or privately sets the number of goals for the national team of the player.</summary>
        public int Goals { get; private set; }
        /// <summary>Gets or privately sets the nationality of the player. The property type is Team, as we are only interested in players from countries with participating teams.</summary>
        public Team Nationality { get; private set; }
        /// <summary>Gets or privately sets the club where the player is registered.</summary>
        public Club Club { get; private set; }

        /// <summary>Constructs a new Player object from the given parameters.</summary>
        /// <param name="id">the ID of the player</param>
        /// <param name="name">the name of the player</param>
        /// <param name="number">the shirt number of the player</param>
        /// <param name="position">the playing position of the player</param>
        /// <param name="birth">the birth date of the player</param>
        /// <param name="caps">the caps of the player</param>
        /// <param name="goals">the number of goals of the player</param>
        /// <param name="nationality">the nationality of the player</param>
        /// <param name="club">the club where the player is registered</param>
        public Player(string id, string name, int number, PlayingPosition position, DateTime birth, int caps, int goals, Team nationality, Club club)
            : base(id)
        {
            this.Name = name;
            this.Number = number;
            this.PlayerPosition = position;
            this.BirthDate = birth;
            this.Caps = caps;
            this.Goals = goals;
            this.Nationality = nationality;
            this.Club = club;
        }

        /// <summary>Parses the given XmlNode into a new Player object.</summary>
        public static Player Parse(XmlNode node, List<Team> teams, ListOfIDObjects<Club> clubs)
        {
            string id = node.Attributes["ID"].Value;
            string name = node.Attributes["name"].Value;
            int number = Int32.Parse(node.Attributes["number"].Value);
            PlayingPosition position = (PlayingPosition) Enum.Parse(typeof(PlayingPosition), node.Attributes["position"].Value);
            DateTime birth = DateTime.Parse(node.Attributes["birthDate"].Value);
            int caps = Int32.Parse(node.Attributes["caps"].Value);
            int goals = Int32.Parse(node.Attributes["goals"].Value);
            Team nationality = teams.First(t => t.Country.ID.Equals(node.Attributes["countryID"].Value));
            Club club = clubs.GetItemByID(node.Attributes["clubID"].Value);
            return new Player(id, name, number, position, birth, caps, goals, nationality, club);
        }

        /// <summary>Generates an XmlNode from this object.</summary>
        /// <param name="doc">the XmlDocument used to create the element</param>
        /// <param name="name">the name of the XmlNode to be created</param>
        public new XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = base.ToXml(doc, name);
            node.AddAttribute(doc, "name", this.Name);
            node.AddAttribute(doc, "number", this.Number);
            node.AddAttribute(doc, "position", this.PlayerPosition.ToString());
            node.AddAttribute(doc, "birthDate", this.BirthDate.ToShortDateString());
            node.AddAttribute(doc, "caps", this.Caps);
            node.AddAttribute(doc, "goals", this.Goals);
            node.AddAttribute(doc, "countryID", this.Nationality.Country.ID);
            node.AddAttribute(doc, "clubID", this.Club.ID);
            return node;
        }

        public override string ToString()
        {
            return string.Format("{0}. {1} ({2}/{3})", this.Number, this.Name, this.Nationality.Country.ID, this.Club.Name);
        }
    }

    /// <summary>
    /// Defines information related to a Team that is participating in the competition. Each team has a country associated with it (1:1 relationship), but not all countries in the database are participating.
    /// </summary>
    public class Team
    {
        /// <summary>Gets or privately sets the country associated with the team.</summary>
        public Country Country { get; private set; }
        /// <summary>Gets or privately sets the nicknames of the team.</summary>
        public List<string> Nicknames { get; private set; }
        /// <summary>Gets or privately sets the map coordinates for the flag. It is used to position a 32x20 FlagView control over the fixed-size map of Europe loaded from the SVG resource file.</summary>
        public Point MapCoords { get; private set; }
        /// <summary>Gets or privately sets the nationality and name of the coach of the team.</summary>
        public KeyValuePair<Country, string> Coach { get; private set; }
        /// <summary>Gets or privately sets the list of players of the team.</summary>
        public ListOfIDObjects<Player> Players { get; private set; }

        /// <summary>Constructs a new Team object from the given parameters.</summary>
        /// <param name="country">the country associated with the team</param>
        /// <param name="nicknames">the nicknames of the team</param>
        /// <param name="mapCoords">the fixed-size SVG map coordinates for the FlagView control</param>
        /// <param name="coach">the nationality and name of the coach of the team</param>
        public Team(Country country, List<string> nicknames, Point mapCoords, KeyValuePair<Country, string> coach)
        {
            this.Country = country;
            this.Nicknames = nicknames;
            this.MapCoords = mapCoords;
            this.Coach = coach;
            this.Players = new ListOfIDObjects<Player>();
        }

        /// <summary>Parses the given XmlNode into a new Team object.</summary>
        public static Team Parse(XmlNode node, ListOfIDObjects<Country> countries)
        {
            Country country = countries.GetItemByID(node.Attributes["countryID"].Value);
            List<string> nicknames = node.Attributes["nicknames"].Value.Split(';').ToList();
            Point mapCoords = new Point(Int32.Parse(node.Attributes["mapCoords"].Value.Split(',')[0]), Int32.Parse(node.Attributes["mapCoords"].Value.Split(',')[1]));
            string[] coachParts = node.Attributes["coach"].Value.Split(':');
            KeyValuePair<Country, string> coach = new KeyValuePair<Country, string>(countries.GetItemByID(coachParts[0]), coachParts[1]);
            return new Team(country, nicknames, mapCoords, coach);
        }

        /// <summary>Generates an XmlNode from this object.</summary>
        /// <param name="doc">the XmlDocument used to create the element</param>
        /// <param name="name">the name of the XmlNode to be created</param>
        public XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = doc.CreateElement(name);
            node.AddAttribute(doc, "countryID", this.Country.ID);
            node.AddAttribute(doc, "nicknames", this.Nicknames.GetListString(";"));
            node.AddAttribute(doc, "coach", string.Format("{0}:{1}", this.Coach.Key.ID, this.Coach.Value));
            node.AddAttribute(doc, "mapCoords", this.MapCoords.X + "," + this.MapCoords.Y);
            return node;
        }

        public override bool Equals(object obj)
        {
            Team team = obj as Team;
            return team == null ? false : this.Country.Equals(team.Country);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Country={0}", this.Country != null ? this.Country.ID : "null");
        }
    }

    /// <summary>
    /// Defines information related to one team's group ranking table line. Contains information such as number of matches played or goals scored.
    /// </summary>
    public class TableLine
    {
        /// <summary>Gets or internally sets the team of this table line.</summary>
        public Team Team { get; internal set; }
        /// <summary>Gets or sets the group ranking position for this table line.</summary>
        public int Position { get; set; }
        /// <summary>Gets or sets the number of matches played for this table line.</summary>
        public int MatchesPlayed { get; set; }
        /// <summary>Gets or sets the number of matches won for this table line.</summary>
        public int Won { get; set; }
        /// <summary>Gets or sets the number of matches drawn for this table line.</summary>
        public int Drawn { get; set; }
        /// <summary>Gets or sets the number of matches lost for this table line.</summary>
        public int Lost { get; set; }
        /// <summary>Gets or sets the number of goals scored by the team of this table line.</summary>
        public int GoalsFor { get; set; }
        /// <summary>Gets or sets the number of goals scored against the team of this table line.</summary>
        public int GoalsAgainst { get; set; }
        /// <summary>Gets or sets the goal difference for this table line.</summary>
        public int GoalDifference { get; set; }
        /// <summary>Gets or sets the number of points for this table line.</summary>
        public int Points { get; set; }

        /// <summary>Constructs a new TableLine object, with the given team and all other fields reset.</summary>
        public TableLine(Team team)
        {
            this.Team = team;
            this.Reset();
        }

        /// <summary>Constructs a new TableLine object, by copying the values from the given parameter TableLine object.</summary>
        public TableLine(TableLine tableLine)
        {
            this.Team = tableLine.Team;
            this.Position = tableLine.Position;
            this.MatchesPlayed = tableLine.MatchesPlayed;
            this.Won = tableLine.Won;
            this.Drawn = tableLine.Drawn;
            this.Lost = tableLine.Lost;
            this.GoalsFor = tableLine.GoalsFor;
            this.GoalsAgainst = tableLine.GoalsAgainst;
            this.GoalDifference = this.GoalsFor - this.GoalsAgainst;
            this.Points = 3 * this.Won + this.Drawn;
        }

        /// <summary>Resets all fields of this table line to default values.</summary>
        public void Reset()
        {
            this.Position = 1;
            this.MatchesPlayed = 0;
            this.Won = this.Drawn = this.Lost = 0;
            this.GoalsFor = this.GoalsAgainst = 0;
            this.GoalDifference = 0;
            this.Points = 0;
        }

        /// <summary>Updates the values of this table line by adding a new match result from the given match (if valid).</summary>
        /// <param name="match">the match result to add. Will be ignored if not played or table line team not involved</param>
        public void AddGroupMatchResult(Match match)
        {
            if (!match.Scoreboard.Played || !match.IsGroupMatch)
                return;
            if (this.Team.Equals(match.Teams.Home)) // this team is home
            {
                switch (match.Scoreboard.FullScore.WhichTeamWon)
                {
                    case -1: // home
                        this.Won++;
                        break;
                    case 0: // tie
                        this.Drawn++;
                        break;
                    case 1: // lost
                        this.Lost++;
                        break;
                }
                this.GoalsFor += match.Scoreboard.FullScore.Home;
                this.GoalsAgainst += match.Scoreboard.FullScore.Away;
            }
            else if (this.Team.Equals(match.Teams.Away))
            {
                switch (match.Scoreboard.FullScore.WhichTeamWon) // this team is away
                {
                    case -1: // home
                        this.Lost++;
                        break;
                    case 0: // tie
                        this.Drawn++;
                        break;
                    case 1: // lost
                        this.Won++;
                        break;
                }
                this.GoalsFor += match.Scoreboard.FullScore.Away;
                this.GoalsAgainst += match.Scoreboard.FullScore.Home;
            }
            else
                return;
            this.MatchesPlayed = this.Won + this.Drawn + this.Lost;
            this.GoalDifference = this.GoalsFor - this.GoalsAgainst;
            this.Points = 3 * this.Won + this.Drawn;
        }

        /// <summary>Formats the goal difference of this table line to look nice and clear.</summary>
        public string FormatGoalDifference
        {
            get { return this.GoalDifference == 0 ? "0" : (this.GoalDifference > 0 ? "+" + this.GoalDifference : this.GoalDifference.ToString()); }
        }

        public override string ToString()
        {
            return string.Format("{0}. {1}m {2}/{3}/{4} {5}-{6} {7}g {8}pts",
                this.Team.Country.ID, this.MatchesPlayed, this.Won, this.Drawn, this.Lost, this.GoalsFor, this.GoalsAgainst, this.GoalDifference, this.Points);
        }
    }

    /// <summary>
    /// Defines information related to a rankings group.
    /// </summary>
    public class Group : ObjectWithID
    {
        /// <summary>Gets or privately sets the name of the group.</summary>
        public string Name { get; private set; }
        /// <summary>Gets or privately sets the list of table lines of the group.</summary>
        public List<TableLine> TableLines { get; private set; }

        /// <summary>Constructs a new Group object from the given parameters.</summary>
        /// <param name="id">the ID of the group</param>
        /// <param name="name">the name of the group</param>
        /// <param name="tableLines">the list of table lines of the group</param>
        public Group(string id, string name, List<TableLine> tableLines)
            : base(id)
        {
            this.Name = name;
            this.TableLines = tableLines;
        }

        /// <summary>Determines whether all the matches in this group have been played. More specifically, tests whether all table lines in the group have exactly 3 matches played.</summary>
        public bool AllMatchesPlayed
        {
            get
            {
                foreach (TableLine line in this.TableLines)
                    if (line.MatchesPlayed != 3)
                        return false;
                return true;
            }
        }

        /// <summary>Sorts the table lines in this group.</summary>
        /// <param name="thirdPlacedTeamGroup">whether to apply the ruleset for sorting third-placed teams</param>
        /// <param name="matches">the reference to the full match list of the database</param>
        public void SortTableLines(bool thirdPlacedTeamGroup, List<Match> matches)
        {
            // sort by points
            for (int iIndex = 0; iIndex < this.TableLines.Count - 1; iIndex++)
                for (int jIndex = iIndex + 1; jIndex < this.TableLines.Count; jIndex++)
                    if (this.TableLines[iIndex].Points < this.TableLines[jIndex].Points)
                        this.TableLines.SwapItemsAtPositions(iIndex, jIndex);

            // take each set of teams with equal points
            List<TableLine> tempLines = new List<TableLine>();
            int iStart = 0, iEnd = 0;
            while (iEnd < this.TableLines.Count)
            {
                // get set of teams with equal points (and remember full group interval with iStart and iEnd)
                tempLines.Clear();
                for (; iEnd < this.TableLines.Count && this.TableLines[iEnd].Points == this.TableLines[iStart].Points; iEnd++)
                    tempLines.Add(thirdPlacedTeamGroup ? new TableLine(this.TableLines[iEnd]) : new TableLine(this.TableLines[iEnd].Team));
                iEnd--;

                // if normal group, register matches for this set (a kind of subgroup of teams, with matches only between them); otherwise, existing matches are good
                if (!thirdPlacedTeamGroup)
                {
                    foreach (Match match in matches)
                        if (tempLines.IndexOfTeam(match.Teams.Home) != -1 && tempLines.IndexOfTeam(match.Teams.Away) != -1)
                            foreach (TableLine tLine in tempLines)
                                tLine.AddGroupMatchResult(match);
                }

                // if more than one team, sort by appropriate criteria
                if (tempLines.Count > 1)
                {
                    if (!thirdPlacedTeamGroup) // normal group, involves direct matches and more complicated criteria (and risk of incorrect output, given combination of results, and lack of certain types of input data)
                    {
                        for (int iIndex = 0; iIndex < tempLines.Count - 1; iIndex++)
                            for (int jIndex = iIndex + 1; jIndex < tempLines.Count; jIndex++)
                            {
                                TableLine iT = tempLines[iIndex], jT = tempLines[jIndex];
                                bool needToSwap = false;

                                if (iT.Points < jT.Points)
                                    needToSwap = true;
                                else if (iT.Points == jT.Points)
                                {
                                    if (iT.GoalDifference < jT.GoalDifference)
                                        needToSwap = true;
                                    else if (iT.GoalDifference == jT.GoalDifference)
                                    {
                                        if (iT.GoalsFor < jT.GoalsFor)
                                            needToSwap = true;
                                        else if (iT.GoalsFor == jT.GoalsFor)
                                            needToSwap = iT.Team.Country.Names.Away.CompareTo(jT.Team.Country.Names.Away) > 0;
                                    }
                                }

                                if (needToSwap)
                                    tempLines.SwapItemsAtPositions(iIndex, jIndex);
                            }

                        for (int fullTableIndex = iStart; fullTableIndex <= iEnd; fullTableIndex++)
                        {
                            int tempTableIndex = fullTableIndex - iStart;
                            if (!this.TableLines[fullTableIndex].Team.Equals(tempLines[tempTableIndex].Team))
                                this.TableLines.SwapItemsAtPositions(fullTableIndex, this.TableLines.IndexOfTeam(tempLines[tempTableIndex].Team));
                        }
                    }
                    else // third-placed teams group, more simple sorting criteria
                    {
                        for (int iIndex = 0; iIndex < tempLines.Count - 1; iIndex++)
                            for (int jIndex = iIndex + 1; jIndex < tempLines.Count; jIndex++)
                            {
                                TableLine iT = tempLines[iIndex], jT = tempLines[jIndex];
                                bool needToSwap = false;

                                if (iT.Points < jT.Points)
                                    needToSwap = true;
                                else if (iT.Points == jT.Points)
                                {
                                    if (iT.GoalDifference < jT.GoalDifference)
                                        needToSwap = true;
                                    else if (iT.GoalDifference == jT.GoalDifference)
                                    {
                                        if (iT.GoalsFor < jT.GoalsFor)
                                            needToSwap = true;
                                        else if (iT.GoalsFor == jT.GoalsFor)
                                            needToSwap = iT.Team.Country.Names.Away.CompareTo(jT.Team.Country.Names.Away) > 0;
                                    }
                                }

                                if (needToSwap)
                                    tempLines.SwapItemsAtPositions(iIndex, jIndex);
                            }
                    }
                }

                iEnd++;
                iStart = iEnd;
            }

            // assign position number
            for (int index = 0; index < this.TableLines.Count; index++)
                this.TableLines[index].Position = index + 1;
        }

        /// <summary>Parses the given XmlNode into a new Group object.</summary>
        public static Group Parse(XmlNode node, List<Team> teams)
        {
            string id = node.Attributes["ID"].Value;
            string name = node.Attributes["name"].Value;
            List<TableLine> tableLines = new List<TableLine>();
            string[] countryIDs = node.Attributes["teams"].Value.Split(',');
            foreach (string countryID in countryIDs)
                tableLines.Add(new TableLine(teams.First(t => t.Country.ID.Equals(countryID))));
            return new Group(id, name, tableLines);
        }

        /// <summary>Generates an XmlNode from this object.</summary>
        /// <param name="doc">the XmlDocument used to create the element</param>
        /// <param name="name">the name of the XmlNode to be created</param>
        public new XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = base.ToXml(doc, name);
            node.AddAttribute(doc, "name", this.Name);
            List<string> countryIDs = new List<string>();
            foreach (TableLine line in this.TableLines)
                countryIDs.Add(line.Team.Country.ID);
            countryIDs.Sort();
            node.AddAttribute(doc, "teams", countryIDs.GetListString(","));
            return node;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1} teams)", this.Name, this.TableLines.Count);
        }
    }

    /// <summary>
    /// Defines information related to a simple scoreboard for one gameplay half.
    /// </summary>
    public class HalfScoreboard : PairT<int>
    {
        /// <summary>Constructs a new HalfScoreboard object with default values (0-0).</summary>
        public HalfScoreboard()
            : this(0, 0)
        {
        }

        /// <summary>Constructs a new HalfScoreboard object with the given values.</summary>
        public HalfScoreboard(int home, int away)
            : base(home, away)
        {
        }

        /// <summary>Determines whether the home team has won this half.</summary>
        public bool HomeWin
        { get { return this.Home > this.Away; } }

        /// <summary>Determines whether the home teams have drawn in this half.</summary>
        public bool Tie
        { get { return this.Home == this.Away; } }

        /// <summary>Determines whether the away team has won this half.</summary>
        public bool AwayWin
        { get { return this.Home < this.Away; } }

        /// <summary>Returns a value indicating which team has won this half. The value is -1 for a home win, 0 for a tie, and 1 for an away win.</summary>
        public int WhichTeamWon
        { get { return this.Tie ? 0 : (this.HomeWin ? -1 : 1); } }

        /// <summary>Adds the goal values of the given HalfScoreboard object to the goals of the current half.</summary>
        public void AddHalfScoreboard(HalfScoreboard scoreboard)
        {
            this.Home += scoreboard.Home;
            this.Away += scoreboard.Away;
        }

        /// <summary>Resets the goal values of this half scoreboard to 0-0.</summary>
        public void Reset()
        {
            this.Home = this.Away = 0;
        }

        /// <summary>Formats the score to look nice and clear.</summary>
        public string FormatHalfScore
        {
            get { return this.Home + "-" + this.Away; }
        }

        /// <summary>Parses the given XmlNode into a new HalfScoreboard object.</summary>
        public static HalfScoreboard Parse(string text)
        {
            string[] parts = text.Split('-');
            return new HalfScoreboard(Int32.Parse(parts[0]), Int32.Parse(parts[1]));
        }

        public override string ToString()
        {
            return this.FormatHalfScore;
        }
    }

    /// <summary>
    /// Defines information related to a full, match scoreboard.
    /// </summary>
    public class MatchScoreboard
    {
        /// <summary>Gets or privately sets the list of scoreboards for the gameplay halves.</summary>
        public List<HalfScoreboard> Halves { get; private set; }
        /// <summary>Gets or privately sets the full score scoreboard, potentially including a fifth HalfScoreboard object with the score of the penalty shootout.</summary>
        public HalfScoreboard FullScore { get; private set; }
        /// <summary>Gets or privately sets the full score scoreboard, without any penalties.</summary>
        public HalfScoreboard FinalScoreWithoutPenalties { get; private set; }

        /// <summary>Constructs a new MatchScoreboard object from the given parameter list of halves.</summary>
        public MatchScoreboard(List<HalfScoreboard> halves)
        {
            this.Halves = new List<HalfScoreboard>();
            this.FullScore = new HalfScoreboard();
            this.FinalScoreWithoutPenalties = new HalfScoreboard();
            this.SetHalves(halves);
        }

        /// <summary>Sets the list of halves of this match scoreboard and updates the final scores.</summary>
        public void SetHalves(List<HalfScoreboard> halves)
        {
            this.Halves.Clear();
            this.FullScore.Reset();
            this.FinalScoreWithoutPenalties.Reset();
            for (int iHalf = 0; iHalf < halves.Count; iHalf++)
            {
                this.Halves.Add(halves[iHalf]);
                this.FullScore.AddHalfScoreboard(halves[iHalf]);
                if (iHalf < 4)
                    this.FinalScoreWithoutPenalties.AddHalfScoreboard(halves[iHalf]);
            }
        }

        /// <summary>Determines whether the match finished in regular playing time. More specifically, it tests whether there are exactly 2 halves.</summary>
        public bool FinishedInRegularTime
        { get { return this.Halves.Count == 2; } }

        /// <summary>Determines whether the match finished in extra playing time. More specifically, it tests whether there are exactly 4 halves.</summary>
        public bool FinishedInExtraTime
        { get { return this.Halves.Count == 4; } }

        /// <summary>Determines whether the match finished at the penalty shootout. More specifically, it tests whether there are exactly 5 halves.</summary>
        public bool FinishedAtPenalties
        { get { return this.Halves.Count == 5; } }

        /// <summary>Determines whether the match was played. More specifically, it tests if any of the FinishedInRegularTime, FinishedInExtraTime or FinishedAtPenalties properties return true.</summary>
        public bool Played
        { get { return this.FinishedInRegularTime || this.FinishedInExtraTime || this.FinishedAtPenalties; } }

        /// <summary>Formats the full score to make it look nice and clear.</summary>
        /// <param name="includePenaltiesIfAny">if set to true, an asterix will be included on the side of the winning team (left for home, or right for away) if the match has reached the penalty shootout</param>
        public string FormatScore(bool includePenaltiesIfAny)
        {
            if (!this.Played)
                return "-";
            if (includePenaltiesIfAny)
                return this.FullScore.FormatHalfScore;
            StringBuilder sb = new StringBuilder(this.FinalScoreWithoutPenalties.FormatHalfScore);
            if (this.FinishedAtPenalties)
            {
                if (this.FullScore.HomeWin)
                    sb.Insert(0, '*');
                if (this.FullScore.AwayWin)
                    sb.Append('*');
            }
            return sb.ToString();
        }

        /// <summary>Returns a text description of the result of the match.</summary>
        /// <param name="includePenaltiesIfAny">if set to true, the fifth penalty shootout score will be included if the match has reached the penalty shootout</param>
        public string ScoreDescription(bool includePenaltiesIfAny)
        {
            if (!this.Played)
                return "";
            StringBuilder sb = new StringBuilder();
            for (int iHalf = 0; iHalf < (includePenaltiesIfAny ? this.Halves.Count : (int) Math.Min(4, this.Halves.Count)); iHalf++)
                sb.Append(this.Halves[iHalf].FormatHalfScore).Append(", ");
            if (!this.FinishedAtPenalties)
                return (this.FinishedInRegularTime ? "finished in regular time" : "finished in extra time") + "\n" + sb.ToString().Substring(0, sb.Length - 2);
            else
                return "finished at penalties\n" + string.Format("(score {0}-{1})", this.Halves[4].Home, this.Halves[4].Away) + "\n" + sb.ToString().Substring(0, sb.Length - 2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(this.FullScore.FormatHalfScore);
            sb.Append(" (").Append(this.Halves.Count).Append(" halves");
            if (this.Halves.Count > 0)
            {
                sb.Append(": ");
                foreach (HalfScoreboard half in this.Halves)
                    sb.Append(half.FormatHalfScore).Append(',');
                sb = sb.Remove(sb.Length - 1, 1);
            }
            return sb.Append(')').ToString();
        }
    }

    /// <summary>
    /// Defines information related to a match.
    /// </summary>
    public class Match : ObjectWithID
    {
        /// <summary>Gets or privately sets the category of the match. It consists of a string code, that can be formatted using the FormatCategory property.</summary>
        public string Category { get; private set; }
        /// <summary>Gets or privately sets the string team reference pair for the match. It can consist in team IDs, group positions or match IDs.</summary>
        public PairT<string> TeamReferences { get; private set; }
        /// <summary>Gets or sets the teams of the match. Each item is null initially or if participating teams are not yet known, they are set in the 'calculate' methods of the Database class.</summary>
        public PairT<Team> Teams { get; private set; }
        /// <summary>Gets or privately sets the date and time when the match is played.</summary>
        public DateTime When { get; private set; }
        /// <summary>Gets or privately sets the venue where the match is played.</summary>
        public Venue Where { get; private set; }
        /// <summary>Gets or privately sets the scoreboard of the match.</summary>
        public MatchScoreboard Scoreboard { get; internal set; }

        /// <summary>Constructs a new Match object from the given parameters.</summary>
        /// <param name="id">the ID of the match</param>
        /// <param name="category">the category of the match</param>
        /// <param name="teamReferences">the team reference pair for the match</param>
        /// <param name="when">the date and time when the match is played</param>
        /// <param name="where">the venue where the match is played</param>
        /// <param name="scoreboard">the scoreboard of the match</param>
        public Match(string id, string category, PairT<string> teamReferences, DateTime when, Venue where, MatchScoreboard scoreboard)
            : base(id)
        {
            this.Category = category;
            this.TeamReferences = teamReferences;
            this.Teams = new PairT<Team>(null, null);
            this.When = when;
            this.Where = where;
            this.Scoreboard = scoreboard;
        }

        /// <summary>Determines whether this match is a group match (based on its category string).</summary>
        public bool IsGroupMatch
        { get { return this.Category[0] == 'G'; } }

        /// <summary>Formats the category of the match to make it look nice and clear.</summary>
        public string FormatCategory
        { get { return Utils.FormatMatchCategory(this.Category); } }

        /// <summary>Parses the given XmlNode into a new Match object.</summary>
        public static Match Parse(XmlNode node, Database database)
        {
            string id = node.Attributes["ID"].Value;
            string category = node.Attributes["category"].Value;
            string[] whoParts = node.Attributes["who"].Value.Split(',');
            PairT<string> teamReferences = new PairT<string>(whoParts[0], whoParts[1]);
            string whenStr = node.Attributes["when"].Value;
            DateTime when = new DateTime(2016,
                Int32.Parse(whenStr.Substring(whenStr.IndexOf('/') + 1, whenStr.IndexOf('@') - whenStr.IndexOf('/') - 1)),
                Int32.Parse(whenStr.Substring(0, whenStr.IndexOf('/'))),
                Int32.Parse(whenStr.Substring(whenStr.IndexOf('@') + 1)), 0, 0);
            Venue where = database.Venues.GetItemByID(node.Attributes["where"].Value);
            List<HalfScoreboard> halves = new List<HalfScoreboard>();
            string scoreboardStr = node.Attributes["scoreboard"].Value;
            if (scoreboardStr.Trim().Length > 0)
            {
                string[] scoreParts = scoreboardStr.Split(',');
                foreach (string score in scoreParts)
                    halves.Add(HalfScoreboard.Parse(score));
            }
            MatchScoreboard scoreboard = new MatchScoreboard(halves);
            return new Match(id, category, teamReferences, when, where, scoreboard);
        }

        /// <summary>Generates an XmlNode from this object.</summary>
        /// <param name="doc">the XmlDocument used to create the element</param>
        /// <param name="name">the name of the XmlNode to be created</param>
        public new XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = base.ToXml(doc, name);
            node.AddAttribute(doc, "category", this.Category);
            node.AddAttribute(doc, "who", string.Format("{0},{1}", this.TeamReferences.Home, this.TeamReferences.Away));
            node.AddAttribute(doc, "when", string.Format("{0}/{1}@{2}", this.When.Day, this.When.Month, this.When.Hour));
            node.AddAttribute(doc, "where", this.Where.ID);
            List<string> scores = new List<string>();
            foreach (HalfScoreboard score in this.Scoreboard.Halves)
                scores.Add(score.Home + "-" + score.Away);
            node.AddAttribute(doc, "scoreboard", scores.GetListString(","));
            return node;
        }

        public override string ToString()
        {
            return string.Format("ID={0}, category={1}, references={2}-{3}, scoreboard={4}",
                this.ID, this.Category, this.TeamReferences.Home, this.TeamReferences.Away, this.Scoreboard.ToString());
        }
    }
}
