using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace System_Curve__4._0
{
    public class brands
    {
        public const int nb = 5;

        public static string[] obtention_marques()
        {
            string[] liste_marques = new string[] {
                                                "Technojet",        //0
                                                "Technoprocess",    //1
                                                "Technoslurry",     //2
                                                "Tsurumi",          //3
                                                "Grundfos"};        //4
            return liste_marques;
        }
    }

    public class serie
    {
        public static string[] obtention_serie(int marque)
        {
            string[] liste_serie = new string[1];

            switch (marque)
            {
                case F.TECHNOJET:
                    liste_serie = new string[] {
                                                            "MH",   //0,0
                                                            "SM" }; //0,1
                    break;

                case F.TECHNOPROCESS:
                    liste_serie = new string[] {
                                                            "TECHNO" }; //1,0
                    break;

                case F.TECHNOSLURRY:
                    liste_serie = new string[] {
                                                            "SHP" };    //2,0
                    break;

                case F.TSURUMI:
                    liste_serie = new string[] {
                                                            "GSZ",      //3,0
                                                            "LH",       //3,1
                                                            "LHW",      //3,2
                                                            "B",        //3,3
                                                            "BZ",       //3,4
                                                            "C_HH",     //3,5
                                                            "C",        //3,6
                                                            "GPN",      //3,7
                                                            "HS",       //3,8
                                                            "KTD",      //3,9
                                                            "KTV",      //3,10
                                                            "KTV_SK",   //3,11
                                                            "KTVE",     //3,12
                                                            "KTZ",      //3,13
                                                            "KRS",      //3,14
                                                            "KRS_SK",   //3,15
                                                            "LSC",      //3,16
                                                            "MG",       //3,17
                                                            "NK",       //3,18
                                                            "NK_SK",    //3,19
                                                            "LB",       //3,20
                                                            "SFQ",      //3,21
                                                            "SQ",       //3,22
                                                            "TM",       //3,23
                                                            "U",        //3,24
                                                            "UT",       //3,25
                                                            "UZ"  };    //3,26
                    break;

                case F.GRUNDFOS:
                    liste_serie = new string[] {
                                                            "CR4S",     //4,0
                                                            "CR7S",     //4,1
                                                            "CR10S",    //4,2
                                                            "CR16S",    //4,3
                                                            "CR25S",    //4,4
                                                            "CR40S",    //4,5
                                                            "CR65S",    //4,6
                                                            "CR75S",    //4,7
                                                            "CR85S",    //4,8
                                                            "CR125S",   //4,9
                                                            "CR230S",   //4,10
                                                            "CR300S",   //4,11
                                                            "CR385S",   //4,12
                                                            "CR475S",   //4,13
                                                            "CR625S",   //4,14
                                                            "CR800S",   //4,15
                                                            "CR1100S",  //4,16
                                                            
                                                            "SP5S",     //4,17
                                                            "SP7S",     //4,18
                                                            "SP10S",    //4,19
                                                            "SP16S",    //4,20
                                                            "SP25S",    //4,21
                                                            "SP40S",    //4,22
                                                            "SP60S",    //4,23
                                                            "SP75S",    //4,24
                                                            "SP85S",    //4,25
                                                            "SP150S",   //4,26
                                                            "SP230S",   //4,27
                                                            "SP300S",   //4,28
                                                            "SP385S",   //4,29
                                                            "SP475S",   //4,30
                                                            "SP625S",   //4,21
                                                            "SP800S",   //4,22
                                                            "SP1100S" };//4,23
                    break;
            }
            return liste_serie;
        }
    }

    public class modele
    {
        public static string[] obtention_modeles(int marque, int serie)
        {
            string[] liste_modeles = new string[1];

            //Si la marque selectionnee est une Technojet
            if (marque == F.TECHNOJET)
            {
                switch (serie)
                {
                    case F.MH:
                        liste_modeles =
                             new string[] {
                                        "MH40_65_1800",     //0,0,0          
                                        "MH45_65_3600",     //0,0,1
                                        "MH50_80_1800",     //0,0,2
                                        "MH50_80_3600",     //0,0,3
                                        "MH80_125_1800",    //0,0,4
                                        "MH80_125_3600",    //0,0,5
                                        "MH100_150_1800",   //0,0,6
                                        "MH100_150_3600",   //0,0,7
                                        "MH125_150A_1800",  //0,0,8
                                        "MH125_150B_1800",  //0,0,9
                                        "MH200_250A_1800"}; //0,0,10
                        break;

                    case F.SM:
                        liste_modeles =
                            new string[] {
                                        "SM40_65_3600",     //0,1,0
                                        "SM50_80_3600",     //0,1,1
                                        "SM80_125_1800",    //0,1,2
                                        "SM80_125_3600",    //0,1,3
                                        "SM100_150_1800",   //0,1,4
                                        "SM100_150_3600",   //0,1,5
                                        "SM125_150A_1800",  //0,1,6
                                        "SM125_150B_1800",  //0,1,7
                                        "SM125_150C_1800",  //0,1,8
                                        "SM200_250_1800" }; //0,1,9
                        break;
                }
            }
            else if (marque == F.TECHNOPROCESS)
            {
                switch (serie)
                {
                    case F.TECHNO:
                        liste_modeles =
                            new string[] {
                                        "TECHNO40",         //1,0,0
                                        "TECHNO60" };       //1,0,1
                        break;
                }
            }
            else if (marque == F.TECHNOSLURRY)
            {
                switch (serie)
                {
                    case F.SHP:
                        liste_modeles =
                            new string[] {
                                        "SHP2600",          //2,0,0
                                        "SHP1800" };        //2,0,1
                        break;
                }
            }
            else if (marque == F.TSURUMI)
            {
                switch (serie)
                {
                    case F.GSZ:
                        liste_modeles =
                            new string[] {
                                        "GSZ 5 22 6",       //
                                        "GSZ 5 37 4H",      //
                                        "GSZ 5 37 4",       //
                                        "GSZ 5 37 6",       //
                                        "GSZ 4 45 4",       //
                                        "GSZ 55 4",         //
                                        "GSZ 75 4",         //
                                        "GSZ 75 4L"};       //
                        break;

                    case F.LH:
                        liste_modeles =
                            new string[] {
                                        "LH33_0",
                                        "LH422",
                                        "LH430",
                                        "LH615",
                                        "LH622",
                                        "LH637",
                                        "LH645",
                                        "LH675",
                                        "LH6110",
                                        "LH837",
                                        "LH845",
                                        "LH875",
                                        "LH8110"}; break;

                    case F.LHW:
                        liste_modeles =
                            new string[] {
                                        "LH23_0W",
                                        "LH25_5W",
                                        "LH311W",
                                        "LH322W",
                                        "LH430W",
                                        "LH4110W"}; break;

                    case F.b:
                        liste_modeles =
                            new string[] {
                                        "50B2_75S",
                                        "50B2_75H",
                                        "50B2_75",
                                        "80B21_5",
                                        "100B42_2",
                                        "100B43_7H",
                                        "100B43_7",
                                        "150B63_7",
                                        "100B45_5",
                                        "100B47_5",
                                        "150B47_5H",
                                        "150B47_5L",
                                        "150B411",
                                        "150B415",
                                        "150BK422",
                                        "150BK437",
                                        "200B47_5",
                                        "200B411",
                                        "200B415" }; break;

                    case F.BZ:
                        liste_modeles =
                            new string[] {
                                        "BZ100BZ41_5",
                                        "BZ100BZ42_2",
                                        "BZ100BZ43_7",
                                        "BZ100BZ411"}; break;

                    case F.C_HH:
                        liste_modeles =
                            new string[] {
                                        "C_HH80C22_2",
                                        "C_HH80C23_7",
                                        "C_HH80C25_5",
                                        "C_HH80C27_5",
                                        "C_HH80C211",
                                        "C_HH80C215",
                                        "C_HH100C222"}; break;

                    case F.c:
                        liste_modeles =
                            new string[] {
                                         "C50C2_75",
                                         "C50C4_75",
                                         "C80C21_5",
                                         "C80C41_5",
                                         "C100C42_2",
                                         "C100C43_7",
                                         "C100C45_5",
                                         "C100C47_5",
                                         "C100C411",
                                         "C100C415",
                                         "C150C611"}; break;

                    case F.GPN:
                        liste_modeles =
                            new string[] {
                                        "GPN35_5",
                                        "GPN411",
                                        "GPN415",
                                        "GPN622"}; break;

                    case F.HS:
                        liste_modeles =
                            new string[] {
                                        "HS2_4S",
                                        "HS3_75S",
                                        "HS2_55S"}; break;

                    case F.KTD:
                        liste_modeles =
                            new string[] {
                                        "KTD22_0",
                                        "KTD33_0"}; break;

                    case F.KTV:
                        liste_modeles =
                            new string[] {
                                        "KTV2_8",
                                        "KTV2_15",
                                        "KTV2_22",
                                        "KTV2_37H",
                                        "KTV2_37",
                                        "KTV2_55"}; break;

                    case F.KTV_SK:
                        liste_modeles =
                            new string[] {
                                        "KTV2_50",
                                        "KTV2_80"}; break;

                    case F.KTVE:
                        liste_modeles =
                            new string[] {
                                        "KTVE2_75",
                                        "KTVE21_5",
                                        "KTVE22_2",
                                        "KTVE33_7"}; break;

                    case F.KTZ:
                        liste_modeles =
                            new string[] {
                                        "KTZ21_5",
                                        "KTZ31_5",
                                        "KTZ22_2",
                                        "KTZ32_2",
                                        "KTZ23_7",
                                        "KTZ33_7",
                                        "KTZ43_7",
                                        "KTZ35_5",
                                        "KTZ45_5",
                                        "KTZ47_5",
                                        "KTZ67_5",
                                        "KTZ411",
                                        "KTZ611"}; break;

                    case F.KRS:
                        liste_modeles =
                            new string[] {
                                        "KRS2_A3",
                                        "KRS2_B3",
                                        "KRS2_A4",
                                        "KRS2_B4",
                                        "KRS2_A6",
                                        "KRS2_B6",
                                        "KRS2_8S",
                                        "KRS815",
                                        "KRS819",
                                        "KRS822",
                                        "KRS822L",
                                        "KRS1022",
                                        "KRS1230",
                                        "KRS1437"}; break;

                    case F.KRS_SK:
                        liste_modeles =
                            new string[] {
                                        "KRS2_80",
                                        "KRS2_100",
                                        "KRS2_150",
                                        "KRS2_200"}; break;

                    case F.LSC:
                        liste_modeles =
                            new string[] {
                                         "LSC1_4S"}; break;

                    case F.MG:
                        liste_modeles =
                            new string[] {
                                        "MG50MG21_5",
                                        "MG50MG22_2"}; break;

                    case F.NK:
                        liste_modeles =
                            new string[] {
                                        "NK2_15",
                                        "NK2_22",
                                        "NK2_22L"}; break;

                    case F.NK_SK:
                        liste_modeles =
                            new string[] {
                                        "NK2_15SK",
                                        "NK2_22SK"}; break;

                    case F.LB:
                        liste_modeles =
                            new string[] {
                                        "LB_480",
                                        "LB_800",
                                        "LBT_800",
                                        "LB_1500",
                                        "LBT_1500"}; break;

                    case F.SFQ:
                        liste_modeles =
                            new string[] {
                                        "SFQ50SFQ2_75",
                                        "SFQ50SFQ21_5",
                                        "SFQ50SFQ23_7",
                                        "SFQ50SFQ25_5",
                                        "SFQ50SFQ27_5",
                                        "SFQ50SFQ211" }; break;

                    case F.SQ:
                        liste_modeles =
                            new string[] {
                                        "SQ50SQ2_4S",
                                        "SQ50SQ2_75"}; break;

                    case F.TM:
                        liste_modeles =
                            new string[] {
                                        "TM50TM2_4S",
                                        "TM50TM2_4",
                                        "TM50TM2_75S",
                                        "TM50TM2_75",
                                        "TM80TM21_5",
                                        "TM80TM22_2",
                                        "TM80TM23_7"}; break;

                    case F.U:
                        liste_modeles =
                            new string[] {
                                        "U50U21_5",
                                        "U80U21_5",
                                        "U80U22_2",
                                        "U80U23_7"}; break;

                    case F.UT:
                        liste_modeles =
                            new string[] {
                                        "UT50UT2_4S",
                                        "UT50UTZ2_4S",
                                        "UT50UT2_75S",
                                        "UT50UTZ2_75S"}; break;

                    case F.UZ:
                        liste_modeles =
                            new string[] {
                                       "UZ50UZ41_5",
                                        "UZ80UZ41_5",
                                        "UZ80UZ42_2",
                                        "UZ80UZ43_7",
                                        "UZ80UZ45_5",
                                        "UZ80UZ47_5",
                                        "UZ80UZ411",
                                        "UZ100UZ43_7",
                                        "UZ100UZ45_5",
                                        "UZ100UZ47_5",
                                        "UZ100UZ411"}; break;
                }
            }
            else if (marque == F.GRUNDFOS)
            {
                switch (serie)
                {
                    case F.CR4S:
                        liste_modeles =
                             new string[] { "4S" }; break;
                    case F.CR7S:
                        liste_modeles =
                            new string[] { "7S" }; break;
                    case F.CR10S:
                        liste_modeles =
                            new string[] {
                                        "10S - 6 STAGES",
                                        "10S - 9 STAGES",
                                        "10S - 12 STAGES",
                                        "10S - 15 STAGES",
                                        "10S - 21 STAGES",
                                        "10S - 27 STAGES",
                                        "10S - 34 STAGES",
                                        "10S - 48 STAGES",
                                        "10S - 58 STAGES" }; break;

                    case F.CR16S:
                        liste_modeles =
                            new string[] {
                                        "16S - 5 STAGES",
                                        "16S - 8 STAGES",
                                        "16S - 10 STAGES",
                                        "16S - 18 STAGES",
                                        "16S - 24 STAGES",
                                        "16S - 38 STAGES",
                                        "16S - 56 STAGES",
                                        "16S - 75 STAGES"}; break;
                    case F.CR25S:
                        liste_modeles =
                            new string[] {
                                        "25S - 3 STAGES",
                                        "25S - 5 STAGES",
                                        "25S - 7 STAGES",
                                        "25S - 9 STAGES",
                                        "25S - 11 STAGES",
                                        "25S - 15 STAGES",
                                        "25S - 26 STAGES",
                                        "25S - 39 STAGES",
                                        "25S - 52 STAGES"}; break;
                    case F.CR40S:
                        liste_modeles =
                            new string[] {
                                        "40S - 3 STAGES",
                                        "40S - 5 STAGES",
                                        "40S - 7 STAGES",
                                        "40S - 9 STAGES",
                                        "40S - 12 STAGES",
                                        "40S - 15 STAGES",
                                        "40S - 21 STAGES",
                                        "40S - 25 STAGES",
                                        "40S - 30 STAGES",
                                        "40S - 37 STAGES",
                                        "40S - 44 STAGES",
                                        "40S - 50 STAGES",
                                        "40S - 58 STAGES",
                                        "40S - 66 STAGES"}; break;
                    case F.CR60S:
                        liste_modeles =
                            new string[] {
                                        "60S - 4 STAGES",
                                        "60S - 5 STAGES",
                                        "60S - 7 STAGES",
                                        "60S - 9 STAGES",
                                        "60S - 13 STAGES",
                                        "60S - 18 STAGES"}; break;
                    case F.CR75S:
                        liste_modeles =
                            new string[] {
                                        "75S - 3 STAGES",
                                        "75S - 5 STAGES",
                                        "75S - 8 STAGES",
                                        "75S - 11 STAGES",
                                        "75S - 12 STAGES",
                                        "75S -15 STAGES",
                                        "75S - 16 STAGES"}; break;
                    case F.CR85S:
                        liste_modeles =
                            new string[] { "85S - 1 STAGE" }; break;
                    case F.CR150S:
                        liste_modeles =
                            new string[] { "150S - 1 STAGE" }; break;
                    case F.CR230S:
                        liste_modeles =
                            new string[] { "230S - 1 STAGE" }; break;
                    case F.CR300S:
                        liste_modeles =
                            new string[] { "300S - 1 STAGE" }; break;
                    case F.CR385S:
                        liste_modeles =
                            new string[] { "385S - 1 STAGE" }; break;
                    case F.CR475S:
                        liste_modeles =
                            new string[] { "475S - 1 STAGE" }; break;
                    case F.CR625S:
                        liste_modeles =
                            new string[] { "625S- 1 STAGE" }; break;
                    case F.CR800S:
                        liste_modeles =
                            new string[] { "800S - 1 STAGE" }; break;
                    case F.CR1100S:
                        liste_modeles =
                            new string[] { "1100S - 1 STAGE" }; break;
                    case F.SP5S:
                        liste_modeles =
                            new string[] { "5S - 8 STAGES" }; break;
                    case F.SP7S:
                        liste_modeles =
                            new string[] { "7S - 1 STAGE" }; break;
                    case F.SP10S:
                        liste_modeles =
                            new string[] {
                                        "10S - S STAGES",
                                        "10S - 9 STAGES",
                                        "10S - 12 STAGES",
                                        "10S - 15 STAGES",
                                        "10S - 21 STAGES",
                                        "10S - 27 STAGES",
                                        "10S - 34 STAGES",
                                        "10S - 48 STAGES",
                                        "10S - 58 STAGES"}; break;
                    case F.SP16S:
                        liste_modeles =
                            new string[] {
                                        "16S - 5 STAGES",
                                        "16S - 8 STAGES",
                                        "16S - 10 STAGES",
                                        "16S - 14 STAGES",
                                        "16S - 18 STAGES",
                                        "16S - 24 STAGES",
                                        "16S - 38 STAGES",
                                        "16S - 56 STAGES",
                                        "16S - 75 STAGES"}; break;
                    case F.SP25S:
                        liste_modeles =
                            new string[] {
                                        "25S - 3 STAGES",
                                        "25S - 5 STAGES",
                                        "25S - 7 STAGES",
                                        "25S - 9 STAGES",
                                        "25S - 11 STAGES",
                                        "25S - 15 STAGES",
                                        "25S - 26 STAGES",
                                        "25S - 39 STAGES",
                                        "25S - 52 STAGES"}; break;
                    case F.SP40S:
                        liste_modeles =
                            new string[] {
                                        "40S - 3 STAGES",
                                        "40S - 5 STAGES",
                                        "40S - 7 STAGES",
                                        "40S - 9 STAGES",
                                        "40S - 12 STAGES",
                                        "40S - 15 STAGES",
                                        "40S - 21 STAGES",
                                        "40S - 25 STAGES",
                                        "40S - 30 STAGES",
                                        "40S - 37 STAGES",
                                        "40S - 44 STAGES",
                                        "40S - 50 STAGES",
                                        "40S - 58 STAGES",
                                        "40S - 66 STAGES"}; break;
                    case F.SP60S:
                        liste_modeles =
                            new string[] {
                                        "60S - 4 STAGES",
                                        "60S - 5 STAGES",
                                        "60S - 7 STAGES",
                                        "40S - 9 STAGES",
                                        "40S - 13 STAGES",
                                        "40S - 18 STAGES"}; break;
                    case F.SP75S:
                        liste_modeles =
                            new string[] {
                                        "75S - 3 STAGES",
                                        "75S - 5 STAGES",
                                        "75S - 8 STAGES",
                                        "75S - 11 STAGES",
                                        "75S - 12 STAGES",
                                        "75S - 15 STAGES",
                                        "75S - 16 STAGES"}; break;
                    case F.SP85S:
                        liste_modeles =
                            new string[] { "85S - 1 STAGE" }; break;
                    case F.SP150S:
                        liste_modeles =
                            new string[] { "150S - 1 STAGE" }; break;
                    case F.SP230S:
                        liste_modeles =
                            new string[] { "230S - 1 STAGE" }; break;
                    case F.SP300S:
                        liste_modeles =
                            new string[] { "300S - 1 STAGE" }; break;
                    case F.SP385S:
                        liste_modeles =
                            new string[] { "385S - 1 STAGE" }; break;
                    case F.SP475S:
                        liste_modeles =
                            new string[] { "475S - 1 STAGE" }; break;
                    case F.SP625S:
                        liste_modeles =
                            new string[] { "625S - 1 STAGE" }; break;
                    case F.SP800S:
                        liste_modeles =
                            new string[] { "800S - 1 STAGE" }; break;
                    case F.SP1100S:
                        liste_modeles =
                            new string[] { "1100S - 1 STAGE" }; break;
                }
            }
            return liste_modeles;
        }
    }
}
