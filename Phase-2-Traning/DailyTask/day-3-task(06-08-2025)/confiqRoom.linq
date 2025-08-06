<Query Kind="Statements">
  <Connection>
    <ID>b3b5193c-2fcd-4be0-90b3-4bb55edfdc77</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <AttachFileName>D:\NewDataBase\NewDataBase.db</AttachFileName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.Sqlite</EFProvider>
    </DriverData>
  </Connection>
</Query>


var conferenceRooms = new List<ConferenceRoom>
{
    new ConferenceRoom { ID = 1, RoomName = "Cypress", SeatingCapacity = 20 },
    new ConferenceRoom { ID = 2, RoomName = "Eucalyptus", SeatingCapacity = 15 },
    new ConferenceRoom { ID = 3, RoomName = "Lavender", SeatingCapacity = 25 }
};


var trees = new List<Tree>
{
    new Tree { ID = 1, TreeName = "Banayan", Details = "Large, deciduous tree" },
    new Tree { ID = 2, TreeName = "Pine", Details = "Evergreen conifer" },
    new Tree { ID = 3, TreeName = "Coconut", Details = "Known for maple syrup" }
};


conferenceRooms.Dump("ConferenceRoom Table");

// Show Tree table
trees.Dump("Tree Table");

// Perform join on ID
var joined = conferenceRooms.Join(
    trees,
    conf => conf.ID,
    tree => tree.ID,
    (conf, tree) => new 
    {
        conf.RoomName,
        conf.SeatingCapacity,
        tree.TreeName,
        tree.Details
    }
);


joined.Dump("Joined ConferenceRoom + Tree");
public class ConferenceRoom
{
    public int ID { get; set; }
    public string RoomName { get; set; }
    public int SeatingCapacity { get; set; }
}

public class Tree
{
    public int ID { get; set; }
    public string TreeName { get; set; }
    public string Details { get; set; }
}
