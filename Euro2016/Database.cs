using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Euro2016
{
    /// <summary>
    /// Defines information related to user settings. Member property names are sufficiently explicit.
    /// </summary>
    public class Settings
    {
        public Team FavoriteTeam { get; set; }
        public bool ShowCountryNamesInNativeLanguage { get; set; }
        public bool ShowKnockoutStageOnStartup { get; set; }
        public bool SpamWithWinnerOnStartup { get; set; }
        public bool ShowFlagsOnMap { get; set; }
        public double TimeOffset { get; set; }

        /// <summary>Parses the settings from a XmlNode object.</summary>
        /// <param name="teams">the reference to the full list of teams participating in the competition</param>
        /// <returns>an empty string if execution ended successfully, or the error description otherwise</returns>
        public string ReadSettings(XmlNode settingsNode, List<Team> teams)
        {
            try
            {
                try { this.FavoriteTeam = teams.First(t => t.Country.ID.Equals(settingsNode.SelectSingleNode("FavoriteTeam").Attributes["value"].Value)); }
                catch (Exception) { this.FavoriteTeam = teams.First(); }

                try { this.ShowCountryNamesInNativeLanguage = bool.Parse(settingsNode.SelectSingleNode("ShowCountryNamesInNativeLanguage").Attributes["value"].Value); }
                catch (Exception) { this.ShowCountryNamesInNativeLanguage = false; }

                try { this.ShowKnockoutStageOnStartup = bool.Parse(settingsNode.SelectSingleNode("ShowKnockoutStageOnStartup").Attributes["value"].Value); }
                catch (Exception) { this.ShowKnockoutStageOnStartup = true; }

                try { this.SpamWithWinnerOnStartup = bool.Parse(settingsNode.SelectSingleNode("SpamWithWinnerOnStartup").Attributes["value"].Value); }
                catch (Exception) { this.SpamWithWinnerOnStartup = true; }

                try { this.ShowFlagsOnMap = bool.Parse(settingsNode.SelectSingleNode("ShowFlagsOnMap").Attributes["value"].Value); }
                catch (Exception) { this.ShowFlagsOnMap = true; }

                try { this.TimeOffset = Int32.Parse(settingsNode.SelectSingleNode("TimeOffset").Attributes["value"].Value); }
                catch (Exception) { this.TimeOffset = 0.0; }

                return "";
            }
            catch (Exception E) { return E.ToString(); }
        }

        /// <summary>Generates a XmlNode object from the settings.</summary>
        /// <param name="doc">the XmlDocument object that will create the XML elements</param>
        /// <param name="nodeName">the name of the resulting XML node</param>
        /// <returns>an empty string if execution ended successfully, or the error description otherwise</returns>
        public string ToXml(XmlDocument doc, string nodeName, out XmlNode resultNode)
        {
            try
            {
                XmlNode result = doc.CreateElement(nodeName), node;

                node = result.AppendChild(doc.CreateElement("FavoriteTeam"));
                node.AddAttribute(doc, "value", this.FavoriteTeam.Country.ID);

                node = result.AppendChild(doc.CreateElement("ShowCountryNamesInNativeLanguage"));
                node.AddAttribute(doc, "value", this.ShowCountryNamesInNativeLanguage.ToString());

                node = result.AppendChild(doc.CreateElement("ShowKnockoutStageOnStartup"));
                node.AddAttribute(doc, "value", this.ShowKnockoutStageOnStartup.ToString());

                node = result.AppendChild(doc.CreateElement("SpamWithWinnerOnStartup"));
                node.AddAttribute(doc, "value", this.SpamWithWinnerOnStartup.ToString());

                node = result.AppendChild(doc.CreateElement("ShowFlagsOnMap"));
                node.AddAttribute(doc, "value", this.ShowFlagsOnMap.ToString());

                node = result.AppendChild(doc.CreateElement("TimeOffset"));
                node.AddAttribute(doc, "value", this.TimeOffset.ToString());

                resultNode = result;
                return "";
            }
            catch (Exception E) { resultNode = null; return E.ToString(); }
        }
    }

    /// <summary>
    /// Defines the top-level data for the application. Contains the centralized lists of various data types, read and write methods, and utility methods.
    /// </summary>
    public class Database
    {
        /// <summary>The identifier for any third-placed teams group in the team references of a match.</summary>
        public const string ThirdPlacedTeamsID = "T";

        /// <summary>Gets or privately sets the list of venues involved at Euro 2016.</summary>
        public ListOfIDObjects<Venue> Venues { get; private set; }
        /// <summary>Gets or privately sets the list of all countries used within the application.</summary>
        public ListOfIDObjects<Country> Countries { get; private set; }
        /// <summary>Gets or privately sets the list of clubs used within the application.</summary>
        public ListOfIDObjects<Club> Clubs { get; private set; }
        /// <summary>Gets or privately sets the list of players used within the application.</summary>
        public ListOfIDObjects<Player> Players { get; private set; }
        /// <summary>Gets or privately sets the list of teams involved at Euro 2016.</summary>
        public List<Team> Teams { get; private set; }
        /// <summary>Gets or privately sets the list of ranking groups of Euro 2016.</summary>
        public ListOfIDObjects<Group> Groups { get; private set; }
        /// <summary>Gets or privately sets the group for the third-placed teams of Euro 2016.</summary>
        public Group ThirdPlacedTeamsGroup { get; private set; }
        /// <summary>Gets or privately sets the list of matches of Euro 2016.</summary>
        public ListOfIDObjects<Match> Matches { get; private set; }
        /// <summary>Gets or privately sets the user settings for the application.</summary>
        public Settings Settings { get; private set; }

        /// <summary>Constructs an empty Database object.</summary>
        public Database()
        {
            this.Venues = new ListOfIDObjects<Venue>();
            this.Countries = new ListOfIDObjects<Country>();
            this.Clubs = new ListOfIDObjects<Club>();
            this.Players = new ListOfIDObjects<Player>();
            this.Teams = new List<Team>();
            this.Groups = new ListOfIDObjects<Group>();
            this.ThirdPlacedTeamsGroup = new Group(Database.ThirdPlacedTeamsID, "Third-placed teams", new List<TableLine>());
            this.Matches = new ListOfIDObjects<Match>();
            this.Settings = new Settings();
        }

        /// <summary>Loads a Database object using data from the database file.</summary>
        /// <param name="mainDatabaseFilePath">the path to the main part of the database file</param>
        /// <param name="playerDatabaseFilePath">the path to the player part of the database file</param>
        /// <returns>an empty string if execution ended successfully, or the error description otherwise</returns>
        public string LoadDatabase(string mainDatabaseFilePath, string playerDatabaseFilePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mainDatabaseFilePath);

                XmlNodeList nodes = doc.SelectNodes("DATABASE/VENUES/Venue");
                foreach (XmlNode node in nodes)
                    this.Venues.Add(Venue.Parse(node));

                nodes = doc.SelectNodes("DATABASE/COUNTRIES/Country");
                foreach (XmlNode node in nodes)
                    this.Countries.Add(Country.Parse(node));

                nodes = doc.SelectNodes("DATABASE/TEAMS/Team");
                foreach (XmlNode node in nodes)
                    this.Teams.Add(Team.Parse(node, this.Countries));

                nodes = doc.SelectNodes("DATABASE/GROUPS/Group");
                foreach (XmlNode node in nodes)
                    this.Groups.Add(Group.Parse(node, this.Teams));

                nodes = doc.SelectNodes("DATABASE/MATCHES/Match");
                foreach (XmlNode node in nodes)
                    this.Matches.Add(Match.Parse(node, this));

                this.Settings.ReadSettings(doc.SelectSingleNode("DATABASE/SETTINGS"), this.Teams);

                doc = new XmlDocument();
                doc.Load(playerDatabaseFilePath);

                nodes = doc.SelectNodes("PLAYER_DATABASE/CLUBS/Club");
                foreach (XmlNode node in nodes)
                    this.Clubs.Add(Club.Parse(node, this.Countries));

                nodes = doc.SelectNodes("PLAYER_DATABASE/PLAYERS/Player");
                foreach (XmlNode node in nodes)
                {
                    Player player = Player.Parse(node, this.Teams, this.Clubs);
                    player.Nationality.Players.Add(player);
                    this.Players.Add(player);
                }

                this.Matches.SetTimeOffset(this.Settings.TimeOffset);
                this.Calculate(true, true, true);

                return "";
            }
            catch (Exception E)
            { return E.ToString(); }
        }

        /// <summary>Saves the database to file.</summary>
        /// <param name="mainDatabaseFilePath">the path to the main part of the database file</param>
        /// <param name="playerDatabaseFilePath">the path to the player part of the database file. If null (or not passed), this part will not be written</param>
        /// <returns>an empty string if execution ended successfully, or the error description otherwise</returns>
        public string SaveDatabase(string mainDatabaseFilePath, string playerDatabaseFilePath = null)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.AppendChild(doc.CreateElement("DATABASE"));
                root.AddAttribute(doc, "lastSaved", DateTime.Now.ToString("dddd, d MMMM yyyy, HH:mm:ss"));

                XmlNode node = root.AppendChild(doc.CreateElement("VENUES"));
                foreach (Venue venue in this.Venues)
                    node.AppendChild(venue.ToXml(doc, "Venue"));

                node = root.AppendChild(doc.CreateElement("COUNTRIES"));
                foreach (Country country in this.Countries)
                    node.AppendChild(country.ToXml(doc, "Country"));

                node = root.AppendChild(doc.CreateElement("TEAMS"));
                foreach (Team team in this.Teams)
                    node.AppendChild(team.ToXml(doc, "Team"));

                node = root.AppendChild(doc.CreateElement("GROUPS"));
                foreach (Group group in this.Groups)
                    node.AppendChild(group.ToXml(doc, "Group"));

                node = root.AppendChild(doc.CreateElement("MATCHES"));
                foreach (Match match in this.Matches)
                    node.AppendChild(match.ToXml(doc, "Match"));

                XmlNode settingsNode;
                this.Settings.ToXml(doc, "SETTINGS", out settingsNode);
                root.AppendChild(settingsNode);

                doc.Save(mainDatabaseFilePath);

                if (playerDatabaseFilePath != null)
                {
                    doc = new XmlDocument();
                    root = doc.AppendChild(doc.CreateElement("PLAYER_DATABASE"));
                    root.AddAttribute(doc, "lastSaved", DateTime.Now.ToString("dddd, d MMMM yyyy, HH:mm:ss"));

                    node = root.AppendChild(doc.CreateElement("CLUBS"));
                    foreach (Club club in this.Clubs)
                        node.AppendChild(club.ToXml(doc, "Club"));

                    node = root.AppendChild(doc.CreateElement("PLAYERS"));
                    foreach (Player player in this.Players)
                        node.AppendChild(player.ToXml(doc, "Player"));

                    doc.Save(playerDatabaseFilePath);
                }

                return "";
            }
            catch (Exception E)
            { return E.ToString(); }
        }

        /// <summary>Utility method. Calls the respective 'calculate' methods for the parameter values that are set to true</summary>
        public void Calculate(bool groupMatchTeams, bool groups = false, bool knockoutMatches = false)
        {
            if (groupMatchTeams)
                this.CalculateGroupMatchTeams();
            if (groups)
            {
                this.CalculateGroups();
                this.CalculateThirdPlacedTeamsGroup();
            }
            if (knockoutMatches)
                this.CalculateKnockoutMatches();
        }

        /// <summary>Calculates the group matches' team references. Should normally only be called once during execution.</summary>
        public void CalculateGroupMatchTeams()
        {
            foreach (Match match in this.Matches)
                if (match.IsGroupMatch)
                {
                    match.Teams.Home = this.Teams.First(t => t.Country.ID.Equals(match.TeamReferences.Home));
                    match.Teams.Away = this.Teams.First(t => t.Country.ID.Equals(match.TeamReferences.Away));
                }
        }

        /// <summary>Calculates the group rankings (resets rankings, adds all valid match results, then sorts).</summary>
        public void CalculateGroups()
        {
            foreach (Group group in this.Groups)
            {
                foreach (TableLine line in group.TableLines)
                {
                    line.Reset();
                    foreach (Match match in this.Matches.GetMatchesBy(group))
                        line.AddGroupMatchResult(match);
                }
                group.SortTableLines(false, this.Matches);
            }
        }

        /// <summary>Calculates the group rankings (resets rankings, adds all valid match results, then sorts) for the third-placed teams.</summary>
        public void CalculateThirdPlacedTeamsGroup()
        {
            this.ThirdPlacedTeamsGroup.TableLines.Clear();
            foreach (Group group in this.Groups)
                this.ThirdPlacedTeamsGroup.TableLines.Add(new TableLine(group.TableLines[2]));
            this.ThirdPlacedTeamsGroup.SortTableLines(true, this.Matches);
        }

        /// <summary>Parses a team reference (in the format 'refGroup:refTeam'; for example, "B:1" or "T:A/C/D") and, if valid, returns the correctly corresponding Team object, or null otherwise.
        /// Note: the parameter 'firstTeam' is necessary only for eight-finals involving third-placed teams (normally at Euro 2016, the first team is the winner of one of groups A-D, and the second is the third-placed team).</summary>
        private Team ParseTeamReference(string reference, string firstTeam = null)
        {
            if (reference.Contains(':')) // table line reference
            {
                string refGroup = reference.Split(':')[0], refTeam = reference.Split(':')[1];

                if (!refGroup.Equals(Database.ThirdPlacedTeamsID)) // normal group
                {
                    Group group = this.Groups.GetItemByID(refGroup);
                    if (group != null && group.AllMatchesPlayed)
                        return group.TableLines[Int32.Parse(refTeam) - 1].Team;
                    return null;
                }
                else // third-place group (also, certainly an eight-final - but can be eighth-final between two non-third-placed teams)
                {
                    string qualifiedTeamsGroups = "", resultReference = "";
                    for (char groupID = 'A'; groupID <= 'F'; groupID++)
                        if (this.ThirdPlacedTeamsGroup.GetTableLineForTeam(this.Groups.GetItemByID(groupID.ToString()).TableLines[2].Team).Position <= 4)
                            qualifiedTeamsGroups += groupID;
                    switch (firstTeam)
                    {
                        case "A:1":
                            if (new string[] { "ABCD", "ABCE", "ABCF", "ACDE", "ACDF", "ACEF", "BCDE", "BCDF", "CDEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "C:3";
                            if (new string[] { "ABDE", "ABDF", "ADEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "D:3";
                            if (new string[] { "ABEF", "BCEF", "BDEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "E:3";
                            break;
                        case "B:1":
                            if (new string[] { "ABCE", "ABCF", "ABDE", "ABDF", "ABEF", "ACEF", "ADEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "A:3";
                            if (new string[] { "BCEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "C:3";
                            if (new string[] { "ABCD", "ACDE", "ACDF", "BCDE", "BCDF", "BDEF", "CDEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "D:3";
                            break;
                        case "C:1":
                            if (new string[] { "ABCD", "ACDE", "ACDF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "A:3";
                            if (new string[] { "ABCE", "ABCF", "ABDE", "ABDF", "ABEF", "BCDE", "BCDF", "BCEF", "BDEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "B:3";
                            if (new string[] { "ACEF", "ADEF", "CDEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "F:3";
                            break;
                        case "D:1":
                            if (new string[] { "ABCD" }.Contains(qualifiedTeamsGroups))
                                resultReference = "B:3";
                            if (new string[] { "ABCE", "ABDE", "ACDE", "ACEF", "ADEF", "BCDE", "CDEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "E:3";
                            if (new string[] { "ABCF", "ABDF", "ABEF", "ACDF", "BCDF", "BCEF", "BDEF" }.Contains(qualifiedTeamsGroups))
                                resultReference = "F:3";
                            break;
                    }
                    return resultReference.Equals("") ? null : this.ParseTeamReference(resultReference);
                }
            }
            else // match winner reference
            {
                Match match = this.Matches.GetItemByID(reference);
                if (match.Teams.Home != null & match.Teams.Away != null && match.Scoreboard.Played)
                    return match.Scoreboard.FullScore.HomeWin ? match.Teams.Home : match.Teams.Away;
                return null;
            }
        }

        /// <summary>Calculates the knockout matches' team references.</summary>
        public void CalculateKnockoutMatches()
        {
            ListOfIDObjects<Match> matches = this.Matches.GetMatchesBy("KO");
            foreach (Match match in matches)
            {
                match.Teams.Home = null;
                match.Teams.Away = null;
            }
            foreach (Match match in matches)
            {
                match.Teams.Home = this.ParseTeamReference(match.TeamReferences.Home);
                match.Teams.Away = this.ParseTeamReference(match.TeamReferences.Away, match.TeamReferences.Home);
            }
        }

        /// <summary>Determines the best result so far in the competition of the given team. More specifically, it returns the category of the last (chronologically-sorted) match involving the given team.</summary>
        public string TournamentResultOfTeam(Team team)
        {
            return this.Matches.GetMatchesBy(team).Last().Category;
        }

        /// <summary>Compares the category string of a match to another category string, returning -1, 0 or 1 to mark which is the greater achievement.</summary>
        public int CompareCategoryTo(string categoryA, string categoryB)
        {
            if (categoryA.Equals(categoryB))
                return 0;
            string[] thisParts = categoryA.Split(':'), otherParts = categoryB.Split(':');
            if (thisParts[0].CompareTo(otherParts[0]) != 0)
                return thisParts[0].CompareTo(otherParts[0]) > 0 ? -1 : 1;
            if (thisParts[0].Equals("G"))
                return thisParts[1].CompareTo(otherParts[1]) > 0 ? -1 : 1;
            return thisParts[1].CompareTo(otherParts[1]) > 0 ? 1 : -1;
        }

        /// <summary>Takes the text from https://en.wikipedia.org/wiki/UEFA_Euro_2016_squads (copied in <code>Paths.DatabasePlayersInputFile</code>) and parses the player data. 
        /// Existing clubs and players will be erased. Countries will not be affected at all.</summary>
        internal void ParseDatabasePlayers(string inputFilePath)
        {
            string[] lines = File.ReadAllLines(inputFilePath);
            Team lastTeam = null;
            string managerName = null;

            this.Clubs.Clear();
            this.Players.Clear();

            for (int iLine = 0; iLine < lines.Length; iLine++)
            {
                string line = lines[iLine].Replace("  ", " ").Replace(" \t", "\t").Replace("Republic of", "");

                // exit cases
                if (line.Trim().Equals("") || line.Contains("Group ") || line.StartsWith("#"))
                    continue;

                // manager
                if (line.StartsWith("Manager"))
                {
                    managerName = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                    Country managerCountry = lastTeam.Country;
                    foreach (Country country in this.Countries)
                        if (managerName.StartsWith(country.Names.Away))
                        {
                            managerCountry = country;
                            managerName = managerName.Replace(country.Names.Away, "").Trim();
                        }
                    lastTeam.Coach = new KeyValuePair<Country, string>(managerCountry, managerName);
                    continue;
                }

                // new country
                Team isTeam = this.Teams.FirstOrDefault(t => t.Country.Names.Away.Equals(line.Trim()));
                if (isTeam != null)
                {
                    lastTeam = isTeam;
                    continue;
                }

                // player row
                if (line[0] >= '1' && line[0] <= '9')
                {
                    string[] cols = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    string id = this.Players.GetUniqueNumericID(1, 1, 3);
                    string name = cols[2].Replace("(captain)", "(C)").Trim();
                    int number = Int32.Parse(cols[0].Trim());
                    Player.PlayingPosition position = (Player.PlayingPosition) Enum.Parse(typeof(Player.PlayingPosition), cols[1].Trim());
                    DateTime birth = DateTime.Parse(cols[3].Substring(0, cols[3].IndexOf('(')).Trim());
                    Tuple<int, int> age = Utils.CalculateAgeInYearsAndDays(new DateTime(2016, 6, 10), birth);
                    int caps = Int32.Parse(cols[4].Trim());
                    int goals = Int32.Parse(cols[5].Trim());
                    Club club = this.GetClub(cols[6].Trim());
                    Player player = new Player(id, name, number, position, birth, age, caps, goals, lastTeam, club);
                    this.Players.Add(player);
                    continue;
                }
            }

            string saveResult = this.SaveDatabase(Paths.DatabaseFile, Paths.DatabasePlayersFile);
            if (!saveResult.Equals(string.Empty))
                MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal Club GetClub(string s)
        {
            // it exists
            foreach (Club club in this.Clubs)
                if (s.Equals(club.Country.Names.Away + " " + club.Name))
                    return club;

            // make new
            Country clubCountry = null;
            foreach (Country country in this.Countries)
                if (s.StartsWith(country.Names.Away))
                { clubCountry = country; break; }
            if (clubCountry == null)
                throw new ApplicationException("country in '" + s + "' does not exist");
            else
            {
                Club club = new Club(this.Clubs.GetUniqueNumericID(1, 1, 3), s.Replace(clubCountry.Names.Away, "").Trim(), clubCountry);
                this.Clubs.Add(club);
                return club;
            }
        }
    }
}
