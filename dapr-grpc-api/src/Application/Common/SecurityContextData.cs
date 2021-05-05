namespace SC.API.CleanArchitecture.Application.Common
{
    public class SecurityContextData
    {
        /// <summary>
        /// party role id partyja (org, sp, plown) u cijem kontekstu se korisnik ulogirava
        /// </summary>
        public long PRInvWithId { get; set; }
        /// <summary>
        /// party role association id konteksta u kojem se korisnik ulogirava
        /// </summary>
        public int PRAssId { get; set; }
        /// <summary>
        /// party role type id party role partyja (org, sp, plown) u cijem kontekstu se korisnik ulogirava
        /// </summary>
        public int IWPRTypeId { get; set; }
        /// <summary>
        /// korisnikov party role id koji je u associjaciji s partyjem (org, sp, plown) za taj kontekst
        /// </summary>
        public long UserPRId { get; set; }
        /// <summary>
        /// party name partyja (org, sp, plown) u cijem kontekstu se korisnik ulogirava
        /// </summary>
        public string IWPartyName { get; set; }
        /// <summary>
        /// party role type name party role partyja (org, sp, plown) u cijem kontekstu se korisnik ulogirava
        /// </summary>
        public string IWPRTypeName { get; set; }
        /// <summary>
        /// ovo se odnosi na aktivni kontext aplikacije i nema veze sa db
        /// </summary>
        public bool IsActiveContext { get; set; } 

        public override bool Equals(object obj)
        {
            var scd = (SecurityContextData)obj;

            if (scd == null) {
                return false;
            }

            return (PRInvWithId == scd.PRInvWithId) && (PRAssId == scd.PRAssId);
        }

        public override int GetHashCode()
        {
            return PRInvWithId.GetHashCode() ^ PRAssId.GetHashCode();
        }

        public static bool operator == (SecurityContextData a, SecurityContextData b)
        {
            if (System.Object.ReferenceEquals(a, b)) {
                return true;
            }

            if ((object)a == null || (object)b == null) {
                return false;
            }

            return a.PRInvWithId == b.PRInvWithId && a.PRAssId == b.PRAssId;
        }

        public static bool operator != (SecurityContextData a, SecurityContextData b)
        {
            return !(a == b);
        }
    }
}
