namespace Web.Data;

public class QSetInitCurrent
{
    public class Nike
    {
        public List<QInit> LodgeSecurity = new List<QInit> 
        {
            new QInit(1, 0, 0, 24, 0, "Are all Security Officers wearing full uniform with their name badge?"),
            new QInit(2, 12, 10, 5, 9, "Are Security Officers visible and vigilant whilst on duty at their post?"),
            new QInit(3, 21, 18, 15, 10, "Are all Security Officers correctly trained for Nike Stores?"),
            new QInit(4, 7, 7, 7, 6, "Are all radios, batteries and chargers in working order?"),
            new QInit(5, 25, 10, 8, 5, "Is the security team making regular shoplifting preventions?"),
            new QInit(6, 3, 8, 10, 3, "Has the store been issued with relevant registers (OB Book, Visitors, Staff Purchases)?"),
            new QInit(7, 4, 4, 4, 4, "Are the Security Officers randomly polygraph tested every 6 months?"),
            new QInit(8, 10, 6, 0, 0, "Is the Security Officer present at the front door?"),
            new QInit(9, 0, 4, 14, 0, "Is the visitors register used and kept up-to-date at the staff entrance?"),
            new QInit(10, 15, 15, 15, 15, "Are security meetings held with the management team on a regular basis?"),
            new QInit(11, 33, 5, 10, 0, "Are Lodge Employees extra vigilant during high-risk times (CIT, opening & closing times)? Security posted outside the store at opening and closing times?"),
            new QInit(12, 0, 26, 10, 0, "Are staff purchases checked and recorded by the Security?"),
            new QInit(13, 0, 16, 8, 0, "Are staff items / apparel declared before entering the store - checked by Security?")
        };

        public List<QInit> PhysicalSecurity = new List<QInit>
        {
            new QInit(1, 20, 12, 10, 3, "Is the Alarm System working and used correctly daily?  Is the contact list up-to-date?"),
            new QInit(2, 20, 12, 10, 3, "Does the store have a panic button (connected to armed response, centre security)?"),
            new QInit(3, 5, 15, 10, 6, "Are the back door and the emergency doors alarmed when the store is open and trading?"),
            new QInit(4, 5, 15, 10, 6, "Is the perimeter security effective (check security gates, perimeter doors and fire escape)?"),
            new QInit(5, 14, 14, 10, 10, "Is the CCTV system working and recording correctly?"),
            new QInit(6, 14, 14, 10, 10, "Are all CCTV Cameras and devices working correctly?"),
            new QInit(7, 45, 20, 15, 0, "Is the cash office closed and only accessible by managers?")
        };

        public List<QInit> SalesFloor = new List<QInit>
        {
            new QInit(1, 27, 11, 10, 0, "Are EAS gates (store entrance) working and tested daily?"),
            new QInit(2, 27, 11, 10, 0, "Are EAS tags correctly applied to all relevant high-risk items?"),
            new QInit(3, 0, 0, 36, 0, "Are EAS tags correctly removed / deactivated at the tills?"),
            new QInit(4, 30, 10, 8, 0, "Are blind spots monitored / patrolled by Security & Staff?"),
            new QInit(5, 30, 10, 8, 0, "Are fitting room checks (pilfering) conducted on a regular basis during trading hours?"),
            new QInit(6, 25, 15, 8, 0, "Are pilfered items (tags / empty boxes / hangers) collected daily and recorded?"),
            new QInit(7, 14, 12, 10, 0, "Are tills correctly manned and is the Head Coach or Assistant Coach visible?"),
            new QInit(8, 10, 10, 10, 2, "Is the sales floor neat and tidy (no clutter or boxes left in the aisle)?")
        };

        public List<QInit> Storeroom = new List<QInit>
        {
            new QInit(1, 0, 0, 24, 0, "Are all fire exits and escape routes clear?"),
            new QInit(2, 0, 12, 7, 8, "Is all wastage checked before disposal?"),
            new QInit(3, 0, 25, 14, 25, "Is the receiving area neat and tidy (stock not left out in the open)?"),
            new QInit(4, 0, 14, 20, 30, "Are deliveries double checked against the invoice before being processed?"),
            new QInit(5, 0, 20, 10, 18, "Is the storeroom neat and tidy (no clutter or loose stock left in the storeroom)?"),
            new QInit(6, 0, 20, 10, 6, "Is there any evidence of pilfering in the storeroom?"),
            new QInit(7, 0, 32, 11, 5, "Is the high-risk locker secured (unauthorised access restricted)?")
        };
    }

    public class NikeTest
    {
        public List<QInit> LodgeSecurity = new List<QInit>
        {
            new QInit(1, 0, 0, 24, 0, "Are all Security Officers wearing full uniform with their name badge?"),
            new QInit(13, 0, 16, 8, 0, "Are staff items / apparel declared before entering the store - checked by Security?")
        };

        public List<QInit> PhysicalSecurity = new List<QInit>
        {
            new QInit(1, 20, 12, 10, 3, "Is the Alarm System working and used correctly daily?  Is the contact list up-to-date?"),
            new QInit(7, 45, 20, 15, 0, "Is the cash office closed and only accessible by managers?")
        };

        public List<QInit> SalesFloor = new List<QInit>
        {
            new QInit(1, 27, 11, 10, 0, "Are EAS gates (store entrance) working and tested daily?"),
            new QInit(8, 10, 10, 10, 2, "Is the sales floor neat and tidy (no clutter or boxes left in the aisle)?")
        };

        public List<QInit> Storeroom = new List<QInit>
        {
            new QInit(1, 0, 0, 24, 0, "Are all fire exits and escape routes clear?"),
            new QInit(7, 0, 32, 11, 5, "Is the high-risk locker secured (unauthorised access restricted)?")
        };
    }
}