using DAL;
using Newtonsoft.Json;
using System;

namespace Models
{
    public class Teachers : Record
    {
        public string FirstName;
        public string LastName;
        public int Code;
        public DateTime StartDate;
        public string Email;
        public int Phone;

        const string Avatars_Folder = @"/App_Assets/Teachers/";
        const string Default_Avatar = @"no_avatar.png";
        [ImageAsset(Avatars_Folder, Default_Avatar)]
        public string Avatar { get; set; } = Avatars_Folder + Default_Avatar;
    }
}
