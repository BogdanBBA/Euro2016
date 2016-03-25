using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        public string ID { get; private set; }

        public ObjectWithID(string id)
        {
            this.ID = id;
        }

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
        public string Name { get; private set; }
        public string City { get; private set; }
        public PointF Location { get; private set; }
        public int YearOpened { get; private set; }
        public int Capacity { get; private set; }

        public Venue(string id, string name, string city, PointF location, int yearOpened, int capacity)
            : base(id)
        {
            this.Name = name;
            this.City = city;
            this.Location = location;
            this.YearOpened = yearOpened;
            this.Capacity = capacity;
        }

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
    /// Defines information related to a country.
    /// </summary>
    public class Country : ObjectWithID
    {
        public PairT<string> Names { get; private set; }
        public Bitmap FlagOriginal { get; private set; }
        public Bitmap Flag100px { get; private set; }
        public Bitmap Flag20px { get; private set; }

        public Country(string id, PairT<string> names, Bitmap flagOriginal)
            : base(id)
        {
            this.Names = names;
            this.FlagOriginal = flagOriginal;
            this.Flag100px = (Bitmap) Utils.ScaleImage(this.FlagOriginal, 160, 100, InterpolationMode.HighQualityBicubic, false);
            this.Flag20px = (Bitmap) Utils.ScaleImage(this.FlagOriginal, 32, 20, InterpolationMode.HighQualityBicubic, false);
        }

        public static Country Parse(XmlNode node)
        {
            string id = node.Attributes["ID"].Value;
            string[] names = node.Attributes["names"].Value.Split('|');
            Bitmap flag = new Bitmap(Paths.FlagsFolder + id + ".png");
            return new Country(id, new PairT<string>(names[0], names[1]), flag);
        }

        public new XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = base.ToXml(doc, name);
            node.AddAttribute(doc, "names", this.Names.Home + "|" + this.Names.Away);
            return node;
        }

        public override string ToString()
        {
            return base.ToString() + ". " + this.Names;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Team
    {
        public Country Country { get; private set; }
        public List<string> Nicknames { get; private set; }

        public Team(Country country, List<string> nicknames)
        {
            this.Country = country;
            this.Nicknames = nicknames;
        }

        public static Team Parse(XmlNode node, ListOfIDObjects<Country> countries)
        {
            Country country = countries.GetItemByID(node.Attributes["countryID"].Value);
            List<string> nicknames = node.Attributes["nicknames"].Value.Split(';').ToList();
            return new Team(country, nicknames);
        }

        public XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = doc.CreateElement(name);
            node.AddAttribute(doc, "countryID", this.Country.ID);
            node.AddAttribute(doc, "nicknames", this.Nicknames.GetListString(";"));
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
            return base.ToString();
        }
    }

    public class TableLine
    {
        public Team Team { get; private set; }
        public int Position { get; set; }
        public int MatchesPlayed { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }

        public TableLine(Team team)
        {
            this.Team = team;
            this.Reset();
        }

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

        public void Reset()
        {
            this.Position = 1;
            this.MatchesPlayed = 0;
            this.Won = this.Drawn = this.Lost = 0;
            this.GoalsFor = this.GoalsAgainst = 0;
            this.GoalDifference = 0;
            this.Points = 0;
        }

        public void AddMatchResult(Match match)
        {
            if (!match.Scoreboard.Played)
                return;
            if (this.Team.Equals(match.Teams.Home)) // this team is home
            {
                switch (match.Scoreboard.FinalScore.WhichTeamWon)
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
                this.GoalsFor += match.Scoreboard.FinalScore.Home;
                this.GoalsAgainst += match.Scoreboard.FinalScore.Away;
            }
            else if (this.Team.Equals(match.Teams.Away))
            {
                switch (match.Scoreboard.FinalScore.WhichTeamWon) // this team is away
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
                this.GoalsFor += match.Scoreboard.FinalScore.Away;
                this.GoalsAgainst += match.Scoreboard.FinalScore.Home;
            }
            else
                return;
            this.MatchesPlayed = this.Won + this.Drawn + this.Lost;
            this.GoalDifference = this.GoalsFor - this.GoalsAgainst;
            this.Points = 3 * this.Won + this.Drawn;
        }

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

    public class Group : ObjectWithID
    {
        public string Name { get; private set; }
        public List<TableLine> TableLines { get; private set; }

        public Group(string id, string name, List<TableLine> tableLines)
            : base(id)
        {
            this.Name = name;
            this.TableLines = tableLines;
        }

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

        public void SortTableLines(bool thirdPlacedTeamGroup, List<Match> matches)
        {
            if (!thirdPlacedTeamGroup)
                for (int iIndex = 0; iIndex < this.TableLines.Count - 1; iIndex++)
                    for (int jIndex = iIndex + 1; jIndex < this.TableLines.Count; jIndex++)
                    {
                        TableLine iT = this.TableLines[iIndex], jT = this.TableLines[jIndex];
                        bool needToSwap = false;

                        if (iT.Points < jT.Points)
                            needToSwap = true;
                        else if (iT.Points == jT.Points)
                        {
                            int whoWonDirectMatch = matches.WhoWonGroupMatchBetween(iT.Team, jT.Team);
                            if (whoWonDirectMatch == 1)
                                needToSwap = true;
                            else if (whoWonDirectMatch == 0 || whoWonDirectMatch == -2)
                            {
                                if (iT.GoalDifference < jT.GoalDifference)
                                    needToSwap = true;
                            }
                        }

                        if (needToSwap)
                            this.TableLines.SwapItemsAtPositions(iIndex, jIndex);
                    }
            else
                for (int iIndex = 0; iIndex < this.TableLines.Count - 1; iIndex++)
                    for (int jIndex = iIndex + 1; jIndex < this.TableLines.Count; jIndex++)
                    {
                        TableLine iT = this.TableLines[iIndex], jT = this.TableLines[jIndex];
                        bool needToSwap = false;

                        if (iT.Points < jT.Points)
                            needToSwap = true;
                        if (iT.Points == jT.Points)
                        {
                            if (iT.GoalDifference < jT.GoalDifference)
                                needToSwap = true;
                            else if (iT.GoalDifference == jT.GoalDifference)
                            {
                                if (iT.GoalsFor < jT.GoalsFor)
                                    needToSwap = true;
                            }
                        }

                        if (needToSwap)
                            this.TableLines.SwapItemsAtPositions(iIndex, jIndex);
                    }
            for (int index = 0; index < this.TableLines.Count; index++)
                this.TableLines[index].Position = index + 1;
        }

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

        public new XmlNode ToXml(XmlDocument doc, string name)
        {
            XmlNode node = base.ToXml(doc, name);
            node.AddAttribute(doc, "name", this.Name);
            List<string> countryIDs = new List<string>();
            foreach (TableLine line in this.TableLines)
                countryIDs.Add(line.Team.Country.ID);
            node.AddAttribute(doc, "teams", countryIDs.GetListString(","));
            return node;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1} teams)", this.Name, this.TableLines.Count);
        }
    }

    public class HalfScoreboard : PairT<int>
    {
        public HalfScoreboard()
            : this(0, 0)
        {
        }

        public HalfScoreboard(int home, int away)
            : base(home, away)
        {
        }

        public bool HomeWin
        { get { return this.Home > this.Away; } }

        public bool Tie
        { get { return this.Home == this.Away; } }

        public bool AwayWin
        { get { return this.Home < this.Away; } }

        public int WhichTeamWon
        { get { return this.Tie ? 0 : (this.HomeWin ? -1 : 1); } }

        public void AddHalfScoreboard(HalfScoreboard scoreboard)
        {
            this.Home += scoreboard.Home;
            this.Away += scoreboard.Away;
        }

        public void Reset()
        {
            this.Home = this.Away = 0;
        }

        public string FormatHalfScore
        {
            get { return this.Home + "-" + this.Away; }
        }
    }

    public class MatchScoreboard
    {
        public List<HalfScoreboard> Halves { get; private set; }
        public HalfScoreboard FinalScore { get; private set; }

        public MatchScoreboard(List<HalfScoreboard> halves)
        {
            this.Halves = new List<HalfScoreboard>();
            this.FinalScore = new HalfScoreboard();
            this.SetHalves(halves);
        }

        public void SetHalves(List<HalfScoreboard> halves)
        {
            this.Halves.Clear();
            this.FinalScore.Reset();
            foreach (HalfScoreboard half in halves)
            {
                this.Halves.Add(half);
                this.FinalScore.AddHalfScoreboard(half);
            }
        }

        public bool Played
        {
            get
            {
                return this.Halves.Count == 2 || this.Halves.Count == 4 || this.Halves.Count == 5;
            }
        }

        public string FormatScore
        {
            get
            {
                if (!this.Played)
                    return "-";
                string score = this.FinalScore.FormatHalfScore;
                //if (this.Halves.Count < 5)
                return score;
                //return this.FinalScore.AwayWin ? score + "*" : "*" + score;
            }
        }
    }

    public class Match : ObjectWithID
    {
        public string Category { get; private set; }
        public PairT<string> TeamReferences { get; private set; }
        public PairT<Team> Teams { get; private set; }
        public DateTime When { get; private set; }
        public Venue Where { get; private set; }
        public MatchScoreboard Scoreboard { get; internal set; }

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

        public bool IsGroupMatch
        { get { return this.Category[0] == 'G'; } }

        public string FormatCategory
        {
            get
            {
                string[] parts = this.Category.Split(':');
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
        }

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
                {
                    string[] goals = score.Split('-');
                    halves.Add(new HalfScoreboard(Int32.Parse(goals[0]), Int32.Parse(goals[1])));
                }
            }
            MatchScoreboard scoreboard = new MatchScoreboard(halves);
            return new Match(id, category, teamReferences, when, where, scoreboard);
        }

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
    }
}
