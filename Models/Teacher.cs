using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web.Mvc;

namespace Models
{
    public class Teacher : Scholar<Allocation>
    {
        public override Repository<Allocation> SelectionRepository => DB.Allocations;

        public DateTime StartDate { get; set; }

        const string Avatars_Folder = @"/App_Assets/Teachers/";
        const string Default_Avatar = @"no_avatar.png";
        [ImageAsset(Avatars_Folder, Default_Avatar)]
        public string Avatar { get; set; } = Avatars_Folder + Default_Avatar;

        public override int TryGenerateCode() => rand.Next() % (int)Math.Pow(10, 5);
        public static string CodeFormat(int code) => "CLG-420-" + code;
        public override void SetCode(int code) => Code = CodeFormat(code);
    }
}
