namespace System_Curve__4._0
{
    public struct t_formule
    {
        //Type struct qui contient le 
        public string serie;
        public string modele;
        public double A;
        public double B;
        public double C;
        public double D;
        public double E;
        public int MAX;
        public double rA;
        public double rB;
        public double rC;
        public double rD;
        public double rE;
    }

    public static class F
    {
        //Constante necessaire pour indexer les tableaux de formule
        //Dans les tableaux de formule [Marque, Serie, Modele, Parametre]
        /*********************************************************************/

        //EXEMPLE ET EXPLICATIONS

        //**************SERIE MHR****************************************//
        //MHR40 - 65 - 1800RPM - 0
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.A] = 0;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.B] = 0;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.C] = -0.0011;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.D] = -0.0135;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.E] = 55.354;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.MAX] = 148;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.VITESSE] = 1800;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rA] = 0;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rB] = 0;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rC] = -0.0052;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rD] = 1.0064;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rE] = 18.838;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rMIN] = 44;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pA] = 0;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pB] = 0;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pC] = -0.00002;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pD] = 0.0101;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pE] = 0.8009;

        //==Le nom du modele avec la vitesse suivit de son index de modele==//
        ////MHR40-65 - 1800RPM    -   0
        //==Ensuite les parametres pour l'equation polynomiale sont listes comme ceci==//
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.A] = 0;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.B] = 0;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.C] = -0.0011;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.D] = -0.0135;
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.E] = 55.354;
        //==Le maximum represente le point en abcisse (X) ou s'arrete la courbe de la pompe en USGPM
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.MAX] = 148;
        //==Represente la vitesse maximale theorique de la pompe
        //formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.VITESSE] = 1800;

        //Parametres de l'equation de la courbe de la pompe
        public const int A = 0;
        public const int B = 1;
        public const int C = 2;
        public const int D = 3;
        public const int E = 4;
        //Valeur maximale sur l'axe des X (Debit)
        public const int MAX = 5;
        //Vitesse standard pour la courbe
        public const int VITESSE = 6;
        //Parametres de l'equation de la courbe d'efficacite (rendement en %)
        public const int rA = 7;
        public const int rB = 8;
        public const int rC = 9;
        public const int rD = 10;
        public const int rE = 11;
        public const int rMIN = 12;
        //Parametres de l'equation de la courbe de puissance (Hp) 
        public const int pA = 13;
        public const int pB = 14;
        public const int pC = 15;
        public const int pD = 16;
        public const int pE = 17;

        //==La premiere dimension du tableau representant les modele suit la liste suivante
        //TECHNOJET = 0
        //TECHNOPROCESS = 1
        //TECHNOSLURRY = 2
        //TSURUMI = 3
        //GRUNDFOS = 4


        //TECHNOJET============================================================
        public const int TECHNOJET = 0;
        //Series
        public const int MH = 0;
        public const int SM = 1;
        //Modeles
        public const int MH40_65_1800 = 0;
        public const int MH45_65_3600 = 1;
        public const int MH50_80_1800 = 2;
        public const int MH50_80_3600 = 3;
        public const int MH80_125_1800 = 4;
        public const int MH80_125_3600 = 5;
        public const int MH100_150_1800 = 6;
        public const int MH100_150_3600 = 7;
        public const int MH125_150A_1800 = 8;
        public const int MH125_150B_1800 = 9;
        public const int MH200_250A_1800 = 10;
        public const int SM40_65_3600 = 0;
        public const int SM50_80_3600 = 1;
        public const int SM80_125_1800 = 2;
        public const int SM80_125_3600 = 3;
        public const int SM100_150_1800 = 4;
        public const int SM100_150_3600 = 5;
        public const int SM125_150A_1800 = 6;
        public const int SM125_150B_1800 = 7;
        public const int SM125_150C_1800 = 8;
        public const int SM200_250_1800 = 9;

        //TECHNOPROCESS========================================================
        public const int TECHNOPROCESS = 1;
        //Series
        public const int TECHNO = 0;
        //Modeles
        public const int TECHNO40 = 0;
        public const int TECHNO60 = 1;

        //TECHNOSLURRY=========================================================
        public const int TECHNOSLURRY = 2;
        //Series
        public const int SHP = 0;
        //Modeles
        public const int SHP2600 = 0;
        public const int SHP1800 = 1;

        //TSURUMI==============================================================
        public const int TSURUMI = 3;
        //Series
        public const int GSZ = 0;
        public const int LH = 1;
        public const int LHW = 2;
        public const int b = 3;
        public const int BZ = 4;
        public const int C_HH = 5;
        public const int c = 6;
        public const int GPN = 7;
        public const int HS = 8;
        public const int KTD = 9;
        public const int KTV = 10;
        public const int KTV_SK = 11;
        public const int KTVE = 12;
        public const int KTZ = 13;
        public const int KRS = 14;
        public const int KRS_SK = 15;
        public const int LSC = 16;
        public const int MG = 17;
        public const int NK = 18;
        public const int NK_SK = 19;
        public const int LB = 20;
        public const int SFQ = 21;
        public const int SQ = 22;
        public const int TM = 23;
        public const int U = 24;
        public const int UT = 25;
        public const int UZ = 26;
        //Modeles
        public const int GSZ_5_22_6 = 0;
        public const int GSZ_5_37_4H = 1;
        public const int GSZ_5_37_4 = 2;
        public const int GSZ_5_37_6 = 3;
        public const int GSZ_4_45_4 = 4;
        public const int GSZ_55_4 = 5;
        public const int GSZ_75_4 = 6;
        public const int GSZ_75_4L = 7;
        public const int LH33_0 = 0;
        public const int LH422 = 1;
        public const int LH430 = 2;
        public const int LH615 = 3;
        public const int LH622 = 4;
        public const int LH637 = 5;
        public const int LH645 = 6;
        public const int LH675 = 7;
        public const int LH6110 = 8;
        public const int LH837 = 9;
        public const int LH845 = 10;
        public const int LH875 = 11;
        public const int LH8110 = 12;
        public const int LH23_0W = 0;
        public const int LH25_5W = 1;
        public const int LH311W = 2;
        public const int LH322W = 3;
        public const int LH430W = 4;
        public const int LH4110W = 5;
        public const int B50B2_75S = 0;
        public const int B50B2_75H = 1;
        public const int B50B2_75 = 2;
        public const int B80B21_5 = 3;
        public const int B100B42_2 = 4;
        public const int B100B43_7H = 5;
        public const int B100B43_7 = 6;
        public const int B150B63_7 = 7;
        public const int B100B45_5 = 8;
        public const int B100B47_5 = 9;
        public const int B150B47_5H = 10;
        public const int B150B47_5L = 11;
        public const int B150B411 = 12;
        public const int B150B415 = 13;
        public const int B150BK422 = 14;
        public const int B150BK437 = 15;
        public const int B200B47_5 = 16;
        public const int B200B411 = 17;
        public const int B200B415 = 18;
        public const int BZ100BZ41_5 = 0;
        public const int BZ100BZ42_2 = 1;
        public const int BZ100BZ43_7 = 2;
        public const int BZ100BZ411 = 3;
        public const int C_HH80C22_2 = 0;
        public const int C_HH80C23_7 = 1;
        public const int C_HH80C25_5 = 2;
        public const int C_HH80C27_5 = 3;
        public const int C_HH80C211 = 4;
        public const int C_HH80C215 = 5;
        public const int C_HH100C222 = 6;
        public const int C50C2_75 = 0;
        public const int C50C4_75 = 1;
        public const int C80C21_5 = 2;
        public const int C80C41_5 = 3;
        public const int C100C42_2 = 4;
        public const int C100C43_7 = 5;
        public const int C100C45_5 = 6;
        public const int C100C47_5 = 7;
        public const int C100C411 = 8;
        public const int C100C415 = 9;
        public const int C150C611 = 10;
        public const int GPN35_5 = 0;
        public const int GPN411 = 1;
        public const int GPN415 = 2;
        public const int GPN622 = 3;
        public const int HS2_4S = 0;
        public const int HS3_75S = 1;
        public const int HS2_55S = 2;
        public const int KTD22_0 = 0;
        public const int KTD33_0 = 1;
        public const int KTV2_8 = 0;
        public const int KTV2_15 = 1;
        public const int KTV2_22 = 2;
        public const int KTV2_37H = 3;
        public const int KTV2_37 = 4;
        public const int KTV2_55 = 5;
        public const int KTV2_50 = 0;
        public const int KTV2_80 = 1;
        public const int KTVE2_75 = 0;
        public const int KTVE21_5 = 1;
        public const int KTVE22_2 = 2;
        public const int KTVE33_7 = 3;
        public const int KTZ21_5 = 0;
        public const int KTZ31_5 = 1;
        public const int KTZ22_2 = 2;
        public const int KTZ32_2 = 3;
        public const int KTZ23_7 = 4;
        public const int KTZ33_7 = 5;
        public const int KTZ43_7 = 6;
        public const int KTZ35_5 = 7;
        public const int KTZ45_5 = 8;
        public const int KTZ47_5 = 9;
        public const int KTZ67_5 = 10;
        public const int KTZ411 = 11;
        public const int KTZ611 = 12;
        public const int KRS2_A3 = 0;
        public const int KRS2_B3 = 1;
        public const int KRS2_A4 = 2;
        public const int KRS2_B4 = 3;
        public const int KRS2_A6 = 4;
        public const int KRS2_B6 = 5;
        public const int KRS2_8S = 6;
        public const int KRS815 = 7;
        public const int KRS819 = 8;
        public const int KRS822 = 9;
        public const int KRS822L = 10;
        public const int KRS1022 = 11;
        public const int KRS1230 = 12;
        public const int KRS1437 = 13;
        public const int KRS2_80 = 0;
        public const int KRS2_100 = 1;
        public const int KRS2_150 = 2;
        public const int KRS2_200 = 3;
        public const int LSC1_4S = 0;
        public const int MG50MG21_5 = 0;
        public const int MG50MG22_2 = 1;
        public const int MG50MG23_7 = 2;
        public const int NK2_15 = 0;
        public const int NK2_22 = 1;
        public const int NK2_22L = 2;
        public const int NK2_15SK = 0;
        public const int NK2_22SK = 1;
        public const int LB_480 = 0;
        public const int LB_800 = 1;
        public const int LBT_800 = 2;
        public const int LB_1500 = 3;
        public const int LBT_1500 = 4;
        public const int SFQ50SFQ2_75 = 0;
        public const int SFQ50SFQ21_5 = 1;
        public const int SFQ50SFQ23_7 = 2;
        public const int SFQ50SFQ25_5 = 3;
        public const int SFQ50SFQ27_5 = 4;
        public const int SFQ50SFQ211 = 5;
        public const int SQ50SQ2_4S = 0;
        public const int SQ50SQ2_75 = 1;
        public const int TM50TM2_4S = 0;
        public const int TM50TM2_4 = 1;
        public const int TM50TM2_75S = 2;
        public const int TM50TM2_75 = 3;
        public const int TM80TM21_5 = 4;
        public const int TM80TM22_2 = 5;
        public const int TM80TM23_7 = 6;
        public const int U50U21_5 = 0;
        public const int U80U21_5 = 1;
        public const int U80U22_2 = 2;
        public const int U80U23_7 = 3;
        public const int UT50UT2_4S = 0;
        public const int UT50UTZ2_4S = 1;
        public const int UT50UT2_75S = 2;
        public const int UT50UTZ2_75S = 3;
        public const int UZ50UZ41_5 = 0;
        public const int UZ80UZ41_5 = 1;
        public const int UZ80UZ42_2 = 2;
        public const int UZ80UZ43_7 = 3;
        public const int UZ80UZ45_5 = 4;
        public const int UZ80UZ47_5 = 5;
        public const int UZ80UZ411 = 6;
        public const int UZ100UZ43_7 = 7;
        public const int UZ100UZ45_5 = 8;
        public const int UZ100UZ47_5 = 9;
        public const int UZ100UZ411 = 10;

        //GRUNDFOS=============================================================
        public const int GRUNDFOS = 4;
        //Series
        public const int CR4S = 0;
        public const int CR7S = 1;
        public const int CR10S = 2;
        public const int CR16S = 3;
        public const int CR25S = 4;
        public const int CR40S = 5;
        public const int CR60S = 6;
        public const int CR75S = 7;
        public const int CR85S = 8;
        public const int CR150S = 9;
        public const int CR230S = 10;
        public const int CR300S = 11;
        public const int CR385S = 12;
        public const int CR475S = 13;
        public const int CR625S = 14;
        public const int CR800S = 15;
        public const int CR1100S = 16;

        public const int SP5S = 17;
        public const int SP7S = 18;
        public const int SP10S = 19;
        public const int SP16S = 20;
        public const int SP25S = 21;
        public const int SP40S = 22;
        public const int SP60S = 23;
        public const int SP75S = 24;
        public const int SP85S = 25;
        public const int SP150S = 26;
        public const int SP230S = 27;
        public const int SP300S = 28;
        public const int SP385S = 29;
        public const int SP475S = 30;
        public const int SP625S = 31;
        public const int SP800S = 32;
        public const int SP1100S = 33;




        public const int SP = 17;
        //Modeles
        public const int CR4S_1_ST = 0;

        public const int CR7S_1_ST = 0;

        public const int CR10S_6_ST = 0;
        public const int CR10S_9_ST = 1;
        public const int CR10S_12_ST = 2;
        public const int CR10S_15_ST = 3;
        public const int CR10S_21_ST = 4;
        public const int CR10S_27_ST = 5;
        public const int CR10S_34_ST = 6;
        public const int CR10S_48_ST = 7;
        public const int CR10S_58_ST = 8;

        public const int CR16S_5_ST = 0;
        public const int CR16S_8_ST = 1;
        public const int CR16S_10_ST = 2;
        public const int CR16S_14_ST = 3;
        public const int CR16S_18_ST = 4;
        public const int CR16S_24_ST = 5;
        public const int CR16S_38_ST = 6;
        public const int CR16S_56_ST = 7;
        public const int CR16S_75_ST = 8;

        public const int CR25S_3_ST = 0;
        public const int CR25S_5_ST = 1;
        public const int CR25S_7_ST = 2;
        public const int CR25S_9_ST = 3;
        public const int CR25S_11_ST = 4;
        public const int CR25S_15_ST = 5;
        public const int CR25S_26_ST = 6;
        public const int CR25S_39_ST = 7;
        public const int CR25S_52_ST = 8;

        public const int CR40S_3_ST = 0;
        public const int CR40S_5_ST = 1;
        public const int CR40S_7_ST = 2;
        public const int CR40S_9_ST = 3;
        public const int CR40S_12_ST = 4;
        public const int CR40S_15_ST = 5;
        public const int CR40S_21_ST = 6;
        public const int CR40S_25_ST = 7;
        public const int CR40S_30_ST = 8;
        public const int CR40S_37_ST = 9;
        public const int CR40S_44_ST = 10;
        public const int CR40S_50_ST = 11;
        public const int CR40S_58_ST = 12;
        public const int CR40S_66_ST = 13;

        public const int CR60S_4_ST = 0;
        public const int CR60S_5_ST = 1;
        public const int CR60S_7_ST = 2;
        public const int CR60S_9_ST = 3;
        public const int CR60S_13_ST = 4;
        public const int CR60S_18_ST = 5;

        public const int CR75S_3_ST = 0;
        public const int CR75S_5_ST = 1;
        public const int CR75S_8_ST = 2;
        public const int CR75S_11_ST = 3;
        public const int CR75S_12_ST = 4;
        public const int CR75S_15_ST = 5;
        public const int CR75S_16_ST = 6;

        public const int CR85S_1_ST = 0;

        public const int CR150S_1_ST = 0;

        public const int CR230S_1_ST = 0;

        public const int CR300S_1_ST = 0;

        public const int CR385S_1_ST = 0;

        public const int CR475S_1_ST = 0;

        public const int CR625S_1_ST = 0;

        public const int CR800S_1_ST = 0;

        public const int CR1100S_1_ST = 0;


        public const int SP5S_8_ST = 0;

        public const int SP7S_1_ST = 0;

        public const int SP10S_6_ST = 0;
        public const int SP10S_9_ST = 1;
        public const int SP10S_12_ST = 2;
        public const int SP10S_15_ST = 3;
        public const int SP10S_21_ST = 4;
        public const int SP10S_27_ST = 5;
        public const int SP10S_34_ST = 6;
        public const int SP10S_48_ST = 7;
        public const int SP10S_58_ST = 8;

        public const int SP16S_5_ST = 0;
        public const int SP16S_8_ST = 1;
        public const int SP16S_10_ST = 2;
        public const int SP16S_14_ST = 3;
        public const int SP16S_18_ST = 4;
        public const int SP16S_24_ST = 5;
        public const int SP16S_38_ST = 6;
        public const int SP16S_56_ST = 7;
        public const int SP16S_75_ST = 8;

        public const int SP25S_3_ST = 0;
        public const int SP25S_5_ST = 1;
        public const int SP25S_7_ST = 2;
        public const int SP25S_9_ST = 3;
        public const int SP25S_11_ST = 4;
        public const int SP25S_15_ST = 5;
        public const int SP25S_26_ST = 6;
        public const int SP25S_39_ST = 7;
        public const int SP25S_52_ST = 8;

        public const int SP40S_3_ST = 0;
        public const int SP40S_5_ST = 1;
        public const int SP40S_7_ST = 2;
        public const int SP40S_9_ST = 3;
        public const int SP40S_12_ST = 4;
        public const int SP40S_15_ST = 5;
        public const int SP40S_21_ST = 6;
        public const int SP40S_25_ST = 7;
        public const int SP40S_30_ST = 8;
        public const int SP40S_37_ST = 9;
        public const int SP40S_44_ST = 10;
        public const int SP40S_50_ST = 11;
        public const int SP40S_58_ST = 12;
        public const int SP40S_66_ST = 13;

        public const int SP60S_4_ST = 0;
        public const int SP60S_5_ST = 1;
        public const int SP60S_7_ST = 2;
        public const int SP60S_9_ST = 3;
        public const int SP60S_13_ST = 4;
        public const int SP60S_18_ST = 5;

        public const int SP75S_3_ST = 0;
        public const int SP75S_5_ST = 1;
        public const int SP75S_8_ST = 2;
        public const int SP75S_11_ST = 3;
        public const int SP75S_12_ST = 4;
        public const int SP75S_15_ST = 5;
        public const int SP75S_16_ST = 6;

        public const int SP85S_1_ST = 0;

        public const int SP150S_1_ST = 0;

        public const int SP230S_1_ST = 0;

        public const int SP300S_1_ST = 0;

        public const int SP385S_1_ST = 0;

        public const int SP475S_1_ST = 0;

        public const int SP625S_1_ST = 0;

        public const int SP800S_1_ST = 0;

        public const int SP1100S_1_ST = 0;

        ////Nouvelle Marque=============================================================
        //public const int NOUVELLE_MARQUE = 5;
        ////Series
        //public const int NOUVELLE_SERIE = 0;
        //public const int AUTRE_NOUVELLE_SERIE = 1;
        ////Modele
        //public const int NOUVEAU_MODELE = 0;
    }



    public class Tendance
    {
        public static void remplissage_formules(double[,,,] formules)
        {
            #region TECHNOJET
            //=================================================================
            ////Technojet
            //=================================================================
            #region Serie MH
            //**************SERIE MHR****************************************//
            //MHR40 - 65 - 1800RPM - 0
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.C] = -0.0011;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.D] = -0.0135;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.E] = 55.354;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.MAX] = 148;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rC] = -0.0052;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rD] = 1.0064;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rE] = 18.838;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.rMIN] = 44;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pC] = -0.00002;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pD] = 0.0101;
            formules[F.TECHNOJET, F.MH, F.MH40_65_1800, F.pE] = 0.8009;
            //MHR40-65 3600RPM  -   1
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.C] = -0.0009;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.D] = -0.0852;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.E] = 223.24;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.MAX] = 260;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.VITESSE] = 3600;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.rC] = -0.0011;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.rD] = 0.4383;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.rE] = 23.921;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.rMIN] = 88;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.pC] = -0.00002;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.pD] = 0.0467;
            formules[F.TECHNOJET, F.MH, F.MH45_65_3600, F.pE] = 8.0859;
            //MHR50-80 1800RPM  -   2
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.C] = -0.0006;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.D] = 0.015;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.E] = 61.328;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.MAX] = 220;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.rC] = -0.0022;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.rD] = 0.6417;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.rE] = 22.216;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.rMIN] = 88;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.pC] = 0.000009;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.pD] = 0.0033;
            formules[F.TECHNOJET, F.MH, F.MH50_80_1800, F.pE] = 2.0212;
            //MHR50-80 3600RPM  -   3
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.C] = -0.0003;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.D] = -0.1246;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.E] = 249.98;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.MAX] = 440;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.VITESSE] = 3600;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.rC] = -0.0007;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.rD] = 0.4281;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.rE] = 1.9758;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.rMIN] = 155;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.pC] = -0.00001;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.pD] = 0.0243;
            formules[F.TECHNOJET, F.MH, F.MH50_80_3600, F.pE] = 15.079;
            //MHR80_125_1800    -   4
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.C] = -0.0003;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.D] = 0.0331;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.E] = 107.05;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.MAX] = 340;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.rC] = -0.0007;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.rD] = 0.339;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.rE] = 26.152;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.rMIN] = 155;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.pC] = 0.00002;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.pD] = 0.0106;
            formules[F.TECHNOJET, F.MH, F.MH80_125_1800, F.pE] = 4.5306;
            //MHR80_125_3600    -   5
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.C] = -0.0003;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.D] = 0.0402;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.E] = 423.75;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.MAX] = 840;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.VITESSE] = 3600;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.rC] = -0.0001;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.rD] = 0.1377;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.rE] = 33.714;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.rMIN] = 320;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.pC] = -0.00006;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.pD] = 0.1203;
            formules[F.TECHNOJET, F.MH, F.MH80_125_3600, F.pE] = 18.571;
            //MH100_150_1800    -   6
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.A] = -0.00000000001;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.B] = -0.0000001;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.C] = 0.000006;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.D] = -0.0104;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.E] = 138.95;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.MAX] = 700;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.rC] = -0.0002;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.rD] = 0.1964;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.rE] = 19.857;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.rMIN] = 175;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.pC] = -0.00003;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.pD] = 0.0392;
            formules[F.TECHNOJET, F.MH, F.MH100_150_1800, F.pE] = 9.2321;
            //MHR100_150_3600    -   7
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.C] = -0.00007;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.D] = -0.0281;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.E] = 559.43;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.MAX] = 1320;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.VITESSE] = 3600;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.rC] = -0.00007;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.rD] = 0.1353;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.rE] = 10.155;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.rMIN] = 210;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.pC] = -0.000007;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.pD] = 0.1014;
            formules[F.TECHNOJET, F.MH, F.MH100_150_3600, F.pE] = 68.29;
            //MHR125_150A_1800    -   8
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.C] = -0.00009;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.D] = 0.0341;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.E] = 177.19;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.MAX] = 1056;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.rA] = -0.0000000001;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.rB] = 0.0000003;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.rC] = -0.0004;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.rD] = 0.2734;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.rE] = 8.3651;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.rMIN] = 176;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.pC] = -0.00002;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.pD] = 0.0511;
            formules[F.TECHNOJET, F.MH, F.MH125_150A_1800, F.pE] = 11.7;
            //MHR125_150B_1800    -   9
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.C] = -0.000009;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.D] = -0.0317;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.E] = 178.83;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.MAX] = 1540;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.rC] = -0.00004;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.rD] = 0.0953;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.rE] = 18.029;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.rMIN] = 520;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.pC] = 0.000005;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.pD] = 0.0049;
            formules[F.TECHNOJET, F.MH, F.MH125_150B_1800, F.pE] = 35.586;
            //MHR200_250A_1800    -   10
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.A] = 0;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.B] = 0;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.C] = -0.00001;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.D] = -0.0006;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.E] = 270.7;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.MAX] = 2640;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.rC] = -0.00002;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.rD] = 0.0731;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.rE] = 4.1;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.rMIN] = 425;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.pC] = -0.000004;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.pD] = 0.054;
            formules[F.TECHNOJET, F.MH, F.MH200_250A_1800, F.pE] = 56.5;
            #endregion

            #region Serie SM
            //**************SERIE SM*****************************************//
            //SM40_65_3600  -   0
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.A] = 0;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.B] = 0;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.C] = -0.0009;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.D] = -0.0852;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.E] = 223.24;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.MAX] = 260;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.VITESSE] = 3600;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.rC] = -0.0011;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.rD] = 0.4383;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.rE] = 23.921;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.rMIN] = 85;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.pA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.pB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.pC] = -0.00002;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.pD] = 0.0467;
            formules[F.TECHNOJET, F.SM, F.SM40_65_3600, F.pE] = 8.0859;
            //SM50_80_3600  -   1
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.A] = 0.000000002;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.B] = -0.000002;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.C] = -0.00009;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.D] = -0.0074;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.E] = 243.56;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.MAX] = 440;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.VITESSE] = 3600;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.rC] = -0.0007;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.rD] = 0.4281;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.rE] = 1.9758;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.rMIN] = 150;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.pA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.pB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.pC] = -0.00001;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.pD] = 0.0243;
            formules[F.TECHNOJET, F.SM, F.SM50_80_3600, F.pE] = 15.079;
            //SM80_125_1800 -   2
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.A] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.B] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.C] = -0.0003;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.D] = 0.0331;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.E] = 107.05;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.MAX] = 340;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.rC] = -0.0007;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.rD] = 0.339;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.rE] = 26.152;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.rMIN] = 150;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.pC] = 0.00002;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.pD] = 0.0106;
            formules[F.TECHNOJET, F.SM, F.SM80_125_1800, F.pE] = 4.5306;
            //SM80_125_3600 -   3
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.A] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.B] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.C] = -0.0003;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.D] = 0.0402;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.E] = 423.75;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.MAX] = 840;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.VITESSE] = 3600;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rC] = -0.0001;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rD] = 0.1377;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rE] = 33.714;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rMIN] = 320;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rC] = -0.00006;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rD] = 0.1203;
            formules[F.TECHNOJET, F.SM, F.SM80_125_3600, F.rE] = 18.571;
            //SM100_150_1800    -   4
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.A] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.B] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.C] = -0.0001;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.D] = 0.0309;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.E] = 137.23;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.MAX] = 700;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.rC] = -0.0002;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.rD] = 0.1964;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.rE] = 19.857;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.rMIN] = 180;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.pC] = -0.00003;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.pD] = 0.0392;
            formules[F.TECHNOJET, F.SM, F.SM100_150_1800, F.pE] = 9.2321;
            //SM100_150_3600    -   5
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.A] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.B] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.C] = -0.00007;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.D] = -0.0281;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.E] = 559.43;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.MAX] = 1320;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.VITESSE] = 3600;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.rC] = -0.00007;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.rD] = 0.1353;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.rE] = 10.155;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.rMIN] = 210;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.pA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.pB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.pC] = -0.000007;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.pD] = 0.1014;
            formules[F.TECHNOJET, F.SM, F.SM100_150_3600, F.pE] = 68.29;
            //SM125_150A_1800   -   6
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.A] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.B] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.C] = -0.00009;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.D] = 0.0341;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.E] = 177.19;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.MAX] = 1056;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.rC] = -0.0001;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.rD] = 0.193;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.rE] = 6.9;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.rMIN] = 165;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.pC] = -0.00002;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.pD] = 0.0511;
            formules[F.TECHNOJET, F.SM, F.SM125_150A_1800, F.pE] = 11.7;
            //SM125_150B_1800   -   7
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.A] = -0.00000000004;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.B] = 0.00000008;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.C] = -0.00004;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.D] = -0.0455;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.E] = 166.16;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.MAX] = 1540;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.rC] = -0.00004;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.rD] = 0.0953;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.rE] = 18.029;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.rMIN] = 520;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.pC] = 0.000005;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.pD] = 0.0049;
            formules[F.TECHNOJET, F.SM, F.SM125_150B_1800, F.pE] = 35.586;
            //SM125_150C_1800   -   8
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.A] = -0.000000002;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.B] = 0.000003;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.C] = -0.0016;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.D] = 0.2888;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.E] = 165.35;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.MAX] = 1000;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.rC] = -0.0004;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.rD] = 0.4036;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.rE] = -20.227;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.rMIN] = 190;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.pC] = -0.0002;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.pD] = 0.2207;
            formules[F.TECHNOJET, F.SM, F.SM125_150C_1800, F.pE] = 19;
            //SM200_250_1800    -   9
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.A] = 0;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.B] = 0;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.C] = -0.00001;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.D] = -0.0006;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.E] = 270.07;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.MAX] = 2640;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.VITESSE] = 1800;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.rA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.rB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.rC] = -0.00002;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.rD] = 0.0731;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.rE] = 4.1;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.rMIN] = 450;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.pA] = 0;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.pB] = 0;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.pC] = -0.000004;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.pD] = 0.054;
            formules[F.TECHNOJET, F.SM, F.SM200_250_1800, F.pE] = 56.5;
            #endregion

            #endregion


            #region TECHNOPROCESS
            //=================================================================
            ////Technoprocess
            //=================================================================

            //TECHNO40
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.A] = -0.000000008;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.B] = 0.00000006;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.C] = -0.0012;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.D] = 0.0714;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.E] = 484.29;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.MAX] = 200;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.VITESSE] = 3600;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.rA] = 0;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.rB] = 0;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.rC] = -0.0006;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.rD] = 0.2601;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.rE] = 5;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.rMIN] = 120;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.pA] = 0;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.pB] = 0;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.pC] = 0.000000000000000007;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.pD] = 0.1071;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO40, F.pE] = 21.286;
            //TECHNO60
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.A] = -0.000000008;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.B] = 0.000003;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.C] = -0.0006;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.D] = 0.0171;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.E] = 475.01;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.MAX] = 400;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.VITESSE] = 3600;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.rA] = 0;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.rB] = 0;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.rC] = -0.0004;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.rD] = 0.3178;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.rE] = 7;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.rMIN] = 200;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.pA] = 0;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.pB] = 0;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.pC] = -0.00005;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.pD] = 0.0587;
            formules[F.TECHNOPROCESS, F.TECHNO, F.TECHNO60, F.pE] = 22.967;
            #endregion


            #region TECHNOSLURRY
            //=================================================================
            ////Technoslurry
            //=================================================================

            //SHP2600
            formules[F.TECHNOSLURRY, F.SHP, F.SHP2600, F.A] = 0;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP2600, F.B] = 0;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP2600, F.C] = -0.0005;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP2600, F.D] = -0.0105;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP2600, F.E] = 525.74;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP2600, F.MAX] = 528;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP2600, F.VITESSE] = 2600;
            //SHP1800
            formules[F.TECHNOSLURRY, F.SHP, F.SHP1800, F.A] = 0;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP1800, F.B] = 0;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP1800, F.C] = -0.0005;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP1800, F.D] = 0.005;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP1800, F.E] = 254.57;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP1800, F.MAX] = 528;
            formules[F.TECHNOSLURRY, F.SHP, F.SHP1800, F.VITESSE] = 1800;
            #endregion


            #region TSURUMI
            //=================================================================
            ////Tsurumi
            //=================================================================

            #region GSZ
            //**************SERIE GSZ****************************************//
            //GSZ_5_22_6    -   0
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.A] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.B] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.C] = -0.000006;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.D] = -0.0149;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.E] = 74.39;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.MAX] = 2200;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.VITESSE] = 1800;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.rA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.rB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.rC] = -0.00003;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.rD] = 0.0877;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.rE] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.rMIN] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.pA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.pB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.pC] = -0.000003;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.pD] = 0.0128;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_22_6, F.pE] = 14.632;
            //GSZ_5_37_4H   -   1
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.A] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.B] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.C] = -0.00008;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.D] = -0.0253;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.E] = 193.84;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.MAX] = 1260;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rC] = -0.0001;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rD] = 0.1411;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rE] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rMIN] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rC] = -0.00003;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rD] = 0.0451;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4H, F.rE] = 29.247;
            //GSZ_5_37_4   -   2
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.A] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.B] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.C] = -0.00003;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.D] = 0.0006;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.E] = 143.92;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.MAX] = 2080;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.rA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.rB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.rC] = -0.00005;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.rD] = 0.1124;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.rE] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.rMIN] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.pA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.pB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.pC] = -0.000006;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.pD] = 0.0224;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_4, F.pE] = 28.93;
            //GSZ_5_37_6    -   3
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.A] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.B] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.C] = -0.000005;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.D] = -0.0122;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.E] = 89.893;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.MAX] = 2500;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.rA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.rB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.rC] = -0.00002;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.rD] = 0.0634;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.rE] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.rMIN] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.pA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.pB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.pC] = -0.000006;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.pD] = 0.0222;
            formules[F.TSURUMI, F.GSZ, F.GSZ_5_37_6, F.pE] = 23.393;
            //GSZ_4_45_4    -   4
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.A] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.B] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.C] = -0.00002;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.D] = 0.0047;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.E] = 150.12;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.MAX] = 2400;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.rA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.rB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.rC] = -0.00003;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.rD] = 0.0931;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.rE] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.rMIN] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.pA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.pB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.pC] = -0.000006;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.pD] = 0.0205;
            formules[F.TSURUMI, F.GSZ, F.GSZ_4_45_4, F.pE] = 40.275;
            //GSZ_55_4    -   5
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.A] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.B] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.C] = -0.00002;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.D] = -0.0008;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.E] = 163.96;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.MAX] = 2600;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.rA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.rB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.rC] = -0.00003;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.rD] = 0.0863;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.rE] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.rMIN] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.pA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.pB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.pC] = -0.000007;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.pD] = 0.0293;
            formules[F.TSURUMI, F.GSZ, F.GSZ_55_4, F.pE] = 40.182;
            //GSZ_75_4    -   6
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.A] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.B] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.C] = -0.00001;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.D] = -0.0091;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.E] = 172.5;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.MAX] = 3200;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.VITESSE] = 1800;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.rA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.rB] = -0.000000003;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.rC] = -0.000005;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.rD] = 0.0542;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.rE] = -1.1805;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.rMIN] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.pA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.pB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.pC] = -0.000008;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.pD] = 0.0346;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4, F.pE] = 52.5;
            //GSZ_75_4L    -   7
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.A] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.B] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.C] = -0.000001;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.D] = -0.0211;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.E] = 160.23;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.MAX] = 4500;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.VITESSE] = 1800;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.rA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.rB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.rC] = -0.000008;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.rD] = 0.0464;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.rE] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.rMIN] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.pA] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.pB] = 0;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.pC] = -0.000002;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.pD] = 0.0151;
            formules[F.TSURUMI, F.GSZ, F.GSZ_75_4L, F.pE] = 73.286;
            #endregion

            #region LH
            //**************SERIE LH*****************************************//
            //LH33_0    -   0
            formules[F.TSURUMI, F.LH, F.LH33_0, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.C] = -0.0004;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.D] = -0.1217;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.E] = 72.988;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.MAX] = 280;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.rA] = 0;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.rB] = 0;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.rC] = -0.0022;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.rD] = 0.6411;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.pC] = -0.00003;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.pD] = 0.0157;
            formules[F.TSURUMI, F.LH, F.LH33_0, F.pE] = 2.0912;
            //LH422    -   1
            formules[F.TSURUMI, F.LH, F.LH422, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH422, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH422, F.C] = -0.0005;
            formules[F.TSURUMI, F.LH, F.LH422, F.D] = 0.0517;
            formules[F.TSURUMI, F.LH, F.LH422, F.E] = 227.4;
            formules[F.TSURUMI, F.LH, F.LH422, F.MAX] = 550;
            formules[F.TSURUMI, F.LH, F.LH422, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH422, F.rA] = 0;
            formules[F.TSURUMI, F.LH, F.LH422, F.rB] = 0;
            formules[F.TSURUMI, F.LH, F.LH422, F.rC] = -0.0004;
            formules[F.TSURUMI, F.LH, F.LH422, F.rD] = 0.3292;
            formules[F.TSURUMI, F.LH, F.LH422, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH422, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH422, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH422, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH422, F.pC] = -0.00006;
            formules[F.TSURUMI, F.LH, F.LH422, F.pD] = 0.0609;
            formules[F.TSURUMI, F.LH, F.LH422, F.pE] = 13.091;
            //LH430    -   2
            formules[F.TSURUMI, F.LH, F.LH430, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH430, F.B] = -0.000002;
            formules[F.TSURUMI, F.LH, F.LH430, F.C] = 0.0007;
            formules[F.TSURUMI, F.LH, F.LH430, F.D] = -0.1518;
            formules[F.TSURUMI, F.LH, F.LH430, F.E] = 282.5;
            formules[F.TSURUMI, F.LH, F.LH430, F.MAX] = 600;
            formules[F.TSURUMI, F.LH, F.LH430, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH430, F.rA] = 0;
            formules[F.TSURUMI, F.LH, F.LH430, F.rB] = 0;
            formules[F.TSURUMI, F.LH, F.LH430, F.rC] = -0.0004;
            formules[F.TSURUMI, F.LH, F.LH430, F.rD] = 0.3284;
            formules[F.TSURUMI, F.LH, F.LH430, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH430, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH430, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH430, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH430, F.pC] = -0.00007;
            formules[F.TSURUMI, F.LH, F.LH430, F.pD] = 0.0782;
            formules[F.TSURUMI, F.LH, F.LH430, F.pE] = 16.396;
            //LH615    -   3
            formules[F.TSURUMI, F.LH, F.LH615, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH615, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH615, F.C] = -0.0004;
            formules[F.TSURUMI, F.LH, F.LH615, F.D] = 0.0164;
            formules[F.TSURUMI, F.LH, F.LH615, F.E] = 169.95;
            formules[F.TSURUMI, F.LH, F.LH615, F.MAX] = 600;
            formules[F.TSURUMI, F.LH, F.LH615, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH615, F.rA] = 0;
            formules[F.TSURUMI, F.LH, F.LH615, F.rB] = 0;
            formules[F.TSURUMI, F.LH, F.LH615, F.rC] = -0.0005;
            formules[F.TSURUMI, F.LH, F.LH615, F.rD] = 0.3507;
            formules[F.TSURUMI, F.LH, F.LH615, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH615, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH615, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH615, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH615, F.pC] = -0.00003;
            formules[F.TSURUMI, F.LH, F.LH615, F.pD] = 0.0364;
            formules[F.TSURUMI, F.LH, F.LH615, F.pE] = 9.7802;
            //LH622    -   4
            formules[F.TSURUMI, F.LH, F.LH622, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH622, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH622, F.C] = -0.00008;
            formules[F.TSURUMI, F.LH, F.LH622, F.D] = -0.0453;
            formules[F.TSURUMI, F.LH, F.LH622, F.E] = 168.43;
            formules[F.TSURUMI, F.LH, F.LH622, F.MAX] = 1000;
            formules[F.TSURUMI, F.LH, F.LH622, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH622, F.rA] = 0.0000000001;
            formules[F.TSURUMI, F.LH, F.LH622, F.rB] = -0.0000003;
            formules[F.TSURUMI, F.LH, F.LH622, F.rC] = 0.0001;
            formules[F.TSURUMI, F.LH, F.LH622, F.rD] = 0.1111;
            formules[F.TSURUMI, F.LH, F.LH622, F.rE] = 13.1917;
            formules[F.TSURUMI, F.LH, F.LH622, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH622, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH622, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH622, F.pC] = 0.00002;
            formules[F.TSURUMI, F.LH, F.LH622, F.pD] = 0.0184;
            formules[F.TSURUMI, F.LH, F.LH622, F.pE] = 22.469;
            //LH637    -   5
            formules[F.TSURUMI, F.LH, F.LH637, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH637, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH637, F.C] = -0.0004;
            formules[F.TSURUMI, F.LH, F.LH637, F.D] = 0.0387;
            formules[F.TSURUMI, F.LH, F.LH637, F.E] = 289.84;
            formules[F.TSURUMI, F.LH, F.LH637, F.MAX] = 650;
            formules[F.TSURUMI, F.LH, F.LH637, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH637, F.rA] = 0;
            formules[F.TSURUMI, F.LH, F.LH637, F.rB] = 0;
            formules[F.TSURUMI, F.LH, F.LH637, F.rC] = -0.0003;
            formules[F.TSURUMI, F.LH, F.LH637, F.rD] = 0.2532;
            formules[F.TSURUMI, F.LH, F.LH637, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH637, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH637, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH637, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH637, F.pC] = 0.000007;
            formules[F.TSURUMI, F.LH, F.LH637, F.pD] = 0.0421;
            formules[F.TSURUMI, F.LH, F.LH637, F.pE] = 21.736;
            //LH645    -   6
            formules[F.TSURUMI, F.LH, F.LH645, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH645, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH645, F.C] = -0.0002;
            formules[F.TSURUMI, F.LH, F.LH645, F.D] = -0.0057;
            formules[F.TSURUMI, F.LH, F.LH645, F.E] = 293.63;
            formules[F.TSURUMI, F.LH, F.LH645, F.MAX] = 800;
            formules[F.TSURUMI, F.LH, F.LH645, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH645, F.rA] = -0.0000000003;
            formules[F.TSURUMI, F.LH, F.LH645, F.rB] = 0.0000005;
            formules[F.TSURUMI, F.LH, F.LH645, F.rC] = -0.0004;
            formules[F.TSURUMI, F.LH, F.LH645, F.rD] = 0.215;
            formules[F.TSURUMI, F.LH, F.LH645, F.rE] = 0.2137;
            formules[F.TSURUMI, F.LH, F.LH645, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH645, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH645, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH645, F.pC] = -0.00006;
            formules[F.TSURUMI, F.LH, F.LH645, F.pD] = 0.0753;
            formules[F.TSURUMI, F.LH, F.LH645, F.pE] = 35.125;
            //LH675    -   7
            formules[F.TSURUMI, F.LH, F.LH675, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH675, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH675, F.C] = -0.0006;
            formules[F.TSURUMI, F.LH, F.LH675, F.D] = 0.1503;
            formules[F.TSURUMI, F.LH, F.LH675, F.E] = 424.09;
            formules[F.TSURUMI, F.LH, F.LH675, F.MAX] = 650;
            formules[F.TSURUMI, F.LH, F.LH675, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH675, F.rA] = -0.0000000004;
            formules[F.TSURUMI, F.LH, F.LH675, F.rB] = 0.0000003;
            formules[F.TSURUMI, F.LH, F.LH675, F.rC] = -0.0001;
            formules[F.TSURUMI, F.LH, F.LH675, F.rD] = 0.151;
            formules[F.TSURUMI, F.LH, F.LH675, F.rE] = -14.2754;
            formules[F.TSURUMI, F.LH, F.LH675, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH675, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH675, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH675, F.pC] = -0.00003;
            formules[F.TSURUMI, F.LH, F.LH675, F.pD] = 0.0532;
            formules[F.TSURUMI, F.LH, F.LH675, F.pE] = 74.314;
            //LH6110    -   8
            formules[F.TSURUMI, F.LH, F.LH6110, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH6110, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH6110, F.C] = -0.0004;
            formules[F.TSURUMI, F.LH, F.LH6110, F.D] = 0.0736;
            formules[F.TSURUMI, F.LH, F.LH6110, F.E] = 595.08;
            formules[F.TSURUMI, F.LH, F.LH6110, F.MAX] = 700;
            formules[F.TSURUMI, F.LH, F.LH6110, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH6110, F.rA] = 0;
            formules[F.TSURUMI, F.LH, F.LH6110, F.rB] = 0;
            formules[F.TSURUMI, F.LH, F.LH6110, F.rC] = -0.0001;
            formules[F.TSURUMI, F.LH, F.LH6110, F.rD] = 0.1547;
            formules[F.TSURUMI, F.LH, F.LH6110, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH6110, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH6110, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH6110, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH6110, F.pC] = -0.00007;
            formules[F.TSURUMI, F.LH, F.LH6110, F.pD] = 0.1297;
            formules[F.TSURUMI, F.LH, F.LH6110, F.pE] = 88.038;
            //LH837    -   9
            formules[F.TSURUMI, F.LH, F.LH837, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH837, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH837, F.C] = -0.00009;
            formules[F.TSURUMI, F.LH, F.LH837, F.D] = 0.0323;
            formules[F.TSURUMI, F.LH, F.LH837, F.E] = 166.00;
            formules[F.TSURUMI, F.LH, F.LH837, F.MAX] = 1400;
            formules[F.TSURUMI, F.LH, F.LH837, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH837, F.rA] = 0;
            formules[F.TSURUMI, F.LH, F.LH837, F.rB] = 0;
            formules[F.TSURUMI, F.LH, F.LH837, F.rC] = -0.0001;
            formules[F.TSURUMI, F.LH, F.LH837, F.rD] = 0.156;
            formules[F.TSURUMI, F.LH, F.LH837, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH837, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH837, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH837, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH837, F.pC] = -0.00002;
            formules[F.TSURUMI, F.LH, F.LH837, F.pD] = 0.0424;
            formules[F.TSURUMI, F.LH, F.LH837, F.pE] = 22.79;
            //LH845    -   10
            formules[F.TSURUMI, F.LH, F.LH845, F.A] = -0.00000000004;
            formules[F.TSURUMI, F.LH, F.LH845, F.B] = -0.00000003;
            formules[F.TSURUMI, F.LH, F.LH845, F.C] = 0.00008;
            formules[F.TSURUMI, F.LH, F.LH845, F.D] = -0.046;
            formules[F.TSURUMI, F.LH, F.LH845, F.E] = 170.57;
            formules[F.TSURUMI, F.LH, F.LH845, F.MAX] = 1400;
            formules[F.TSURUMI, F.LH, F.LH845, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH845, F.rA] = -0.00000000008;
            formules[F.TSURUMI, F.LH, F.LH845, F.rB] = 0.0000002;
            formules[F.TSURUMI, F.LH, F.LH845, F.rC] = -0.0002;
            formules[F.TSURUMI, F.LH, F.LH845, F.rD] = 0.1205;
            formules[F.TSURUMI, F.LH, F.LH845, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH845, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH845, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH845, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH845, F.pC] = -0.00003;
            formules[F.TSURUMI, F.LH, F.LH845, F.pD] = 0.0513;
            formules[F.TSURUMI, F.LH, F.LH845, F.pE] = 33.333;
            //LH875    -   11
            formules[F.TSURUMI, F.LH, F.LH875, F.A] = -0.00000000005;
            formules[F.TSURUMI, F.LH, F.LH875, F.B] = 0.0000001;
            formules[F.TSURUMI, F.LH, F.LH875, F.C] = -0.00009;
            formules[F.TSURUMI, F.LH, F.LH875, F.D] = 0.0249;
            formules[F.TSURUMI, F.LH, F.LH875, F.E] = 222.03;
            formules[F.TSURUMI, F.LH, F.LH875, F.MAX] = 1700;
            formules[F.TSURUMI, F.LH, F.LH875, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH875, F.rA] = -0.00000000002;
            formules[F.TSURUMI, F.LH, F.LH875, F.rB] = 0.00000005;
            formules[F.TSURUMI, F.LH, F.LH875, F.rC] = -0.00005;
            formules[F.TSURUMI, F.LH, F.LH875, F.rD] = 0.0764;
            formules[F.TSURUMI, F.LH, F.LH875, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH875, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH875, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH875, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH875, F.pC] = -0.00001;
            formules[F.TSURUMI, F.LH, F.LH875, F.pD] = 0.0324;
            formules[F.TSURUMI, F.LH, F.LH875, F.pE] = 81.085;
            //LH8110    -   12
            formules[F.TSURUMI, F.LH, F.LH8110, F.A] = 0;
            formules[F.TSURUMI, F.LH, F.LH8110, F.B] = 0;
            formules[F.TSURUMI, F.LH, F.LH8110, F.C] = -0.00008;
            formules[F.TSURUMI, F.LH, F.LH8110, F.D] = 0.0291;
            formules[F.TSURUMI, F.LH, F.LH8110, F.E] = 368.42;
            formules[F.TSURUMI, F.LH, F.LH8110, F.MAX] = 1600;
            formules[F.TSURUMI, F.LH, F.LH8110, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LH, F.LH8110, F.rA] = 0;
            formules[F.TSURUMI, F.LH, F.LH8110, F.rB] = 0;
            formules[F.TSURUMI, F.LH, F.LH8110, F.rC] = -0.00004;
            formules[F.TSURUMI, F.LH, F.LH8110, F.rD] = 0.0981;
            formules[F.TSURUMI, F.LH, F.LH8110, F.rE] = 0;
            formules[F.TSURUMI, F.LH, F.LH8110, F.rMIN] = 0;
            formules[F.TSURUMI, F.LH, F.LH8110, F.pA] = 0;
            formules[F.TSURUMI, F.LH, F.LH8110, F.pB] = 0;
            formules[F.TSURUMI, F.LH, F.LH8110, F.pC] = -0.00002;
            formules[F.TSURUMI, F.LH, F.LH8110, F.pD] = 0.0559;
            formules[F.TSURUMI, F.LH, F.LH8110, F.pE] = 96.255;
            #endregion

            #region LHW
            //**************SERIE LHW*****************************************//
            //LH23_0W    -   0
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.A] = 0;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.B] = 0;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.C] = -0.0044;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.D] = -0.0234;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.E] = 142.07;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.MAX] = 140;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.rA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.rB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.rC] = -0.009;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.rD] = 1.3826;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.rE] = 0;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.rMIN] = 0;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.pA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.pB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.pC] = -0.0002;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.pD] = 0.0321;
            formules[F.TSURUMI, F.LHW, F.LH23_0W, F.pE] = 2.0462;
            //LH25_5W    -   1
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.A] = 0;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.B] = 0;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.C] = -0.008;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.D] = -0.1527;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.E] = 214.89;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.MAX] = 130;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.rA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.rB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.rC] = -0.0071;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.rD] = 1.1307;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.rE] = 0;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.rMIN] = 0;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.pA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.pB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.pC] = -0.0002;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.pD] = 0.0512;
            formules[F.TSURUMI, F.LHW, F.LH25_5W, F.pE] = 4.0011;
            //LH311W    -   2
            formules[F.TSURUMI, F.LHW, F.LH311W, F.A] = 0;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.B] = 0;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.C] = -0.0047;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.D] = 0.1386;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.E] = 263.42;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.MAX] = 200;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.rA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.rB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.rC] = -0.003;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.rD] = 0.7731;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.rE] = 0;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.rMIN] = 0;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.pA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.pB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.pC] = -0.0001;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.pD] = 0.067;
            formules[F.TSURUMI, F.LHW, F.LH311W, F.pE] = 7.1441;
            //LH322W    -   3
            formules[F.TSURUMI, F.LHW, F.LH322W, F.A] = 0;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.B] = 0;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.C] = -0.0031;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.D] = -0.0502;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.E] = 356.01;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.MAX] = 240;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.rA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.rB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.rC] = -0.0014;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.rD] = 0.4805;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.rE] = 0;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.rMIN] = 0;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.pA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.pB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.pC] = -0.0001;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.pD] = 0.0843;
            formules[F.TSURUMI, F.LHW, F.LH322W, F.pE] = 16.385;
            //LH430W    -   4
            formules[F.TSURUMI, F.LHW, F.LH430W, F.A] = 0;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.B] = 0;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.C] = -0.0021;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.D] = -0.0043;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.E] = 417.07;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.MAX] = 320;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.rA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.rB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.rC] = -0.0011;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.rD] = 0.435;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.rE] = 0;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.rMIN] = 0;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.pA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.pB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.pC] = -0.0001;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.pD] = 0.0936;
            formules[F.TSURUMI, F.LHW, F.LH430W, F.pE] = 22.833;
            //LH4110W    -   5
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.A] = 0;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.B] = 0;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.C] = -0.0007;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.D] = 0.0085;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.E] = 755.07;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.MAX] = 530;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.rA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.rB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.rC] = -0.0001;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.rD] = 0.1668;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.rE] = 0;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.rMIN] = 0;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.pA] = 0;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.pB] = 0;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.pC] = -0.00002;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.pD] = 0.0759;
            formules[F.TSURUMI, F.LHW, F.LH4110W, F.pE] = 114.25;
            #endregion

            #region B
            //**************SERIE B*****************************************//
            //B50B2_75S    -   0
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.C] = -0.0013;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.D] = -0.3378;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.E] = 48.63;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.MAX] = 90;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.rC] = -0.0133;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.rD] = 1.4146;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.pC] = -0.00003;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.pD] = 0.0041;
            formules[F.TSURUMI, F.b, F.B50B2_75S, F.pE] = 0.8398;
            //B50B2_75H    -   1
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.C] = -0.0013;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.D] = -0.3378;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.E] = 48.63;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.MAX] = 90;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.rC] = -0.0133;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.rD] = 1.4064;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.pC] = -0.00003;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.pD] = 0.004;
            formules[F.TSURUMI, F.b, F.B50B2_75H, F.pE] = 0.843;
            //B50B2_75    -   2
            formules[F.TSURUMI, F.b, F.B50B2_75, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.C] = -0.0012;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.D] = -0.1595;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.E] = 38.772;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.MAX] = 110;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.rC] = -0.009;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.rD] = 1.2064;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.pC] = -0.00002;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.pD] = 0.0022;
            formules[F.TSURUMI, F.b, F.B50B2_75, F.pE] = 0.8609;
            //B80B21_5    -   3
            formules[F.TSURUMI, F.b, F.B80B21_5, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.C] = 0.0000006;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.D] = -0.2146;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.E] = 55.184;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.MAX] = 220;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.rC] = -0.0029;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.rD] = 0.7375;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.pC] = -0.00002;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.pD] = 0.0044;
            formules[F.TSURUMI, F.b, F.B80B21_5, F.pE] = 1.6117;
            //B100B42_2    -   4
            formules[F.TSURUMI, F.b, F.B100B42_2, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.C] = 0.00004;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.D] = -0.1538;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.E] = 58.5;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.MAX] = 360;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.rC] = -0.0013;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.rD] = 0.5246;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.pC] = -0.000006;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.pD] = 0.0046;
            formules[F.TSURUMI, F.b, F.B100B42_2, F.pE] = 1.9967;
            //B100B43_7H    -   5
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.C] = 0.00004;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.D] = -0.1538;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.E] = 58.5;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.MAX] = 360;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.rC] = -0.0013;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.rD] = 0.5246;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.pC] = -0.000006;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.pD] = 0.0046;
            formules[F.TSURUMI, F.b, F.B100B43_7H, F.pE] = 1.9967;
            //B100B43_7    -   6
            formules[F.TSURUMI, F.b, F.B100B43_7, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.C] = 0.00004;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.D] = -0.1538;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.E] = 58.5;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.MAX] = 360;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.rC] = -0.0013;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.rD] = 0.5246;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.pC] = -0.000006;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.pD] = 0.0046;
            formules[F.TSURUMI, F.b, F.B100B43_7, F.pE] = 1.9967;
            //B150B63_7    -   7
            formules[F.TSURUMI, F.b, F.B150B63_7, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.C] = -0.000006;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.D] = -0.0142;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.E] = 28.815;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.MAX] = 1050;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.rC] = -0.0001;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.rD] = 0.1367;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.rE] = 17.167;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.rMIN] = 260;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.pC] = -0.000003;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.pD] = 0.004;
            formules[F.TSURUMI, F.b, F.B150B63_7, F.pE] = 2.7902;
            //B100B45_5    -   8
            formules[F.TSURUMI, F.b, F.B100B45_5, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.C] = -0.000007;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.D] = -0.141;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.E] = 87.902;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.MAX] = 540;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.rC] = -0.0006;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.rD] = 0.3583;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.pC] = -0.000003;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.pD] = 0.0058;
            formules[F.TSURUMI, F.b, F.B100B45_5, F.pE] = 5.2976;
            //B100B47_5    -   9
            formules[F.TSURUMI, F.b, F.B100B47_5, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.C] = -0.000007;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.D] = -0.141;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.E] = 87.902;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.MAX] = 540;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.rC] = -0.0006;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.rD] = 0.3583;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.pC] = -0.000003;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.pD] = 0.0058;
            formules[F.TSURUMI, F.b, F.B100B47_5, F.pE] = 5.2976;
            //B150B47_5H    -   10
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.C] = 0.0000003;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.D] = -0.0538;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.E] = 71.143;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.MAX] = 930;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.rC] = -0.0001;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.rD] = 0.1693;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.rE] = 9.7619;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.rMIN] = 130;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.pC] = -0.000006;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.pD] = 0.1008;
            formules[F.TSURUMI, F.b, F.B150B47_5H, F.pE] = 6.6631;
            //B150B47_5L    -   11
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.C] = -0.0000008;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.D] = -0.0208;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.E] = 42.414;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.MAX] = 1300;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.rC] = -0.00006;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.rD] = 0.1188;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.rMIN] = 260;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.pC] = -0.0000002;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.pD] = 0.0004;
            formules[F.TSURUMI, F.b, F.B150B47_5L, F.pE] = 8.8287;
            //B150B411    -   12
            formules[F.TSURUMI, F.b, F.B150B411, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B150B411, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B150B411, F.C] = -0.00002;
            formules[F.TSURUMI, F.b, F.B150B411, F.D] = -0.0355;
            formules[F.TSURUMI, F.b, F.B150B411, F.E] = 82.655;
            formules[F.TSURUMI, F.b, F.B150B411, F.MAX] = 1200;
            formules[F.TSURUMI, F.b, F.B150B411, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B150B411, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B150B411, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B150B411, F.rC] = -0.0001;
            formules[F.TSURUMI, F.b, F.B150B411, F.rD] = 0.1639;
            formules[F.TSURUMI, F.b, F.B150B411, F.rE] = 9.4;
            formules[F.TSURUMI, F.b, F.B150B411, F.rMIN] = 120;
            formules[F.TSURUMI, F.b, F.B150B411, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B150B411, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B150B411, F.pC] = -0.000007;
            formules[F.TSURUMI, F.b, F.B150B411, F.pD] = 0.0134;
            formules[F.TSURUMI, F.b, F.B150B411, F.pE] = 7.44;

            //Manque la 150B415 - 13
            formules[F.TSURUMI, F.b, F.B150B415, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B150B415, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B150B415, F.C] = -0.00002;
            formules[F.TSURUMI, F.b, F.B150B415, F.D] = -0.0355;
            formules[F.TSURUMI, F.b, F.B150B415, F.E] = 82.655;
            formules[F.TSURUMI, F.b, F.B150B415, F.MAX] = 1200;
            formules[F.TSURUMI, F.b, F.B150B415, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B150B415, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B150B415, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B150B415, F.rC] = -0.0001;
            formules[F.TSURUMI, F.b, F.B150B415, F.rD] = 0.1639;
            formules[F.TSURUMI, F.b, F.B150B415, F.rE] = 9.4;
            formules[F.TSURUMI, F.b, F.B150B415, F.rMIN] = 120;
            formules[F.TSURUMI, F.b, F.B150B415, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B150B415, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B150B415, F.pC] = -0.000006;
            formules[F.TSURUMI, F.b, F.B150B415, F.pD] = 0.0137;
            formules[F.TSURUMI, F.b, F.B150B415, F.pE] = 10.453;

            //B150BK422    -   14
            formules[F.TSURUMI, F.b, F.B150BK422, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B150BK422, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B150BK422, F.C] = -0.00001;
            formules[F.TSURUMI, F.b, F.B150BK422, F.D] = -0.0466;
            formules[F.TSURUMI, F.b, F.B150BK422, F.E] = 124.37;
            formules[F.TSURUMI, F.b, F.B150BK422, F.MAX] = 1200;
            formules[F.TSURUMI, F.b, F.B150BK422, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B150BK422, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B150BK422, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B150BK422, F.rC] = -0.00009;
            formules[F.TSURUMI, F.b, F.B150BK422, F.rD] = 0.1553;
            formules[F.TSURUMI, F.b, F.B150BK422, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B150BK422, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B150BK422, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B150BK422, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B150BK422, F.pC] = -0.000004;
            formules[F.TSURUMI, F.b, F.B150BK422, F.pD] = 0.0149;
            formules[F.TSURUMI, F.b, F.B150BK422, F.pE] = 16.011;
            //B150BK437    -   15
            formules[F.TSURUMI, F.b, F.B150BK437, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B150BK437, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B150BK437, F.C] = -0.00003;
            formules[F.TSURUMI, F.b, F.B150BK437, F.D] = -0.0113;
            formules[F.TSURUMI, F.b, F.B150BK437, F.E] = 136.31;
            formules[F.TSURUMI, F.b, F.B150BK437, F.MAX] = 1600;
            formules[F.TSURUMI, F.b, F.B150BK437, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B150BK437, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B150BK437, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B150BK437, F.rC] = -0.00004;
            formules[F.TSURUMI, F.b, F.B150BK437, F.rD] = 0.0941;
            formules[F.TSURUMI, F.b, F.B150BK437, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B150BK437, F.rMIN] = 0;
            formules[F.TSURUMI, F.b, F.B150BK437, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B150BK437, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B150BK437, F.pC] = -0.00001;
            formules[F.TSURUMI, F.b, F.B150BK437, F.pD] = 0.0283;
            formules[F.TSURUMI, F.b, F.B150BK437, F.pE] = 30.794;
            //B200B47_5    -   16
            formules[F.TSURUMI, F.b, F.B200B47_5, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.C] = -0.000002;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.D] = -0.0185;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.E] = 41.837;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.MAX] = 1300;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.rC] = -0.00006;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.rD] = 0.1203;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.rE] = 0;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.rMIN] = 260;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.pC] = -0.0000002;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.pD] = 0.0003;
            formules[F.TSURUMI, F.b, F.B200B47_5, F.pE] = 8.8464;
            //B200B411    -   17
            formules[F.TSURUMI, F.b, F.B200B411, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B200B411, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B200B411, F.C] = -0.000002;
            formules[F.TSURUMI, F.b, F.B200B411, F.D] = -0.0289;
            formules[F.TSURUMI, F.b, F.B200B411, F.E] = 69.357;
            formules[F.TSURUMI, F.b, F.B200B411, F.MAX] = 1700;
            formules[F.TSURUMI, F.b, F.B200B411, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B200B411, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B200B411, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B200B411, F.rC] = -0.00004;
            formules[F.TSURUMI, F.b, F.B200B411, F.rD] = 0.0935;
            formules[F.TSURUMI, F.b, F.B200B411, F.rE] = 14.661;
            formules[F.TSURUMI, F.b, F.B200B411, F.rMIN] = 240;
            formules[F.TSURUMI, F.b, F.B200B411, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B200B411, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B200B411, F.pC] = -0.000007;
            formules[F.TSURUMI, F.b, F.B200B411, F.pD] = 0.014;
            formules[F.TSURUMI, F.b, F.B200B411, F.pE] = 7.7964;
            //B200B415    -   18
            formules[F.TSURUMI, F.b, F.B200B415, F.A] = 0;
            formules[F.TSURUMI, F.b, F.B200B415, F.B] = 0;
            formules[F.TSURUMI, F.b, F.B200B415, F.C] = -0.000008;
            formules[F.TSURUMI, F.b, F.B200B415, F.D] = -0.0206;
            formules[F.TSURUMI, F.b, F.B200B415, F.E] = 81.119;
            formules[F.TSURUMI, F.b, F.B200B415, F.MAX] = 1850;
            formules[F.TSURUMI, F.b, F.B200B415, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.b, F.B200B415, F.rA] = 0;
            formules[F.TSURUMI, F.b, F.B200B415, F.rB] = 0;
            formules[F.TSURUMI, F.b, F.B200B415, F.rC] = -0.00005;
            formules[F.TSURUMI, F.b, F.B200B415, F.rD] = 0.1167;
            formules[F.TSURUMI, F.b, F.B200B415, F.rE] = 2.619;
            formules[F.TSURUMI, F.b, F.B200B415, F.rMIN] = 240;
            formules[F.TSURUMI, F.b, F.B200B415, F.pA] = 0;
            formules[F.TSURUMI, F.b, F.B200B415, F.pB] = 0;
            formules[F.TSURUMI, F.b, F.B200B415, F.pC] = -0.000004;
            formules[F.TSURUMI, F.b, F.B200B415, F.pD] = 0.0084;
            formules[F.TSURUMI, F.b, F.B200B415, F.pE] = 14.648;
            #endregion

            #region BZ
            //**************SERIE BZ*****************************************//
            //BZ100BZ41_5    -   0
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.A] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.B] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.C] = 0.00005;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.D] = -0.0664;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.E] = 27.845;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.MAX] = 350;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.rA] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.rB] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.rC] = -0.0008;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.rD] = 0.3966;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.rE] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.rMIN] = 100;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.pA] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.pB] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.pC] = 0.0000003;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.pD] = 0.0012;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ41_5, F.pE] = 1.4988;
            //BZ100BZ42_2    -   1
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.A] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.B] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.C] = 0.00005;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.D] = -0.0664;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.E] = 27.845;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.MAX] = 350;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.rA] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.rB] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.rC] = -0.0008;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.rD] = 0.3966;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.rE] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.rMIN] = 100;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.pA] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.pB] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.pC] = 0.0000003;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.pD] = 0.0012;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ42_2, F.pE] = 1.4988;
            //BZ100BZ43_7    -   2
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.A] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.B] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.C] = 0.00005;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.D] = -0.0664;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.E] = 27.845;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.MAX] = 350;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.rA] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.rB] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.rC] = -0.0008;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.rD] = 0.3966;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.rE] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.pA] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.pB] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.pC] = 0.0000003;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.pD] = 0.0012;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ43_7, F.pE] = 1.4988;
            //BZ100BZ411    -   3
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.A] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.B] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.C] = 0.00004;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.D] = -0.1134;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.E] = 100.5;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.MAX] = 900;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.rA] = -0.0000000009;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.rB] = 0.0000008;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.rC] = -0.0008;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.rD] = 0.3615;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.rE] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.rMIN] = 260;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.pA] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.pB] = 0;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.pC] = -0.000004;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.pD] = 0.0106;
            formules[F.TSURUMI, F.BZ, F.BZ100BZ411, F.pE] = 6.4573;
            #endregion

            #region C_HH
            //**************SERIE C_HH*****************************************//
            //C_HH80C22_2    -   0
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.A] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.B] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.C] = -0.0002;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.D] = -0.2203;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.E] = 71.846;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.MAX] = 240;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.rA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.rB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.rC] = -0.0026;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.rD] = 0.6995;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.rE] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.pA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.pB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.pC] = -0.00003;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.pD] = 0.0097;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C22_2, F.pE] = 2.0492;
            //C_HH80C23_7    -   1
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.A] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.B] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.C] = -0.0002;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.D] = -0.2203;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.E] = 71.846;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.MAX] = 240;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.rA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.rB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.rC] = -0.0026;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.rD] = 0.6995;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.rE] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.pA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.pB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.pC] = -0.00003;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.pD] = 0.0097;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C23_7, F.pE] = 2.0492;
            //C_HH80C25_5    -   2
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.A] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.B] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.C] = -0.0006;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.D] = -0.0455;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.E] = 124.99;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.MAX] = 400;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.rA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.rB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.rC] = -0.001;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.rD] = 0.4701;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.rE] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.pA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.pB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.pC] = -0.00004;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.pD] = 0.0164;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C25_5, F.pE] = 6.8818;
            //C_HH80C27_5    -   3
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.A] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.B] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.C] = -0.0006;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.D] = -0.0455;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.E] = 124.99;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.MAX] = 400;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.rA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.rB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.rC] = -0.001;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.rD] = 0.4701;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.rE] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.pA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.pB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.pC] = -0.00004;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.pD] = 0.0164;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C27_5, F.pE] = 6.8818;
            //C_HH80C211    -   4
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.A] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.B] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.C] = -0.0003;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.D] = -0.1897;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.E] = 166.66;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.MAX] = 300;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.rA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.rB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.rC] = -0.0007;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.rD] = 0.3702;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.rE] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.rMIN] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.pA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.pB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.pC] = -0.00002;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.pD] = 0.0121;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C211, F.pE] = 11.066;
            //C_HH80C215    -   5
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.A] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.B] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.C] = 0.00001;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.D] = -0.2831;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.E] = 186.97;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.MAX] = 300;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.rA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.rB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.rC] = -0.0005;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.rD] = 0.2986;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.rE] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.rMIN] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.pA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.pB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.pC] = -0.00001;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.pD] = 0.0139;
            formules[F.TSURUMI, F.C_HH, F.C_HH80C215, F.pE] = 15.889;
            //C_HH100C222    -   6
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.A] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.B] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.C] = -0.0002;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.D] = -0.1777;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.E] = 229.45;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.MAX] = 450;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.VITESSE] = 450;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.rA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.rB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.rC] = -0.0003;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.rD] = 0.2246;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.rE] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.rMIN] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.pA] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.pB] = 0;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.pC] = -0.00002;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.pD] = 0.021;
            formules[F.TSURUMI, F.C_HH, F.C_HH100C222, F.pE] = 24.377;
            #endregion

            #region C
            //**************SERIE C*****************************************//
            //C50C2_75    -   0
            formules[F.TSURUMI, F.c, F.C50C2_75, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.C] = -0.0002;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.D] = -0.3277;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.E] = 37.218;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.MAX] = 90;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.rC] = -0.0098;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.rD] = 1.0648;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.pC] = 0.00002;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.pD] = -0.0025;
            formules[F.TSURUMI, F.c, F.C50C2_75, F.pE] = 0.9505;
            //C50C4_75    -   1
            formules[F.TSURUMI, F.c, F.C50C4_75, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.C] = -0.0005;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.D] = -0.1668;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.E] = 27.671;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.MAX] = 110;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.rC] = -0.013;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.rD] = 1.5989;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.pC] = -0.000003;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.pD] = 0.001;
            formules[F.TSURUMI, F.c, F.C50C4_75, F.pE] = 0.8596;
            //C80C21_5    -   2
            formules[F.TSURUMI, F.c, F.C80C21_5, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.C] = -0.00004;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.D] = -0.1811;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.E] = 42.927;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.MAX] = 190;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.rC] = -0.003;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.rD] = 0.6869;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.pC] = -0.00001;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.pD] = 0.0031;
            formules[F.TSURUMI, F.c, F.C80C21_5, F.pE] = 1.4374;
            //C80C41_5    -   3
            formules[F.TSURUMI, F.c, F.C80C41_5, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.C] = -0.0007;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.D] = -0.0403;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.E] = 34.545;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.MAX] = 170;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.rC] = -0.0037;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.rD] = 0.7181;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.pC] = 0.00002;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.pD] = 0.0004;
            formules[F.TSURUMI, F.c, F.C80C41_5, F.pE] = 1.4416;
            //C100C42_2    -   4
            formules[F.TSURUMI, F.c, F.C100C42_2, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.C] = 0.00005;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.D] = -0.1386;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.E] = 48.524;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.MAX] = 320;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.rC] = -0.0013;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.rD] = 0.473;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.pC] = -0.000004;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.pD] = 0.0046;
            formules[F.TSURUMI, F.c, F.C100C42_2, F.pE] = 1.8679;
            //C100C43_7    -   5
            formules[F.TSURUMI, F.c, F.C100C43_7, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.C] = -0.00002;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.D] = -0.0897;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.E] = 55.682;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.MAX] = 460;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.rC] = -0.0006;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.rD] = 0.3431;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.pC] = -0.000008;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.pD] = 0.0056;
            formules[F.TSURUMI, F.c, F.C100C43_7, F.pE] = 3.5177;
            //C100C45_5    -   6
            formules[F.TSURUMI, F.c, F.C100C45_5, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.C] = -0.000006;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.D] = -0.0865;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.E] = 61.758;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.MAX] = 600;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.rC] = -0.0004;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.rD] = 0.2571;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.pC] = -0.000006;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.pD] = 0.006;
            formules[F.TSURUMI, F.c, F.C100C45_5, F.pE] = 4.9791;
            //C100C47_5    -   7
            formules[F.TSURUMI, F.c, F.C100C47_5, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.C] = -0.000006;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.D] = -0.0865;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.E] = 61.758;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.MAX] = 600;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.rC] = -0.0004;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.rD] = 0.2571;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.pC] = -0.000006;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.pD] = 0.006;
            formules[F.TSURUMI, F.c, F.C100C47_5, F.pE] = 4.9791;
            //C100C411    -   8
            formules[F.TSURUMI, F.c, F.C100C411, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C100C411, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C100C411, F.C] = -0.00003;
            formules[F.TSURUMI, F.c, F.C100C411, F.D] = -0.073;
            formules[F.TSURUMI, F.c, F.C100C411, F.E] = 84.262;
            formules[F.TSURUMI, F.c, F.C100C411, F.MAX] = 720;
            formules[F.TSURUMI, F.c, F.C100C411, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C100C411, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C100C411, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C100C411, F.rC] = -0.0003;
            formules[F.TSURUMI, F.c, F.C100C411, F.rD] = 0.2249;
            formules[F.TSURUMI, F.c, F.C100C411, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C100C411, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C100C411, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C100C411, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C100C411, F.pC] = -0.000003;
            formules[F.TSURUMI, F.c, F.C100C411, F.pD] = 0.0078;
            formules[F.TSURUMI, F.c, F.C100C411, F.pE] = 8.4868;
            //C100C415    -   9
            formules[F.TSURUMI, F.c, F.C100C415, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C100C415, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C100C415, F.C] = -0.00003;
            formules[F.TSURUMI, F.c, F.C100C415, F.D] = -0.0505;
            formules[F.TSURUMI, F.c, F.C100C415, F.E] = 91.569;
            formules[F.TSURUMI, F.c, F.C100C415, F.MAX] = 570;
            formules[F.TSURUMI, F.c, F.C100C415, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C100C415, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C100C415, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C100C415, F.rC] = -0.0002;
            formules[F.TSURUMI, F.c, F.C100C415, F.rD] = 0.2055;
            formules[F.TSURUMI, F.c, F.C100C415, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C100C415, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C100C415, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C100C415, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C100C415, F.pC] = 0.000003;
            formules[F.TSURUMI, F.c, F.C100C415, F.pD] = 0.0047;
            formules[F.TSURUMI, F.c, F.C100C415, F.pE] = 11.178;
            //C150C611    -   10
            formules[F.TSURUMI, F.c, F.C150C611, F.A] = 0;
            formules[F.TSURUMI, F.c, F.C150C611, F.B] = 0;
            formules[F.TSURUMI, F.c, F.C150C611, F.C] = -0.000006;
            formules[F.TSURUMI, F.c, F.C150C611, F.D] = -0.0149;
            formules[F.TSURUMI, F.c, F.C150C611, F.E] = 48.401;
            formules[F.TSURUMI, F.c, F.C150C611, F.MAX] = 1450;
            formules[F.TSURUMI, F.c, F.C150C611, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.c, F.C150C611, F.rA] = 0;
            formules[F.TSURUMI, F.c, F.C150C611, F.rB] = 0;
            formules[F.TSURUMI, F.c, F.C150C611, F.rC] = -0.00006;
            formules[F.TSURUMI, F.c, F.C150C611, F.rD] = 0.1194;
            formules[F.TSURUMI, F.c, F.C150C611, F.rE] = 0;
            formules[F.TSURUMI, F.c, F.C150C611, F.rMIN] = 0;
            formules[F.TSURUMI, F.c, F.C150C611, F.pA] = 0;
            formules[F.TSURUMI, F.c, F.C150C611, F.pB] = 0;
            formules[F.TSURUMI, F.c, F.C150C611, F.pC] = -0.000002;
            formules[F.TSURUMI, F.c, F.C150C611, F.pD] = 0.0058;
            formules[F.TSURUMI, F.c, F.C150C611, F.pE] = 8.9368;
            #endregion

            #region GPN
            //**************SERIE GPN*****************************************//
            //GPN35_5    -   0
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.A] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.B] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.C] = -0.0001;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.D] = -0.003;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.E] = 52.35;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.MAX] = 500;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.rA] = -0.000000003;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.rB] = 0.000003;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.rC] = -0.0013;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.rD] = 0.3556;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.rE] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.pA] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.pB] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.pC] = -0.00003;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.pD] = 0.0199;
            formules[F.TSURUMI, F.GPN, F.GPN35_5, F.pE] = 3.2853;
            //GPN411    -   1
            formules[F.TSURUMI, F.GPN, F.GPN411, F.A] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.B] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.C] = -0.00004;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.D] = -0.0191;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.E] = 63.588;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.MAX] = 850;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.rA] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.rB] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.rC] = -0.0001;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.rD] = 0.1672;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.rE] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.rMIN] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.pA] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.pB] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.pC] = -0.00001;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.pD] = 0.0137;
            formules[F.TSURUMI, F.GPN, F.GPN411, F.pE] = 8.0527;
            //GPN415    -   1
            formules[F.TSURUMI, F.GPN, F.GPN415, F.A] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.B] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.C] = -0.00004;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.D] = -0.0121;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.E] = 80.78;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.MAX] = 1150;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.rA] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.rB] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.rC] = -0.0002;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.rD] = 0.1882;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.rE] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.rMIN] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.pA] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.pB] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.pC] = -0.000008;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.pD] = 0.0142;
            formules[F.TSURUMI, F.GPN, F.GPN415, F.pE] = 8.969;
            //GPN622    -   2
            formules[F.TSURUMI, F.GPN, F.GPN622, F.A] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.B] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.C] = -0.00004;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.D] = -0.0056;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.E] = 91.914;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.MAX] = 1350;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.rA] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.rB] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.rC] = -0.00009;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.rD] = 0.1323;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.rE] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.rMIN] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.pA] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.pB] = 0;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.pC] = -0.000009;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.pD] = 0.0176;
            formules[F.TSURUMI, F.GPN, F.GPN622, F.pE] = 16.675;
            #endregion

            #region HS
            //**************SERIE HS*****************************************//
            //HS2_4S    -   0
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.A] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.B] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.C] = -0.0064;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.D] = -0.3092;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.E] = 38.86;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.MAX] = 53;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.rA] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.rB] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.rC] = -0.0459;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.rD] = 2.5968;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.rE] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.rMIN] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.pA] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.pB] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.pC] = -0.00009;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.pD] = 0.0078;
            formules[F.TSURUMI, F.HS, F.HS2_4S, F.pE] = 0.3346;
            //HS3_75S    -   1
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.A] = -0.000001;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.B] = -0.0001;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.C] = 0.0113;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.D] = -0.807;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.E] = 61.953;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.MAX] = 60;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.rA] = -0.000005;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.rB] = 0.0004;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.rC] = -0.0407;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.rD] = 2.3075;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.rE] = 0;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.rMIN] = 0;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.pA] = 0;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.pB] = 0;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.pC] = -0.00002;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.pD] = 0.0079;
            formules[F.TSURUMI, F.HS, F.HS3_75S, F.pE] = 0.611;
            //HS2_55S    -   2
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.A] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.B] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.C] = -0.0041;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.D] = -0.4132;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.E] = 43.151;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.MAX] = 58;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.rA] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.rB] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.rC] = -0.0339;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.rD] = 2.462;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.rE] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.rMIN] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.pA] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.pB] = 0;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.pC] = -0.00003;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.pD] = 0.0074;
            formules[F.TSURUMI, F.HS, F.HS2_55S, F.pE] = 0.3598;
            #endregion

            #region KTD
            //**************SERIE KTD*****************************************//
            //KTD22_0    -   0
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.A] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.B] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.C] = -0.0022;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.D] = -0.2199;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.E] = 64.885;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.MAX] = 110;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.rA] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.rB] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.rC] = -0.0077;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.rD] = 1.0286;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.rE] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.pA] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.pB] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.pC] = -0.00007;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.pD] = 0.0108;
            formules[F.TSURUMI, F.KTD, F.KTD22_0, F.pE] = 1.5879;
            //KTD33_0    -   1
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.A] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.B] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.C] = -0.0013;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.D] = -0.0621;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.E] = 75.028;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.MAX] = 210;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.rA] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.rB] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.rC] = -0.0028;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.rD] = 0.665;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.rE] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.pA] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.pB] = 0;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.pC] = -0.00008;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.pD] = 0.0194;
            formules[F.TSURUMI, F.KTD, F.KTD33_0, F.pE] = 2.6077;
            #endregion

            #region KTV
            //**************SERIE KTV*****************************************//
            //KTV2_8    -   0
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.A] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.B] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.C] = -0.0037;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.D] = -0.233;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.E] = 52.448;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.MAX] = 85;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.rA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.rB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.rC] = -0.0235;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.rD] = 2.1197;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.rE] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.pA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.pB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.pC] = -0.00006;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.pD] = 0.0111;
            formules[F.TSURUMI, F.KTV, F.KTV2_8, F.pE] = 0.4664;
            //KTV2_15    -   1
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.A] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.B] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.C] = -0.0034;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.D] = -0.1768;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.E] = 68.297;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.MAX] = 110;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.rA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.rB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.rC] = -0.0106;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.rD] = 1.2773;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.rE] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.pA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.pB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.pC] = -0.00007;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.pD] = 0.0146;
            formules[F.TSURUMI, F.KTV, F.KTV2_15, F.pE] = 1.2106;
            //KTV2_22    -   2
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.A] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.B] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.C] = -0.003;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.D] = -0.2016;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.E] = 84.686;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.MAX] = 130;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.rA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.rB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.rC] = -0.0088;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.rD] = 1.1983;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.rE] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.pA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.pB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.pC] = 0.000002;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.pD] = 0.0104;
            formules[F.TSURUMI, F.KTV, F.KTV2_22, F.pE] = 1.5996;
            //KTV2_37H    -   3
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.A] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.B] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.C] = -0.0031;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.D] = -0.0866;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.E] = 113.16;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.MAX] = 125;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.rA] = -0.0000002;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.rB] = 0.00008;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.rC] = -0.0162;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.rD] = 1.4205;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.rE] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.pA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.pB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.pC] = -0.0001;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.pD] = 0.035;
            formules[F.TSURUMI, F.KTV, F.KTV2_37H, F.pE] = 1.7571;
            //KTV2_37    -   4
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.A] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.B] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.C] = -0.0017;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.D] = -0.0269;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.E] = 92.336;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.MAX] = 210;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.rA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.rB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.rC] = -0.004;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.rD] = 0.9091;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.rE] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.pA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.pB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.pC] = -0.00007;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.pD] = 0.0241;
            formules[F.TSURUMI, F.KTV, F.KTV2_37, F.pE] = 1.9958;
            //KTV2_55    -   5
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.A] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.B] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.C] = -0.0019;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.D] = -0.0188;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.E] = 119.32;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.MAX] = 230;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.rA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.rB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.rC] = -0.0027;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.rD] = 0.6912;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.rE] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.pA] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.pB] = 0;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.pC] = -0.00005;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.pD] = 0.0265;
            formules[F.TSURUMI, F.KTV, F.KTV2_55, F.pE] = 4.0016;
            #endregion

            #region KTV_SK
            //**************SERIE KTV_SK*****************************************//
            //KTV2_50    -   0
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.A] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.B] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.C] = -0.0035;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.D] = -0.1721;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.E] = 68.352;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.MAX] = 110;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.rA] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.rB] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.rC] = -0.0109;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.rD] = 1.2972;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.rE] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.pA] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.pB] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.pC] = -0.00006;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.pD] = 0.014;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_50, F.pE] = 1.2049;
            //KTV2_80    -   1
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.A] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.B] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.C] = -0.0016;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.D] = -0.0442;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.E] = 74.018;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.MAX] = 180;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.rA] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.rB] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.rC] = -0.005;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.rD] = 0.9927;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.rE] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.pA] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.pB] = 0;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.pC] = 0.00001;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.pD] = 0.0089;
            formules[F.TSURUMI, F.KTV_SK, F.KTV2_80, F.pE] = 1.7245;
            #endregion

            #region KVTE
            //**************SERIE KTVE*****************************************//
            //KTVE2_75    -   0
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.A] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.B] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.C] = -0.0037;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.D] = -0.233;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.E] = 52.448;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.MAX] = 85;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.rA] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.rB] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.rC] = -0.0238;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.rD] = 2.1398;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.rE] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.pA] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.pB] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.pC] = -0.00006;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.pD] = 0.011;
            formules[F.TSURUMI, F.KTVE, F.KTVE2_75, F.pE] = 0.4627;
            //KTVE21_5    -   1
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.A] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.B] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.C] = -0.0034;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.D] = -0.1699;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.E] = 68.305;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.MAX] = 110;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.rA] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.rB] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.rC] = -0.0109;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.rD] = 1.2941;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.rE] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.pA] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.pB] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.pC] = -0.00007;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.pD] = 0.0146;
            formules[F.TSURUMI, F.KTVE, F.KTVE21_5, F.pE] = 1.2106;
            //KTVE22_2    -   2
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.A] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.B] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.C] = -0.0032;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.D] = -0.1733;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.E] = 84.046;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.MAX] = 130;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.rA] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.rB] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.rC] = -0.0089;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.rD] = 1.215;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.rE] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.pA] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.pB] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.pC] = -0.00001;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.pD] = 0.0122;
            formules[F.TSURUMI, F.KTVE, F.KTVE22_2, F.pE] = 1.5468;
            //KTVE33_7    -   3
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.A] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.B] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.C] = -0.0017;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.D] = -0.0269;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.E] = 92.336;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.MAX] = 220;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.rA] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.rB] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.rC] = -0.004;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.rD] = 0.9074;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.rE] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.pA] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.pB] = 0;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.pC] = -0.00007;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.pD] = 0.0241;
            formules[F.TSURUMI, F.KTVE, F.KTVE33_7, F.pE] = 1.9958;
            #endregion

            #region KTZ
            //**************SERIE KTZ*****************************************//
            //KTZ21_5    -   0
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.C] = -0.0052;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.D] = 0.0227;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.E] = 74.573;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.MAX] = 105;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.rC] = -0.0104;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.rD] = 1.364;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.pC] = -0.00008;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.pD] = 0.0134;
            formules[F.TSURUMI, F.KTZ, F.KTZ21_5, F.pE] = 1.4185;
            //KTZ31_5    -   1
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.C] = -0.0014;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.D] = 0.0334;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.E] = 46.391;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.MAX] = 180;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.rC] = -0.0047;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.rD] = 0.9331;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.pC] = -0.00002;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.pD] = 0.0085;
            formules[F.TSURUMI, F.KTZ, F.KTZ31_5, F.pE] = 1.2866;
            //KTZ22_2    -   2
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.C] = -0.0032;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.D] = -0.1731;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.E] = 100.03;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.MAX] = 130;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.rC] = -0.0076;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.rD] = 1.1935;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.pC] = -0.00007;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.pD] = 0.0177;
            formules[F.TSURUMI, F.KTZ, F.KTZ22_2, F.pE] = 1.878;
            //KTZ32_2    -   3
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.C] = -0.0016;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.D] = 0.0397;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.E] = 65.308;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.MAX] = 200;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.rC] = -0.0041;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.rD] = 0.9134;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.pC] = -0.00004;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.pD] = 0.0138;
            formules[F.TSURUMI, F.KTZ, F.KTZ32_2, F.pE] = 1.7161;
            //KTZ23_7    -   4
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.C] = -0.0015;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.D] = -0.1862;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.E] = 113.31;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.MAX] = 140;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.rC] = -0.0046;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.rD] = 0.903;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.pC] = 0.00001;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.pD] = 0.0155;
            formules[F.TSURUMI, F.KTZ, F.KTZ23_7, F.pE] = 2.8746;
            //KTZ33_7    -   5
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.C] = -0.0012;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.D] = -0.1102;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.E] = 101.1;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.MAX] = 220;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.rC] = -0.0027;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.rD] = 0.7047;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.pC] = -0.00007;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.pD] = 0.018;
            formules[F.TSURUMI, F.KTZ, F.KTZ33_7, F.pE] = 3.2104;
            //KTZ43_7    -   6
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.C] = -0.0003;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.D] = -0.0417;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.E] = 60.833;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.MAX] = 380;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.rC] = -0.001;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.rD] = 0.4389;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.pC] = -0.00003;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.pD] = 0.0168;
            formules[F.TSURUMI, F.KTZ, F.KTZ43_7, F.pE] = 2.5125;
            //KTZ35_5    -   7
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.C] = -0.0013;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.D] = -0.043;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.E] = 120.85;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.MAX] = 260;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.rA] = -0.00000006;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.rB] = 0.00003;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.rC] = -0.0049;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.rD] = 0.668;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.pC] = -0.00008;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.pD] = 0.0317;
            formules[F.TSURUMI, F.KTZ, F.KTZ35_5, F.pE] = 4.4886;
            //KTZ45_5    -   8
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.C] = -0.0003;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.D] = -0.0373;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.E] = 77.467;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.MAX] = 430;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.rC] = -0.0008;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.rD] = 0.3775;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.pC] = -0.00004;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.pD] = 0.0235;
            formules[F.TSURUMI, F.KTZ, F.KTZ45_5, F.pE] = 3.7824;
            //KTZ47_5    -   9
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.C] = -0.0007;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.D] = -0.0323;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.E] = 135.96;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.MAX] = 350;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.rC] = -0.0011;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.rD] = 0.4831;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.pC] = -0.00004;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.pD] = 0.0235;
            formules[F.TSURUMI, F.KTZ, F.KTZ47_5, F.pE] = 3.7824;
            //KTZ67_5    -   10
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.C] = -0.0016;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.D] = -0.0442;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.E] = 74.018;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.MAX] = 190;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.rC] = -0.005;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.rD] = 0.9927;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.pC] = 0.00001;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.pD] = 0.0089;
            formules[F.TSURUMI, F.KTZ, F.KTZ67_5, F.pE] = 1.7245;
            //KTZ411    -   11
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.C] = -0.0007;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.D] = -0.0258;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.E] = 164.13;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.MAX] = 370;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.rC] = -0.0008;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.rD] = 0.4019;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.pC] = -0.00008;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.pD] = 0.0464;
            formules[F.TSURUMI, F.KTZ, F.KTZ411, F.pE] = 8.1458;
            //KTZ611    -   12
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.A] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.B] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.C] = -0.0002;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.D] = -0.0082;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.E] = 104.43;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.MAX] = 650;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.rA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.rB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.rC] = -0.0004;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.rD] = 0.2781;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.rE] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.rMIN] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.pA] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.pB] = 0;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.pC] = -0.00003;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.pD] = 0.0323;
            formules[F.TSURUMI, F.KTZ, F.KTZ611, F.pE] = 6.7692;
            #endregion

            #region KRS
            //**************SERIE KRS*****************************************//
            //KRS2_A3    -   0
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.C] = -0.0004;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.D] = -0.0295;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.E] = 42.829;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.MAX] = 270;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.rA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.rB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.rC] = -0.0018;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.rD] = 0.5572;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.pC] = -0.00003;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.pD] = 0.0129;
            formules[F.TSURUMI, F.KRS, F.KRS2_A3, F.pE] = 1.4311;
            //KRS2_B3    -   1
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.C] = -0.0003;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.D] = -0.0137;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.E] = 56.625;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.MAX] = 370;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.rA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.rB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.rC] = -0.0011;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.rD] = 0.4578;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.pC] = -0.00003;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.pD] = 0.0175;
            formules[F.TSURUMI, F.KRS, F.KRS2_B3, F.pE] = 1.725;
            //KRS2_A4    -   2
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.C] = -0.0002;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.D] = 0.0004;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.E] = 45.491;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.MAX] = 450;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.rA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.rB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.rC] = -0.0008;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.rD] = 0.3843;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.pC] = -0.00001;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.pD] = 0.0095;
            formules[F.TSURUMI, F.KRS, F.KRS2_A4, F.pE] = 2.7055;
            //KRS2_B4    -   3
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.C] = -0.0002;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.D] = 0.0027;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.E] = 63.706;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.MAX] = 500;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.rA] = -0.000000003;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.rB] = 0.000003;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.rC] = -0.0014;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.rD] = 0.4224;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.pC] = -0.00001;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.pD] = 0.0144;
            formules[F.TSURUMI, F.KRS, F.KRS2_B4, F.pE] = 3.4203;
            //KRS2_A6    -   4
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.C] = -0.00003;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.D] = -0.0533;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.E] = 68.824;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.MAX] = 800;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.rA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.rB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.rC] = -0.0003;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.rD] = 0.2468;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.pC] = -0.00001;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.pD] = 0.0126;
            formules[F.TSURUMI, F.KRS, F.KRS2_A6, F.pE] = 4.7485;
            //KRS2_B6    -   5
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.C] = -0.00004;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.D] = -0.0246;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.E] = 74.727;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.MAX] = 950;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.rA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.rB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.rC] = -0.0002;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.rD] = 0.1973;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.pC] = -0.00001;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.pD] = 0.0196;
            formules[F.TSURUMI, F.KRS, F.KRS2_B6, F.pE] = 5.3427;
            //KRS2_8S    -   6
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.C] = 0.0000005;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.D] = -0.0201;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.E] = 58.989;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.MAX] = 1450;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.rA] = -0.0000000001;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.rB] = 0.0000003;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.rC] = -0.0003;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.rD] = 0.2143;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.pC] = -0.000007;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.pD] = 0.0146;
            formules[F.TSURUMI, F.KRS, F.KRS2_8S, F.pE] = 6.3375;
            //KRS815    -   7
            formules[F.TSURUMI, F.KRS, F.KRS815, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.C] = -0.00002;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.D] = -0.0017;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.E] = 73.727;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.MAX] = 1650;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.rA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.rB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.rC] = -0.00007;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.rD] = 0.1358;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.pC] = -0.000007;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.pD] = 0.0161;
            formules[F.TSURUMI, F.KRS, F.KRS815, F.pE] = 10.748;
            //KRS819    -   8
            formules[F.TSURUMI, F.KRS, F.KRS819, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.C] = -0.00004;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.D] = -0.0045;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.E] = 95.25;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.MAX] = 1400;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.VITESSE] = 1800;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.rA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.rB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.rC] = -0.00009;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.rD] = 0.1401;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.pC] = -0.00001;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.pD] = 0.0233;
            formules[F.TSURUMI, F.KRS, F.KRS819, F.pE] = 13.875;
            //KRS822    -   9
            formules[F.TSURUMI, F.KRS, F.KRS822, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.C] = -0.00004;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.D] = -0.0076;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.E] = 112.88;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.MAX] = 1400;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.VITESSE] = 1800;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.rA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.rB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.rC] = -0.00008;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.rD] = 0.1444;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.pC] = -0.00001;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.pD] = 0.0224;
            formules[F.TSURUMI, F.KRS, F.KRS822, F.pE] = 15.875;
            //KRS822L    -   10
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.C] = -0.00002;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.D] = 0.0078;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.E] = 78.807;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.MAX] = 1650;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.VITESSE] = 1800;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.rA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.rB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.rC] = -0.00005;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.rD] = 0.1109;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.pC] = -0.000009;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.pD] = 0.0235;
            formules[F.TSURUMI, F.KRS, F.KRS822L, F.pE] = 13.043;
            //KRS1022    -   11
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.C] = 0.00008;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.D] = -0.1062;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.E] = 89.673;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.MAX] = 3200;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.rA] = -0.000000000005;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.rB] = 0.00000002;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.rC] = -0.00004;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.rD] = 0.0583;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.rMIN] = 1050;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.pC] = -0.000002;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.pD] = 0.0071;
            formules[F.TSURUMI, F.KRS, F.KRS1022, F.pE] = 24.034;
            //KRS1230    -   12
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.C] = 0.000008;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.D] = -0.0287;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.E] = 66.824;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.MAX] = 4300;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.rA] = -0.0000000000006;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.rB] = 0.000000004;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.rC] = -0.00001;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.rD] = 0.0406;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.rMIN] = 500;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.pA] = -0.00000000000003;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.pB] = -0.000000001;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.pC] = 0.000009;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.pD] = -0.0198;
            formules[F.TSURUMI, F.KRS, F.KRS1230, F.pE] = 48.13;
            //KRS1437    -   13
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.A] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.B] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.C] = -0.000007;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.D] = -0.0126;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.E] = 65.519;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.MAX] = 4300;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.rA] = -0.000000000001;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.rB] = 0.000000008;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.rC] = -0.00002;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.rD] = 0.0444;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.rE] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.rMIN] = 500;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.pA] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.pB] = 0;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.pC] = -0.0000008;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.pD] = 0.0059;
            formules[F.TSURUMI, F.KRS, F.KRS1437, F.pE] = 38.481;
            #endregion

            #region KRS_SK
            //**************SERIE KRS_SK*****************************************//
            //KRS2_80    -   0
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.A] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.B] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.C] = -0.0002;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.D] = -0.0108;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.E] = 47.755;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.MAX] = 450;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.rA] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.rB] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.rC] = -0.0006;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.rD] = 0.3234;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.rE] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.pA] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.pB] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.pC] = -0.00002;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.pD] = 0.0137;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_80, F.pE] = 3.0809;
            //KRS2_100    -   1
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.A] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.B] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.C] = -0.0001;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.D] = -0.0169;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.E] = 55.044;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.MAX] = 600;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.MAX] = 3600;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.rA] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.rB] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.rC] = -0.0003;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.rD] = 0.2301;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.rE] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.pA] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.pB] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.pC] = -0.00002;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.pD] = 0.014;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_100, F.pE] = 5.3071;
            //KRS2_150    -   2
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.A] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.B] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.C] = -0.00005;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.D] = -0.0375;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.E] = 78.661;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.MAX] = 850;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.rA] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.rB] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.rC] = -0.0002;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.rD] = -0.2161;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.rE] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.pA] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.pB] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.pC] = -0.00001;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.pD] = 0.0144;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_150, F.pE] = 7.5939;
            //KRS2_200    -   3
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.A] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.B] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.C] = -0.00002;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.D] = 0.0061;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.E] = 73.22;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.MAX] = 1650;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.rA] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.rB] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.rC] = -0.00006;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.rD] = 0.1192;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.rE] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.rMIN] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.pA] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.pB] = 0;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.pC] = -0.000009;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.pD] = 0.0228;
            formules[F.TSURUMI, F.KRS_SK, F.KRS2_200, F.pE] = 10.707;
            #endregion

            #region LSC
            //**************SERIE LSC*****************************************//
            //LSC1_4S    -   0
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.A] = 0;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.B] = 0;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.C] = -0.0089;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.D] = -0.3157;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.E] = 38.168;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.MAX] = 45;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.rA] = 0;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.rB] = 0;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.rC] = -0.0493;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.rD] = 2.4625;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.rE] = 0;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.rMIN] = 0;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.pA] = 0;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.pB] = 0;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.pC] = -0.00004;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.pD] = 0.007;
            formules[F.TSURUMI, F.LSC, F.LSC1_4S, F.pE] = 0.3625;
            #endregion

            #region MG
            //**************SERIE MG*****************************************//
            //MG50MG21_5    -   0
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.A] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.B] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.C] = -0.035;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.D] = 0.1512;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.E] = 78.042;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.MAX] = 38;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.rA] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.rB] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.rC] = -0.0304;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.rD] = 1.5759;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.rE] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.pA] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.pB] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.pC] = 0.0002;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.pD] = 0.005;
            formules[F.TSURUMI, F.MG, F.MG50MG21_5, F.pE] = 1.5592;
            //MG50MG22_2    -   1
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.A] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.B] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.C] = -0.0033;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.D] = -0.3211;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.E] = 80.782;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.MAX] = 85;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.rA] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.rB] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.rC] = -0.0097;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.rD] = 1.0729;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.rE] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.pA] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.pB] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.pC] = 0.0002;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.pD] = -0.0044;
            formules[F.TSURUMI, F.MG, F.MG50MG22_2, F.pE] = 1.8527;
            //MG50MG23_7    -   2
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.A] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.B] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.C] = -0.0033;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.D] = -0.3211;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.E] = 80.782;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.MAX] = 85;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.rA] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.rB] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.rC] = -0.00005;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.rD] = 0.0219;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.rE] = 3.1575;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.pA] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.pB] = 0;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.pC] = -0.00005;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.pD] = 0.0219;
            formules[F.TSURUMI, F.MG, F.MG50MG23_7, F.pE] = 3.1575;
            #endregion

            #region NK
            //**************SERIE NK*****************************************//
            //NK2_15    -   0
            formules[F.TSURUMI, F.NK, F.NK2_15, F.A] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.B] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.C] = -0.0032;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.D] = -0.1949;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.E] = 68.383;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.MAX] = 110;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.rA] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.rB] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.rC] = -0.0107;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.rD] = 1.2837;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.rE] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.rMIN] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.pA] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.pB] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.pC] = -0.00007;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.pD] = 0.0141;
            formules[F.TSURUMI, F.NK, F.NK2_15, F.pE] = 1.212;
            //NK2_22    -   1
            formules[F.TSURUMI, F.NK, F.NK2_22, F.A] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.B] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.C] = -0.0031;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.D] = -0.1929;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.E] = 84.571;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.MAX] = 130;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.rA] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.rB] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.rC] = -0.0089;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.rD] = 1.203;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.rE] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.rMIN] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.pA] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.pB] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.pC] = 0.00003;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.pD] = 0.0059;
            formules[F.TSURUMI, F.NK, F.NK2_22, F.pE] = 1.6905;
            //NK2_22L    -   2
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.A] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.B] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.C] = -0.0011;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.D] = -0.0164;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.E] = 58.413;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.MAX] = 210;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.rA] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.rB] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.rC] = -0.0037;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.rD] = 0.8561;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.rE] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.rMIN] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.pA] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.pB] = 0;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.pC] = -0.00004;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.pD] = 0.0131;
            formules[F.TSURUMI, F.NK, F.NK2_22L, F.pE] = 1.448;
            #endregion

            #region NK_SK
            //**************SERIE NK_SK*****************************************//
            //NK2_15SK    -   0
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.A] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.B] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.C] = -0.0032;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.D] = -0.1903;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.E] = 68.467;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.MAX] = 110;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.rA] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.rB] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.rC] = -0.0113;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.rD] = 1.3102;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.rE] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.rMIN] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.pA] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.pB] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.pC] = -0.00009;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.pD] = 0.0165;
            formules[F.TSURUMI, F.NK_SK, F.NK2_15SK, F.pE] = 1.1927;
            //NK2_22SK    -   1
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.A] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.B] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.C] = -0.0034;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.D] = -0.1699;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.E] = 68.305;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.MAX] = 110;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.rA] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.rB] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.rC] = -0.0107;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.rD] = 1.2843;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.rE] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.rMIN] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.pA] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.pB] = 0;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.pC] = -0.00005;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.pD] = 0.0131;
            formules[F.TSURUMI, F.NK_SK, F.NK2_22SK, F.pE] = 1.2157;
            #endregion

            #region LB
            //**************SERIE LB*****************************************//
            //LB_480    -   0
            formules[F.TSURUMI, F.LB, F.LB_480, F.A] = 0;
            formules[F.TSURUMI, F.LB, F.LB_480, F.B] = 0;
            formules[F.TSURUMI, F.LB, F.LB_480, F.C] = -0.0045;
            formules[F.TSURUMI, F.LB, F.LB_480, F.D] = -0.2436;
            formules[F.TSURUMI, F.LB, F.LB_480, F.E] = 38.852;
            formules[F.TSURUMI, F.LB, F.LB_480, F.MAX] = 63;
            formules[F.TSURUMI, F.LB, F.LB_480, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LB, F.LB_480, F.rA] = 0;
            formules[F.TSURUMI, F.LB, F.LB_480, F.rB] = 0;
            formules[F.TSURUMI, F.LB, F.LB_480, F.rC] = -0.033;
            formules[F.TSURUMI, F.LB, F.LB_480, F.rD] = 2.3054;
            formules[F.TSURUMI, F.LB, F.LB_480, F.rE] = 0;
            formules[F.TSURUMI, F.LB, F.LB_480, F.rMIN] = 0;
            formules[F.TSURUMI, F.LB, F.LB_480, F.pA] = 0;
            formules[F.TSURUMI, F.LB, F.LB_480, F.pB] = 0;
            formules[F.TSURUMI, F.LB, F.LB_480, F.pC] = -0.00005;
            formules[F.TSURUMI, F.LB, F.LB_480, F.pD] = 0.006;
            formules[F.TSURUMI, F.LB, F.LB_480, F.pE] = 0.3896;
            //LB_800    -   1
            formules[F.TSURUMI, F.LB, F.LB_800, F.A] = 0;
            formules[F.TSURUMI, F.LB, F.LB_800, F.B] = 0;
            formules[F.TSURUMI, F.LB, F.LB_800, F.C] = -0.0033;
            formules[F.TSURUMI, F.LB, F.LB_800, F.D] = -0.3566;
            formules[F.TSURUMI, F.LB, F.LB_800, F.E] = 58.507;
            formules[F.TSURUMI, F.LB, F.LB_800, F.MAX] = 83;
            formules[F.TSURUMI, F.LB, F.LB_800, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LB, F.LB_800, F.rA] = 0;
            formules[F.TSURUMI, F.LB, F.LB_800, F.rB] = 0;
            formules[F.TSURUMI, F.LB, F.LB_800, F.rC] = -0.0256;
            formules[F.TSURUMI, F.LB, F.LB_800, F.rD] = 2.2548;
            formules[F.TSURUMI, F.LB, F.LB_800, F.rE] = 0;
            formules[F.TSURUMI, F.LB, F.LB_800, F.rMIN] = 0;
            formules[F.TSURUMI, F.LB, F.LB_800, F.pA] = 0;
            formules[F.TSURUMI, F.LB, F.LB_800, F.pB] = 0;
            formules[F.TSURUMI, F.LB, F.LB_800, F.pC] = -0.00005;
            formules[F.TSURUMI, F.LB, F.LB_800, F.pD] = 0.0092;
            formules[F.TSURUMI, F.LB, F.LB_800, F.pE] = 0.5211;
            //LBT_800    -   2
            formules[F.TSURUMI, F.LB, F.LBT_800, F.A] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.B] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.C] = -0.0033;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.D] = -0.3566;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.E] = 58.507;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.MAX] = 83;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.rA] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.rB] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.rC] = -0.0256;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.rD] = 2.2548;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.rE] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.rMIN] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.pA] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.pB] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.pC] = -0.00005;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.pD] = 0.0092;
            formules[F.TSURUMI, F.LB, F.LBT_800, F.pE] = 0.5211;
            //LB_1500    -   3
            formules[F.TSURUMI, F.LB, F.LB_1500, F.A] = 0;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.B] = 0;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.C] = -0.0032;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.D] = -0.1934;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.E] = 68.331;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.MAX] = 110;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.rA] = 0;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.rB] = 0;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.rC] = -0.0125;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.rD] = 1.4831;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.rE] = 0;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.rMIN] = 0;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.pA] = 0;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.pB] = 0;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.pC] = -0.00001;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.pD] = 0.0072;
            formules[F.TSURUMI, F.LB, F.LB_1500, F.pE] = 1.1755;
            //LBT_1500    -   4
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.A] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.B] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.C] = -0.0032;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.D] = -0.1934;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.E] = 68.331;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.MAX] = 110;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.rA] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.rB] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.rC] = -0.0125;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.rD] = 1.4831;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.rE] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.rMIN] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.pA] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.pB] = 0;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.pC] = -0.00001;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.pD] = 0.0072;
            formules[F.TSURUMI, F.LB, F.LBT_1500, F.pE] = 1.1755;
            #endregion

            #region SFQ
            //**************SERIE SFQ*****************************************//
            //SFQ50SFQ2_75    -   0
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.A] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.B] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.C] = -0.004;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.D] = -0.0948;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.E] = 51.3;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.MAX] = 95;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.rA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.rB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.rC] = -0.0193;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.rD] = 2.0289;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.rE] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.rMIN] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.pA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.pB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.pC] = -0.0001;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.pD] = 0.0152;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ2_75, F.pE] = 0.4081;
            //SFQ50SFQ21_5    -   1
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.A] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.B] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.C] = -0.0014;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.D] = -0.1514;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.E] = 70.382;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.MAX] = 165;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.rA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.rB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.rC] = -0.0064;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.rD] = 1.1575;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.rE] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.pA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.pB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.pC] = -0.00008;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.pD] = 0.0173;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ21_5, F.pE] = 1.1113;
            //SFQ50SFQ23_7    -   2
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.A] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.B] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.C] = -0.0007;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.D] = -0.0072;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.E] = 78.8;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.MAX] = 300;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.rA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.rB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.rC] = -0.0021;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.rD] = 0.7012;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.rE] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.pA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.pB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.pC] = -0.00003;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.pD] = 0.013;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ23_7, F.pE] = 2.6471;
            //SFQ50SFQ25_5    -   3
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.A] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.B] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.C] = -0.0005;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.D] = -0.05;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.E] = 97.857;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.MAX] = 350;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.rA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.rB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.rC] = -0.0011;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.rD] = 0.4903;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.rE] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.pA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.pB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.pC] = -0.00006;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.pD] = 0.0239;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ25_5, F.pE] = 3.9833;
            //SFQ50SFQ27_5    -   4
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.A] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.B] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.C] = -0.0003;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.D] = -0.0532;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.E] = 120.59;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.MAX] = 470;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.rA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.rB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.rC] = -0.0007;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.rD] = 0.4014;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.rE] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.pA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.pB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.pC] = -0.00004;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.pD] = 0.0251;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ27_5, F.pE] = 6.6427;
            //SFQ50SFQ211    -   5
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.A] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.B] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.C] = -0.0004;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.D] = -0.0233;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.E] = 140.6;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.MAX] = 550;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.rA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.rB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.rC] = -0.0006;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.rD] = 0.3788;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.rE] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.rMIN] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.pA] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.pB] = 0;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.pC] = -0.00007;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.pD] = 0.041;
            formules[F.TSURUMI, F.SFQ, F.SFQ50SFQ211, F.pE] = 8.05;
            #endregion

            #region SQ
            //**************SERIE SQ*****************************************//
            //SQ50SQ2_4S    -   0
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.A] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.B] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.C] = -0.0064;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.D] = -0.0933;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.E] = 34.714;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.MAX] = 64;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.rA] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.rB] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.rC] = -0.0392;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.rD] = 2.6348;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.rE] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.rMIN] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.pA] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.pB] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.pC] = -0.00003;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.pD] = 0.0052;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_4S, F.pE] = 0.3262;
            //SQ50SQ2_75    -   1
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.A] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.B] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.C] = -0.0057;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.D] = -0.0905;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.E] = 50.667;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.MAX] = 80;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rA] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rB] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rC] = -0.0271;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rD] = 2.2838;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rE] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rMIN] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rA] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rB] = 0;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rC] = 0.000004;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rD] = 0.0063;
            formules[F.TSURUMI, F.SQ, F.SQ50SQ2_75, F.rE] = 0.5392;
            #endregion

            #region TM
            //**************SERIE TM*****************************************//
            //TM50TM2_4S    -   0
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.A] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.B] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.C] = -0.0003;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.D] = -0.4654;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.E] = 42.963;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.MAX] = 80;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.rA] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.rB] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.rC] = 0.0256;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.rD] = 2.2953;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.rE] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.rMIN] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.pA] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.pB] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.pC] = -0.00005;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.pD] = 0.0064;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4S, F.pE] = 0.3396;
            //TM50TM2_4    -   1
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.A] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.B] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.C] = -0.0003;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.D] = -0.4654;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.E] = 42.963;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.MAX] = 80;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.rA] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.rB] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.rC] = 0.0256;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.rD] = 2.2953;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.rE] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.rMIN] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.pA] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.pB] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.pC] = -0.00005;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.pD] = 0.0064;
            formules[F.TSURUMI, F.TM, F.TM50TM2_4, F.pE] = 0.3396;
            //TM50TM2_75S    -   2
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.A] = 0.0000003;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.B] = -0.0001;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.C] = 0.0102;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.D] = -0.7079;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.E] = 56.031;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.MAX] = 90;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.rA] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.rB] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.rC] = -0.0144;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.rD] = 1.6401;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.rE] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.rMIN] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.pA] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.pB] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.pC] = -0.00007;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.pD] = 0.0102;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75S, F.pE] = 0.6262;
            //TM50TM2_75    -   3
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.A] = 0.0000003;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.B] = -0.0001;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.C] = 0.0102;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.D] = -0.7079;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.E] = 56.031;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.MAX] = 90;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.rA] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.rB] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.rC] = -0.0144;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.rD] = 1.6401;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.rE] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.rMIN] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.pA] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.pB] = 0;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.pC] = -0.00007;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.pD] = 0.0102;
            formules[F.TSURUMI, F.TM, F.TM50TM2_75, F.pE] = 0.6262;
            //TM80TM21_5    -   4
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.A] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.B] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.C] = -0.2693;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.D] = -0.2693;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.E] = 63.4;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.MAX] = 170;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.rA] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.rB] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.rC] = -0.007;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.rD] = 1.1896;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.rE] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.pA] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.pB] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.pC] = -0.00001;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.pD] = 0.0068;
            formules[F.TSURUMI, F.TM, F.TM80TM21_5, F.pE] = 1.1115;
            //TM80TM22_2    -   5
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.A] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.B] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.C] = -0.0478;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.D] = -0.0478;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.E] = 74.885;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.MAX] = 170;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.rA] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.rB] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.rC] = -0.0027;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.rD] = 0.4986;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.rE] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.pA] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.pB] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.pC] = -0.00005;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.pD] = 0.0151;
            formules[F.TSURUMI, F.TM, F.TM80TM22_2, F.pE] = 1.6964;
            //TM80TM23_7    -   6
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.A] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.B] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.C] = -0.0523;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.D] = -0.0523;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.E] = 74.885;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.MAX] = 93.124;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.rA] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.rB] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.rC] = -0.0033;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.rD] = 0.7862;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.rE] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.pA] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.pB] = 0;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.pC] = -0.00004;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.pD] = 0.0213;
            formules[F.TSURUMI, F.TM, F.TM80TM23_7, F.pE] = 2.4146;
            #endregion

            #region U
            //**************SERIE U*****************************************//
            //U50U21_5    -   0
            formules[F.TSURUMI, F.U, F.U50U21_5, F.A] = 0;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.B] = 0;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.C] = -0.4042;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.D] = -0.4042;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.E] = 71.191;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.MAX] = 90;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.rA] = 0;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.rB] = 0;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.rC] = -0.0087;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.rD] = 1.1523;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.rE] = 0;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.pA] = 0;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.pB] = 0;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.pC] = -0.00004;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.pD] = 0.0104;
            formules[F.TSURUMI, F.U, F.U50U21_5, F.pE] = 1.3526;
            //U80U21_5    -   1
            formules[F.TSURUMI, F.U, F.U80U21_5, F.A] = -0.00000004;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.B] = 0.000003;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.C] = -0.0001;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.D] = -0.1602;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.E] = 52.317;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.MAX] = 150;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.rA] = -0.0000002;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.rB] = 0.00005;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.rC] = -0.0073;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.rD] = 0.876;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.rE] = 0;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.pA] = 0;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.pB] = 0;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.pC] = -0.00003;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.pD] = 0.0075;
            formules[F.TSURUMI, F.U, F.U80U21_5, F.pE] = 1.4703;
            //U80U22_2    -   2
            formules[F.TSURUMI, F.U, F.U80U22_2, F.A] = 0;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.B] = 0;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.C] = -0.0003;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.D] = -0.1174;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.E] = 56.986;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.MAX] = 220;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.rA] = 0;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.rB] = 0;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.rC] = -0.0024;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.rD] = 0.6458;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.rE] = 0;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.pA] = 0;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.pB] = 0;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.pC] = -0.00002;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.pD] = 0.0083;
            formules[F.TSURUMI, F.U, F.U80U22_2, F.pE] = 1.9161;
            //U80U23_7    -   3
            formules[F.TSURUMI, F.U, F.U80U23_7, F.A] = 0;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.B] = 0;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.C] = -0.0006;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.D] = -0.1026;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.E] = 89.889;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.MAX] = 260;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.rA] = 0;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.rB] = 0;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.rC] = -0.0021;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.rD] = 0.6433;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.rE] = 0;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.pA] = 0;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.pB] = 0;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.pC] = -0.00002;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.pD] = 0.011;
            formules[F.TSURUMI, F.U, F.U80U23_7, F.pE] = 2.8089;
            #endregion

            #region UT
            //**************SERIE UT*****************************************//
            //UT50UT2_4S    -   0
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.A] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.B] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.C] = -0.0018;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.D] = -0.2143;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.E] = 29.5;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.MAX] = 70;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.rA] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.rB] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.rC] = -0.0199;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.rD] = 1.6746;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.rE] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.rMIN] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.pA] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.pB] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.pC] = -0.00003;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.pD] = 0.0034;
            formules[F.TSURUMI, F.UT, F.UT50UT2_4S, F.pE] = 0.4279;
            //UT50UTZ2_4S    -   1
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.A] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.B] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.C] = -0.0018;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.D] = -0.2143;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.E] = 29.5;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.MAX] = 70;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.rA] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.rB] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.rC] = -0.0199;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.rD] = 1.6746;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.rE] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.MAX] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.pA] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.pB] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.pC] = -0.00003;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.pD] = 0.0034;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_4S, F.pE] = 0.4279;
            //UT50UT2_75S    -   2
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.A] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.B] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.C] = -0.0013;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.D] = -0.268;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.E] = 43.936;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.MAX] = 95;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.rA] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.rB] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.rC] = -0.0143;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.rD] = 1.5189;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.rE] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.rMIN] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.pA] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.pB] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.pC] = -0.000003;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.pD] = 0.0035;
            formules[F.TSURUMI, F.UT, F.UT50UT2_75S, F.pE] = 0.6977;
            //UT50UTZ2_75S    -   3
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.A] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.B] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.C] = -0.0013;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.D] = -0.268;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.E] = 43.936;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.MAX] = 95;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.rA] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.rB] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.rC] = -0.0143;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.rD] = 1.5189;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.rE] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.rMIN] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.pA] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.pB] = 0;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.pC] = -0.000003;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.pD] = 0.0035;
            formules[F.TSURUMI, F.UT, F.UT50UTZ2_75S, F.pE] = 0.6977;
            #endregion

            #region UZ
            //**************SERIE UZ*****************************************//
            //UZ50UZ41_5    -   0
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.C] = -0.0002;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.D] = -0.0673;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.E] = 36.491;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.MAX] = 190;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.rC] = -0.0027;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.rD] = 0.7018;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.pC] = -0.00001;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.pD] = 0.0069;
            formules[F.TSURUMI, F.UZ, F.UZ50UZ41_5, F.pE] = 1.0585;
            //UZ80UZ41_5    -   1
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.C] = -0.00006;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.D] = -0.0371;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.E] = 23.764;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.MAX] = 270;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.rC] = -0.0012;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.rD] = 0.4437;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.pC] = 0.000004;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.pD] = 0.0015;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ41_5, F.pE] = 1.3459;
            //UZ80UZ42_2    -   2
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.C] = -0.00005;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.D] = -0.0375;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.E] = 29.958;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.MAX] = 350;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.rC] = -0.001;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.rD] = 0.4231;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.pC] = 0.000005;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.pD] = 0.0022;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ42_2, F.pE] = 1.5917;
            //UZ80UZ43_7    -   3
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.C] = -0.00004;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.D] = -0.0442;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.E] = 42.424;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.MAX] = 400;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.rC] = -0.0006;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.rD] = 0.3344;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.pC] = -0.0000004;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.pD] = 0.0062;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ43_7, F.pE] = 2.6321;
            //UZ80UZ45_5    -   4
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.C] = -0.0001;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.D] = -0.0346;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.E] = 59.582;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.MAX] = 470;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.rC] = -0.0006;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.rD] = 0.3486;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.pC] = 0.000007;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.pD] = 0.0044;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ45_5, F.pE] = 3.8373;
            //UZ80UZ47_5    -   5
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.C] = -0.0001;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.D] = -0.0346;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.E] = 59.582;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.MAX] = 470;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.rC] = -0.0006;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.rD] = 0.3486;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.pC] = 0.000007;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.pD] = 0.0044;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ47_5, F.pE] = 3.8373;
            //UZ80UZ411    -   6
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.C] = -0.00003;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.D] = -0.051;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.E] = 81.685;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.MAX] = 500;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.rC] = -0.0003;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.rD] = 0.2386;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.pC] = 0.000006;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.pD] = 0.0101;
            formules[F.TSURUMI, F.UZ, F.UZ80UZ411, F.pE] = 7.4112;
            //UZ100UZ43_7    -   7
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.C] = -0.00007;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.D] = -0.013;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.E] = 32.777;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.MAX] = 450;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.rC] = -0.0004;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.rD] = 0.2671;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.pC] = 0.000006;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.pD] = 0.0009;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ43_7, F.pE] = 3.4664;
            //UZ100UZ45_5    -   8
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.C] = -0.00004;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.D] = -0.0363;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.E] = 59.071;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.MAX] = 650;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.rC] = -0.0003;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.rD] = 0.2421;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.pC] = -0.000004;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.pD] = -0.0087;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ45_5, F.pE] = 5.1361;
            //UZ100UZ47_5    -   9
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.C] = -0.00004;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.D] = -0.0363;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.E] = 59.071;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.MAX] = 650;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.rC] = -0.0003;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.rD] = 0.2421;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.pC] = -0.000004;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.pD] = 0.0087;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ47_5, F.pE] = 5.1361;
            //UZ100UZ411    -   9
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.A] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.B] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.C] = -0.00003;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.D] = -0.0343;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.E] = 69.639;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.MAX] = 730;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.VITESSE] = 3600;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.rA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.rB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.rC] = -0.0002;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.rD] = 0.2063;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.rE] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.rMIN] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.pA] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.pB] = 0;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.pC] = -0.0000007;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.pD] = 0.0096;
            formules[F.TSURUMI, F.UZ, F.UZ100UZ411, F.pE] = 7.1696;
            #endregion


            #endregion



            //=================================================================
            ////Grundfos
            //=================================================================



            //**************SERIE CR4S****************************************//
            //CR4S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR4S, F.CR4S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR4S, F.CR4S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR4S, F.CR4S_1_ST, F.C] = -0.0005;
            formules[F.GRUNDFOS, F.CR4S, F.CR4S_1_ST, F.D] = -0.0317;
            formules[F.GRUNDFOS, F.CR4S, F.CR4S_1_ST, F.E] = 123.76;
            formules[F.GRUNDFOS, F.CR4S, F.CR4S_1_ST, F.MAX] = 280;
            formules[F.GRUNDFOS, F.CR4S, F.CR4S_1_ST, F.VITESSE] = 3600;
            //**************SERIE CR7S***************************************//
            //CR7S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR7S, F.CR7S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR7S, F.CR7S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR7S, F.CR7S_1_ST, F.C] = -1.2311;
            formules[F.GRUNDFOS, F.CR7S, F.CR7S_1_ST, F.D] = -1.5871;
            formules[F.GRUNDFOS, F.CR7S, F.CR7S_1_ST, F.E] = 228.23;
            formules[F.GRUNDFOS, F.CR7S, F.CR7S_1_ST, F.MAX] = 9;
            formules[F.GRUNDFOS, F.CR7S, F.CR7S_1_ST, F.VITESSE] = 3600;
            //***************SERIE CR 10S**************************************//
            //CR10S_6_ST    -   0
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_6_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_6_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_6_ST, F.C] = -0.5952;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_6_ST, F.D] = 0.8929;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_6_ST, F.E] = 169.17;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_6_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_6_ST, F.VITESSE] = 3600;
            //CR10S_9_ST    -   1
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_9_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_9_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_9_ST, F.C] = -0.9524;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_9_ST, F.D] = 1.6071;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_9_ST, F.E] = 249.88;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_9_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_9_ST, F.VITESSE] = 3600;
            //CR10S_12_ST    -   2
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_12_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_12_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_12_ST, F.C] = -1.2202;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_12_ST, F.D] = 1.4286;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_12_ST, F.E] = 336.31;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_12_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_12_ST, F.VITESSE] = 3600;
            //CR10S_15_ST    -   3
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_15_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_15_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_15_ST, F.C] = -1.4732;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_15_ST, F.D] = 1.875;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_15_ST, F.E] = 416.79;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_15_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_15_ST, F.VITESSE] = 3600;
            //CR10S_21_ST    -   4
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_21_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_21_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_21_ST, F.C] = -1.9494;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_21_ST, F.D] = 1.3393;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_21_ST, F.E] = 587.62;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_21_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_21_ST, F.VITESSE] = 3600;
            //CR10S_27_ST    -   5
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_27_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_27_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_27_ST, F.C] = -2.5893;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_27_ST, F.D] = 2.1429;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_27_ST, F.E] = 753.93;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_27_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_27_ST, F.VITESSE] = 3600;
            //CR10S_34_ST    -   6
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_34_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_34_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_34_ST, F.C] = -3.4673;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_34_ST, F.D] = 3.6607;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_34_ST, F.E] = 966.19;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_34_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_34_ST, F.VITESSE] = 3600;
            //CR10S_48_ST    -   7
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_48_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_48_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_48_ST, F.C] = -4.6875;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_48_ST, F.D] = 3.4821;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_48_ST, F.E] = 1378.6;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_48_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_48_ST, F.VITESSE] = 3600;
            //CR10S_58_ST    -   8
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_58_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_58_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_58_ST, F.C] = -5.6696;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_58_ST, F.D] = 2.5893;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_58_ST, F.E] = 1661.4;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_58_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.CR10S, F.CR10S_58_ST, F.VITESSE] = 3600;
            //*******************SERIE 16S************************************//
            //CR16S_5_ST    -   0
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_5_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_5_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_5_ST, F.C] = -0.1034;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_5_ST, F.D] = -1.5449;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_5_ST, F.E] = 151.75;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_5_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_5_ST, F.VITESSE] = 3600;
            //CR16S_8_ST    -   1
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_8_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_8_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_8_ST, F.C] = -0.2229;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_8_ST, F.D] = -1.2465;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_8_ST, F.E] = 235.94;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_8_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_8_ST, F.VITESSE] = 3600;
            //CR16S_10_ST    -   2
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_10_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_10_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_10_ST, F.C] = -0.3307;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_10_ST, F.D] = -0.6812;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_10_ST, F.E] = 298.11;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_10_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_10_ST, F.VITESSE] = 3600;
            //CR16S_14_ST    -   3
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_14_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_14_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_14_ST, F.C] = -0.5026;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_14_ST, F.D] = -0.1521;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_14_ST, F.E] = 413.25;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_14_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_14_ST, F.VITESSE] = 3600;
            //CR16S_18_ST    -   4
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_18_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_18_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_18_ST, F.C] = -0.5522;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_18_ST, F.D] = -2.0705;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_18_ST, F.E] = 531.19;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_18_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_18_ST, F.VITESSE] = 3600;
            //CR16S_24_ST    -   5
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_24_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_24_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_24_ST, F.C] = -0.8202;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_24_ST, F.D] = -0.8001;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_24_ST, F.E] = 706.47;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_24_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_24_ST, F.VITESSE] = 3600;
            //CR16S_38_ST    -   6
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_38_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_38_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_38_ST, F.C] = -1.1174;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_38_ST, F.D] = -0.6742;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_38_ST, F.E] = 1110;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_38_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_38_ST, F.VITESSE] = 3600;
            //CR16S_56_ST    -   7
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_56_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_56_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_56_ST, F.C] = -1.5895;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_56_ST, F.D] = -2.6428;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_56_ST, F.E] = 1640.8;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_56_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_56_ST, F.VITESSE] = 3600;
            //CR16S_75_ST    -   8
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_75_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_75_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_75_ST, F.C] = -2.0731;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_75_ST, F.D] = -5.3328;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_75_ST, F.E] = 2197.2;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_75_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.CR16S, F.CR16S_75_ST, F.VITESSE] = 3600;
            //*******************SERIE 25S************************************//
            //CR25S_3_ST    -   0
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_3_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_3_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_3_ST, F.C] = -0.0405;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_3_ST, F.D] = -0.1548;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_3_ST, F.E] = 87.917;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_3_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_3_ST, F.VITESSE] = 1800;
            //CR25S_5_ST    -   1
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_5_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_5_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_5_ST, F.C] = -0.0679;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_5_ST, F.D] = -0.2798;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_5_ST, F.E] = 146.46;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_5_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_5_ST, F.VITESSE] = 1800;
            //CR25S_7_ST    -   2
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_7_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_7_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_7_ST, F.C] = -0.0905;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_7_ST, F.D] = -0.5952;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_7_ST, F.E] = 206.25;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_7_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_7_ST, F.VITESSE] = 1800;
            //CR25S_9_ST    -   3
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_9_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_9_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_9_ST, F.C] = -0.1286;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_9_ST, F.D] = -0.4048;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_9_ST, F.E] = 265.83;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_9_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_9_ST, F.VITESSE] = 1800;
            //CR25S_11_ST    -   4
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_11_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_11_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_11_ST, F.C] = -0.1607;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_11_ST, F.D] = -0.1726;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_11_ST, F.E] = 321.46;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_11_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_11_ST, F.VITESSE] = 1800;
            //CR25S_15_ST    -   5
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_15_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_15_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_15_ST, F.C] = -0.2024;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_15_ST, F.D] = -0.9881;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_15_ST, F.E] = 443.33;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_15_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_15_ST, F.VITESSE] = 1800;
            //CR25S_26_ST    -   6
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_26_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_26_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_26_ST, F.C] = -0.3524;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_26_ST, F.D] = -1.6667;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_26_ST, F.E] = 770.83;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_26_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_26_ST, F.VITESSE] = 1800;
            //CR25S_39_ST    -   7
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_39_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_39_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_39_ST, F.C] = -0.5;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_39_ST, F.D] = -3.3095;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_39_ST, F.E] = 1154.2;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_39_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_39_ST, F.VITESSE] = 1800;
            //CR25S_52_ST    -   8
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_52_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_52_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_52_ST, F.C] = -0.7262;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_52_ST, F.D] = -2.9881;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_52_ST, F.E] = 1541.3;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_52_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.CR25S, F.CR25S_52_ST, F.VITESSE] = 1800;
            //*******************SERIE 40S************************************//
            //CR40S_3_ST    -   0
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_3_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_3_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_3_ST, F.C] = 0.0045;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_3_ST, F.D] = -0.7946;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_3_ST, F.E] = 79.98;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_3_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_3_ST, F.VITESSE] = 1800;
            //CR40S_5_ST    -   1
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_5_ST, F.A] = -0.00001;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_5_ST, F.B] = 0.0016;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_5_ST, F.C] = -0.0747;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_5_ST, F.D] = 0.1614;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_5_ST, F.E] = 139.99;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_5_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_5_ST, F.VITESSE] = 1800;
            //CR40S_7_ST    -   2
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_7_ST, F.A] = -0.00007;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_7_ST, F.B] = 0.0071;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_7_ST, F.C] = -0.2128;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_7_ST, F.D] = 0.5073;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_7_ST, F.E] = 199.98;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_7_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_7_ST, F.VITESSE] = 1800;
            //CR40S_9_ST    -   3
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_9_ST, F.A] = -0.0001;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_9_ST, F.B] = 0.0119;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_9_ST, F.C] = -0.3583;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_9_ST, F.D] = 1.5853;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_9_ST, F.E] = 254.88;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_9_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_9_ST, F.VITESSE] = 1800;
            //CR40S_12_ST    -   4
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_12_ST, F.A] = -0.00008;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_12_ST, F.B] = 0.0069;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_12_ST, F.C] = -0.175;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_12_ST, F.D] = -1.004;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_12_ST, F.E] = 340.24;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_12_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_12_ST, F.VITESSE] = 1800;
            //CR40S_15_ST    -   5
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_15_ST, F.A] = -0.00005;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_15_ST, F.B] = 0.0039;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_15_ST, F.C] = -0.0941;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_15_ST, F.D] = -1.953;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_15_ST, F.E] = 425.1;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_15_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_15_ST, F.VITESSE] = 1800;
            //CR40S_21_ST    -   6
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_21_ST, F.A] = -0.0001;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_21_ST, F.B] = 0.0115;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_21_ST, F.C] = -0.2927;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_21_ST, F.D] = -1.9762;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_21_ST, F.E] = 599.82;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_21_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_21_ST, F.VITESSE] = 1800;
            //CR40S_25_ST    -   7
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_25_ST, F.A] = -0.0001;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_25_ST, F.B] = 0.0103;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_25_ST, F.C] = -0.2458;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_25_ST, F.D] = -3.5873;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_25_ST, F.E] = 710.24;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_25_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_25_ST, F.VITESSE] = 1800;
            //CR40S_30_ST    -   8
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_30_ST, F.A] = -0.0002;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_30_ST, F.B] = 0.0133;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_30_ST, F.C] = -0.3083;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_30_ST, F.D] = -4.0833;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_30_ST, F.E] = 850;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_30_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_30_ST, F.VITESSE] = 1800;
            //CR40S_37_ST    -   9
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_37_ST, F.A] = -0.0004;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_37_ST, F.B] = 0.0353;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_37_ST, F.C] = -1.0458;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_37_ST, F.D] = 1.7341;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_37_ST, F.E] = 1051;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_37_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_37_ST, F.VITESSE] = 1800;
            //CR40S_44_ST    -   10
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_44_ST, F.A] = -0.0002;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_44_ST, F.B] = 0.012;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_44_ST, F.C] = -0.2361;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_44_ST, F.D] = -8.4378;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_44_ST, F.E] = 1249.6;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_44_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_44_ST, F.VITESSE] = 1800;
            //CR40S_50_ST    -   10
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_50_ST, F.A] = -0.0002;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_50_ST, F.B] = 0.0185;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_50_ST, F.C] = -0.4562;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_50_ST, F.D] = -7.127;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_50_ST, F.E] = 1420.1;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_50_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_50_ST, F.VITESSE] = 1800;
            //CR40S_58_ST    -   10
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_58_ST, F.A] = -0.0003;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_58_ST, F.B] = 0.0242;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_58_ST, F.C] = -0.6208;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_58_ST, F.D] = -6.9167;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_58_ST, F.E] = 1640;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_58_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_58_ST, F.VITESSE] = 1800;
            //CR40S_66_ST    -   11
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_66_ST, F.A] = -0.0004;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_66_ST, F.B] = 0.0288;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_66_ST, F.C] = -0.6965;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_66_ST, F.D] = -9.3981;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_66_ST, F.E] = 1869.7;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_66_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.CR40S, F.CR40S_66_ST, F.VITESSE] = 1800;
            //*******************SERIE 60S************************************//
            //CR60S_4_ST    -   0
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_4_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_4_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_4_ST, F.C] = -0.0165;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_4_ST, F.D] = 0.3896;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_4_ST, F.E] = 125.45;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_4_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_4_ST, F.VITESSE] = 3600;
            //CR60S_5_ST    -   1
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_5_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_5_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_5_ST, F.C] = -0.0202;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_5_ST, F.D] = 0.5064;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_5_ST, F.E] = 156.61;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_5_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_5_ST, F.VITESSE] = 1800;
            //CR60S_7_ST    -   2
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_7_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_7_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_7_ST, F.C] = -0.0272;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_7_ST, F.D] = 0.6232;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_7_ST, F.E] = 222.76;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_7_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_7_ST, F.VITESSE] = 1800;
            //CR60S_9_ST    -   3
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_9_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_9_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_9_ST, F.C] = -0.036;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_9_ST, F.D] = 0.8165;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_9_ST, F.E] = 284.03;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_9_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_9_ST, F.VITESSE] = 1800;
            //CR60S_13_ST    -   4
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_13_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_13_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_13_ST, F.C] = -0.0532;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_13_ST, F.D] = 1.2597;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_13_ST, F.E] = 408.64;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_13_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_13_ST, F.VITESSE] = 1800;
            //CR60S_18_ST    -   5
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_18_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_18_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_18_ST, F.C] = -0.0754;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_18_ST, F.D] = 1.8513;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_18_ST, F.E] = 564.15;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_18_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.CR60S, F.CR60S_18_ST, F.VITESSE] = 1800;
            //*******************SERIE 75S************************************//
            //CR75S_3_ST    -   0
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_3_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_3_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_3_ST, F.C] = -0.0062;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_3_ST, F.D] = 0.0585;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_3_ST, F.E] = 95.473;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_3_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_3_ST, F.VITESSE] = 3600;
            //CR75S_5_ST    -   1
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_5_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_5_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_5_ST, F.C] = -0.0114;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_5_ST, F.D] = 0.1892;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_5_ST, F.E] = 156.68;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_5_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_5_ST, F.VITESSE] = 3600;
            //CR75S_8_ST    -   2
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_8_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_8_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_8_ST, F.C] = -0.0158;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_8_ST, F.D] = 0.1364;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_8_ST, F.E] = 253.27;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_8_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_8_ST, F.VITESSE] = 3600;
            //CR75S_11_ST    -   3
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_11_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_11_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_11_ST, F.C] = -0.0225;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_11_ST, F.D] = 0.1743;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_11_ST, F.E] = 348.77;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_11_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_11_ST, F.VITESSE] = 1800;
            //CR75S_12_ST    -   4
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_12_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_12_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_12_ST, F.C] = -0.0246;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_12_ST, F.D] = 0.2492;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_12_ST, F.E] = 378.45;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_12_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_12_ST, F.VITESSE] = 1800;
            //CR75S_15_ST    -   5
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_15_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_15_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_15_ST, F.C] = -0.032;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_15_ST, F.D] = 0.2201;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_15_ST, F.E] = 464.32;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_15_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_15_ST, F.VITESSE] = 1800;
            //CR75S_16_ST    -   6
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_16_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_16_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_16_ST, F.C] = -0.0326;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_16_ST, F.D] = 0.0864;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_16_ST, F.E] = 494.45;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_16_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.CR75S, F.CR75S_16_ST, F.VITESSE] = 3600;
            //*******************SERIE 85S************************************//
            //CR85S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR85S, F.CR85S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR85S, F.CR85S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR85S, F.CR85S_1_ST, F.C] = -0.0021;
            formules[F.GRUNDFOS, F.CR85S, F.CR85S_1_ST, F.D] = -0.0561;
            formules[F.GRUNDFOS, F.CR85S, F.CR85S_1_ST, F.E] = 56.5;
            formules[F.GRUNDFOS, F.CR85S, F.CR85S_1_ST, F.MAX] = 100;
            formules[F.GRUNDFOS, F.CR85S, F.CR85S_1_ST, F.VITESSE] = 3600;
            //*******************SERIE 150S************************************//
            //CR150S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR150S, F.CR150S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR150S, F.CR150S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR150S, F.CR150S_1_ST, F.C] = -0.0007;
            formules[F.GRUNDFOS, F.CR150S, F.CR150S_1_ST, F.D] = -0.0345;
            formules[F.GRUNDFOS, F.CR150S, F.CR150S_1_ST, F.E] = 55.161;
            formules[F.GRUNDFOS, F.CR150S, F.CR150S_1_ST, F.MAX] = 200;
            formules[F.GRUNDFOS, F.CR150S, F.CR150S_1_ST, F.VITESSE] = 3600;
            //*******************SERIE 230S************************************//
            //CR230S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR230S, F.CR230S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR230S, F.CR230S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR230S, F.CR230S_1_ST, F.C] = -0.0003;
            formules[F.GRUNDFOS, F.CR230S, F.CR230S_1_ST, F.D] = -0.0364;
            formules[F.GRUNDFOS, F.CR230S, F.CR230S_1_ST, F.E] = 64.143;
            formules[F.GRUNDFOS, F.CR230S, F.CR230S_1_ST, F.MAX] = 300;
            formules[F.GRUNDFOS, F.CR230S, F.CR230S_1_ST, F.VITESSE] = 3600;
            //*******************SERIE 300S************************************//
            //CR300S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR300S, F.CR300S_1_ST, F.A] = -0.00000001;
            formules[F.GRUNDFOS, F.CR300S, F.CR300S_1_ST, F.B] = 0.00001;
            formules[F.GRUNDFOS, F.CR300S, F.CR300S_1_ST, F.C] = -0.0028;
            formules[F.GRUNDFOS, F.CR300S, F.CR300S_1_ST, F.D] = 0.1687;
            formules[F.GRUNDFOS, F.CR300S, F.CR300S_1_ST, F.E] = 62.592;
            formules[F.GRUNDFOS, F.CR300S, F.CR300S_1_ST, F.MAX] = 400;
            formules[F.GRUNDFOS, F.CR300S, F.CR300S_1_ST, F.VITESSE] = 3600;
            //*******************SERIE 385S************************************//
            //CR385S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR385S, F.CR385S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR385S, F.CR385S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR385S, F.CR385S_1_ST, F.C] = -0.0002;
            formules[F.GRUNDFOS, F.CR385S, F.CR385S_1_ST, F.D] = -0.0505;
            formules[F.GRUNDFOS, F.CR385S, F.CR385S_1_ST, F.E] = 98.685;
            formules[F.GRUNDFOS, F.CR385S, F.CR385S_1_ST, F.MAX] = 500;
            formules[F.GRUNDFOS, F.CR385S, F.CR385S_1_ST, F.VITESSE] = 1800;
            //*******************SERIE 475S************************************//
            //CR475S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR475S, F.CR475S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR475S, F.CR475S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR475S, F.CR475S_1_ST, F.C] = -0.00004;
            formules[F.GRUNDFOS, F.CR475S, F.CR475S_1_ST, F.D] = -0.0921;
            formules[F.GRUNDFOS, F.CR475S, F.CR475S_1_ST, F.E] = 105.88;
            formules[F.GRUNDFOS, F.CR475S, F.CR475S_1_ST, F.MAX] = 600;
            formules[F.GRUNDFOS, F.CR475S, F.CR475S_1_ST, F.VITESSE] = 1800;
            //*******************SERIE 625S************************************//
            //CR625S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR625S, F.CR625S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.CR625S, F.CR625S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.CR625S, F.CR625S_1_ST, F.C] = -0.00006;
            formules[F.GRUNDFOS, F.CR625S, F.CR625S_1_ST, F.D] = -0.0393;
            formules[F.GRUNDFOS, F.CR625S, F.CR625S_1_ST, F.E] = 147.55;
            formules[F.GRUNDFOS, F.CR625S, F.CR625S_1_ST, F.MAX] = 800;
            formules[F.GRUNDFOS, F.CR625S, F.CR625S_1_ST, F.VITESSE] = 1800;
            //*******************SERIE 800S************************************//
            //CR800S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR800S, F.CR800S_1_ST, F.A] = -0.0000000006;
            formules[F.GRUNDFOS, F.CR800S, F.CR800S_1_ST, F.B] = 0.000001;
            formules[F.GRUNDFOS, F.CR800S, F.CR800S_1_ST, F.C] = -0.0009;
            formules[F.GRUNDFOS, F.CR800S, F.CR800S_1_ST, F.D] = 0.1238;
            formules[F.GRUNDFOS, F.CR800S, F.CR800S_1_ST, F.E] = 158.33;
            formules[F.GRUNDFOS, F.CR800S, F.CR800S_1_ST, F.MAX] = 1100;
            formules[F.GRUNDFOS, F.CR800S, F.CR800S_1_ST, F.VITESSE] = 3600;
            //*******************SERIE 1100S************************************//
            //CR1100S_1_ST    -   0
            formules[F.GRUNDFOS, F.CR1100S, F.CR1100S_1_ST, F.A] = -0.0000000002;
            formules[F.GRUNDFOS, F.CR1100S, F.CR1100S_1_ST, F.B] = 0.0000004;
            formules[F.GRUNDFOS, F.CR1100S, F.CR1100S_1_ST, F.C] = -0.0004;
            formules[F.GRUNDFOS, F.CR1100S, F.CR1100S_1_ST, F.D] = 0.0537;
            formules[F.GRUNDFOS, F.CR1100S, F.CR1100S_1_ST, F.E] = 183.99;
            formules[F.GRUNDFOS, F.CR1100S, F.CR1100S_1_ST, F.MAX] = 1400;
            formules[F.GRUNDFOS, F.CR1100S, F.CR1100S_1_ST, F.MAX] = 3600;


            //**************SERIE SP5S****************************************//
            //SP5S_8_ST    -   0
            formules[F.GRUNDFOS, F.SP5S, F.SP5S_8_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP5S, F.SP5S_8_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP5S, F.SP5S_8_ST, F.C] = -2.7976;
            formules[F.GRUNDFOS, F.SP5S, F.SP5S_8_ST, F.D] = 0.1786;
            formules[F.GRUNDFOS, F.SP5S, F.SP5S_8_ST, F.E] = 249.4;
            formules[F.GRUNDFOS, F.SP5S, F.SP5S_8_ST, F.MAX] = 6;
            formules[F.GRUNDFOS, F.SP5S, F.SP5S_8_ST, F.VITESSE] = 3600;
            //**************SERIE SP7S****************************************//
            //SP7S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP7S, F.SP7S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP7S, F.SP7S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP7S, F.SP7S_1_ST, F.C] = -1.2311;
            formules[F.GRUNDFOS, F.SP7S, F.SP7S_1_ST, F.D] = -1.5871;
            formules[F.GRUNDFOS, F.SP7S, F.SP7S_1_ST, F.E] = 228.23;
            formules[F.GRUNDFOS, F.SP7S, F.SP7S_1_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP7S, F.SP7S_1_ST, F.VITESSE] = 3600;
            //**************SERIE SP7S****************************************//
            //SP10S_6_ST    -   0
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_6_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_6_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_6_ST, F.C] = -0.5952;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_6_ST, F.D] = 0.8929;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_6_ST, F.E] = 169.17;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_6_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_6_ST, F.VITESSE] = 3600;
            //SP10S_9_ST    -   1
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_9_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_9_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_9_ST, F.C] = -0.9524;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_9_ST, F.D] = 1.6071;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_9_ST, F.E] = 249.88;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_9_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_9_ST, F.VITESSE] = 3600;
            //SP10S_12_ST    -   2
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_12_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_12_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_12_ST, F.C] = -1.2202;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_12_ST, F.D] = 1.4286;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_12_ST, F.E] = 336.31;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_12_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_12_ST, F.VITESSE] = 3600;
            //SP10S_15_ST    -   3
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_15_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_15_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_15_ST, F.C] = -1.4732;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_15_ST, F.D] = 1.875;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_15_ST, F.E] = 416.79;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_15_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_15_ST, F.VITESSE] = 3600;
            //SP10S_21_ST    -   4
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_21_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_21_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_21_ST, F.C] = -1.9494;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_21_ST, F.D] = 1.3393;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_21_ST, F.E] = 587.62;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_21_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_21_ST, F.VITESSE] = 3600;
            //SP10S_27_ST    -   5
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_27_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_27_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_27_ST, F.C] = -2.5893;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_27_ST, F.D] = 2.1429;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_27_ST, F.E] = 753.93;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_27_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_27_ST, F.VITESSE] = 3600;
            //SP10S_34_ST    -   6
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_34_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_34_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_34_ST, F.C] = -3.4673;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_34_ST, F.D] = 3.6607;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_34_ST, F.E] = 966.19;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_34_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_34_ST, F.VITESSE] = 3600;
            //SP10S_48_ST    -   7
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_48_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_48_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_48_ST, F.C] = -4.6875;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_48_ST, F.D] = 3.4821;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_48_ST, F.E] = 1378.6;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_48_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_48_ST, F.VITESSE] = 3600;
            //SP10S_58_ST    -   8
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_58_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_58_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_58_ST, F.C] = -5.6696;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_58_ST, F.D] = 2.5893;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_58_ST, F.E] = 1661.4;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_58_ST, F.MAX] = 12;
            formules[F.GRUNDFOS, F.SP10S, F.SP10S_58_ST, F.VITESSE] = 3600;
            //**************SERIE SP16S****************************************//
            //SP16S_5_ST    -   0
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_5_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_5_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_5_ST, F.C] = -0.1034;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_5_ST, F.D] = -1.5449;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_5_ST, F.E] = 151.75;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_5_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_5_ST, F.VITESSE] = 3600;
            //SP16S_8_ST    -   1
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_8_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_8_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_8_ST, F.C] = -0.2229;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_8_ST, F.D] = -1.2465;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_8_ST, F.E] = 235.94;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_8_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_8_ST, F.VITESSE] = 3600;
            //SP16S_10_ST    -   2
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_10_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_10_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_10_ST, F.C] = -0.3307;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_10_ST, F.D] = -0.6812;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_10_ST, F.E] = 298.11;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_10_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_10_ST, F.VITESSE] = 3600;
            //SP16S_14_ST    -   3
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_14_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_14_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_14_ST, F.C] = -0.5026;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_14_ST, F.D] = -0.1521;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_14_ST, F.E] = 413.25;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_14_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_14_ST, F.VITESSE] = 3600;
            //SP16S_18_ST    -   4
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_18_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_18_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_18_ST, F.C] = -0.5522;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_18_ST, F.D] = -2.0705;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_18_ST, F.E] = 531.19;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_18_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_18_ST, F.VITESSE] = 3600;
            //SP16S_24_ST    -   5
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_24_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_24_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_24_ST, F.C] = -0.8202;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_24_ST, F.D] = -0.8001;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_24_ST, F.E] = 706.47;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_24_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_24_ST, F.VITESSE] = 3600;
            //SP16S_38_ST    -   6
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_38_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_38_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_38_ST, F.C] = -1.1174;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_38_ST, F.D] = -0.6742;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_38_ST, F.E] = 1110;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_38_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_38_ST, F.VITESSE] = 3600;
            //SP16S_56_ST    -   7
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_56_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_56_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_56_ST, F.C] = -1.5895;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_56_ST, F.D] = -2.6428;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_56_ST, F.E] = 1640.8;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_56_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_56_ST, F.VITESSE] = 3600;
            //SP16S_75_ST    -   8
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_75_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_75_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_75_ST, F.C] = -2.0731;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_75_ST, F.D] = -5.3328;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_75_ST, F.E] = 2197.2;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_75_ST, F.MAX] = 20;
            formules[F.GRUNDFOS, F.SP16S, F.SP16S_75_ST, F.VITESSE] = 3600;
            //**************SERIE SP25S****************************************//
            //SP25S_3_ST    -   0
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_3_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_3_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_3_ST, F.C] = -0.0405;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_3_ST, F.D] = -0.1548;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_3_ST, F.E] = 87.917;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_3_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_3_ST, F.VITESSE] = 1800;
            //SP25S_5_ST    -   1
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_5_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_5_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_5_ST, F.C] = -0.0679;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_5_ST, F.D] = -0.2798;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_5_ST, F.E] = 146.46;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_5_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_5_ST, F.VITESSE] = 1800;
            //SP25S_7_ST    -   2
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_7_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_7_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_7_ST, F.C] = -0.0905;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_7_ST, F.D] = -0.5952;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_7_ST, F.E] = 206.25;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_7_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_7_ST, F.VITESSE] = 1800;
            //SP25S_9_ST    -   3
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_9_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_9_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_9_ST, F.C] = -0.1286;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_9_ST, F.D] = -0.4048;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_9_ST, F.E] = 265.83;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_9_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_9_ST, F.VITESSE] = 1800;
            //SP25S_11_ST    -   4
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_11_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_11_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_11_ST, F.C] = -0.1607;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_11_ST, F.D] = -0.1726;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_11_ST, F.E] = 321.46;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_11_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_11_ST, F.VITESSE] = 1800;
            //SP25S_15_ST    -   5
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_15_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_15_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_15_ST, F.C] = -0.2024;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_15_ST, F.D] = -0.9881;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_15_ST, F.E] = 443.33;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_15_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_15_ST, F.VITESSE] = 1800;
            //SP25S_26_ST    -   6
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_26_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_26_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_26_ST, F.C] = -0.3524;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_26_ST, F.D] = -1.6667;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_26_ST, F.E] = 770.83;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_26_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_26_ST, F.VITESSE] = 1800;
            //SP25S_39_ST    -   7
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_39_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_39_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_39_ST, F.C] = -0.5;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_39_ST, F.D] = -3.3095;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_39_ST, F.E] = 1154.2;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_39_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_39_ST, F.VITESSE] = 1800;
            //SP25S_52_ST    -   8
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_52_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_52_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_52_ST, F.C] = -0.7548;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_52_ST, F.D] = -2.2738;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_52_ST, F.E] = 1546.3;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_52_ST, F.MAX] = 35;
            formules[F.GRUNDFOS, F.SP25S, F.SP25S_52_ST, F.VITESSE] = 1800;
            //**************SERIE SP40S****************************************//
            //SP40S_3_ST    -   0
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_3_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_3_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_3_ST, F.C] = 0.0045;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_3_ST, F.D] = -0.7946;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_3_ST, F.E] = 79.107;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_3_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_3_ST, F.VITESSE] = 1800;
            //SP40S_5_ST    -   1
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_5_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_5_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_5_ST, F.C] = -0.0045;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_5_ST, F.D] = -0.8339;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_5_ST, F.E] = 141.61;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_5_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_5_ST, F.VITESSE] = 1800;
            //SP40S_7_ST    -   2
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_7_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_7_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_7_ST, F.C] = -0.0062;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_7_ST, F.D] = -1.2589;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_7_ST, F.E] = 200.54;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_7_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_7_ST, F.VITESSE] = 1800;
            //SP40S_9_ST    -   3
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_9_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_9_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_9_ST, F.C] = -0.0161;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_9_ST, F.D] = -1.225;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_9_ST, F.E] = 255.36;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_9_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_9_ST, F.VITESSE] = 1800;
            //SP40S_12_ST    -   4
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_12_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_12_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_12_ST, F.C] = -0.0232;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_12_ST, F.D] = -1.4821;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_12_ST, F.E] = 337.5;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_12_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_12_ST, F.VITESSE] = 1800;
            //SP40S_15_ST    -   5
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_15_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_15_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_15_ST, F.C] = -0.0348;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_15_ST, F.D] = -1.6018;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_15_ST, F.E] = 421.96;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_15_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_15_ST, F.VITESSE] = 1800;
            //SP40S_21_ST    -   6
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_21_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_21_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_21_ST, F.C] = -0.033;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_21_ST, F.D] = -2.9911;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_21_ST, F.E] = 595.89;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_21_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_21_ST, F.VITESSE] = 1800;
            //SP40S_25_ST    -   7
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_25_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_25_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_25_ST, F.C] = -0.0286;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_25_ST, F.D] = -4.1143;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_25_ST, F.E] = 705.71;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_25_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_25_ST, F.VITESSE] = 1800;
            //SP40S_30_ST    -   8
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_30_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_30_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_30_ST, F.C] = -0.0464;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_30_ST, F.D] = -4.2786;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_30_ST, F.E] = 842.86;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_30_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_30_ST, F.VITESSE] = 1800;
            //SP40S_37_ST    -   9
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_37_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_37_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_37_ST, F.C] = -0.0607;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_37_ST, F.D] = -5.9357;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_37_ST, F.E] = 1050.7;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_37_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_37_ST, F.VITESSE] = 1800;
            //SP40S_44_ST    -   10
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_44_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_44_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_44_ST, F.C] = -0.0714;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_44_ST, F.D] = -6.8571;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_44_ST, F.E] = 1238.6;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_44_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_44_ST, F.VITESSE] = 1800;
            //SP40S_50_ST    -   11
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_50_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_50_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_50_ST, F.C] = -0.0857;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_50_ST, F.D] = -7.5857;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_50_ST, F.E] = 1410.7;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_50_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_50_ST, F.VITESSE] = 1800;
            //SP40S_58_ST    -   12
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_58_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_58_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_58_ST, F.C] = -0.1;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_58_ST, F.D] = -8.4;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_58_ST, F.E] = 1630;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_58_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_58_ST, F.VITESSE] = 1800;
            //SP40S_66_ST    -   13
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_66_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_66_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_66_ST, F.C] = -0.1018;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_66_ST, F.D] = -10.511;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_66_ST, F.E] = 1856.1;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_66_ST, F.MAX] = 50;
            formules[F.GRUNDFOS, F.SP40S, F.SP40S_66_ST, F.VITESSE] = 1800;
            //**************SERIE SP60S****************************************//
            //SP60S_4_ST    -   0
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_4_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_4_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_4_ST, F.C] = -0.016;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_4_ST, F.D] = 0.3462;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_4_ST, F.E] = 125.87;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_4_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_4_ST, F.VITESSE] = 1800;
            //SP60S_5_ST    -   1
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_5_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_5_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_5_ST, F.C] = -0.0202;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_5_ST, F.D] = 0.5064;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_5_ST, F.E] = 156.61;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_5_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_5_ST, F.VITESSE] = 3600;
            //SP60S_7_ST    -   2
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_7_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_7_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_7_ST, F.C] = -0.0272;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_7_ST, F.D] = 0.6232;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_7_ST, F.E] = 222.76;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_7_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_7_ST, F.VITESSE] = 1800;
            //SP60S_9_ST    -   3
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_9_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_9_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_9_ST, F.C] = -0.036;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_9_ST, F.D] = 0.8165;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_9_ST, F.E] = 284.03;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_9_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_9_ST, F.VITESSE] = 1800;
            //SP60S_13_ST    -   4
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_13_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_13_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_13_ST, F.C] = -0.0532;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_13_ST, F.D] = 1.2597;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_13_ST, F.E] = 408.64;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_13_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_13_ST, F.VITESSE] = 1800;
            //SP60S_18_ST    -   5
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_18_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_18_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_18_ST, F.C] = -0.0754;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_18_ST, F.D] = 1.8513;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_18_ST, F.E] = 564.15;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_18_ST, F.MAX] = 80;
            formules[F.GRUNDFOS, F.SP60S, F.SP60S_18_ST, F.VITESSE] = 1800;
            //**************SERIE SP75S****************************************//
            //SP75S_3_ST    -   0
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_3_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_3_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_3_ST, F.C] = -0.0062;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_3_ST, F.D] = 0.0585;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_3_ST, F.E] = 95.473;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_3_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_3_ST, F.VITESSE] = 3600;
            //SP75S_5_ST    -   1
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_5_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_5_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_5_ST, F.C] = -0.0114;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_5_ST, F.D] = 0.1892;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_5_ST, F.E] = 156.68;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_5_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_5_ST, F.VITESSE] = 3600;
            //SP75S_8_ST    -   2
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_8_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_8_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_8_ST, F.C] = -0.0158;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_8_ST, F.D] = 0.1364;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_8_ST, F.E] = 253.27;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_8_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_8_ST, F.VITESSE] = 3600;
            //SP75S_11_ST    -   3
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_11_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_11_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_11_ST, F.C] = -0.0225;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_11_ST, F.D] = 0.1743;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_11_ST, F.E] = 348.77;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_11_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_11_ST, F.VITESSE] = 1800;
            //SP75S_12_ST    -   4
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_12_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_12_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_12_ST, F.C] = -0.0246;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_12_ST, F.D] = 0.2492;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_12_ST, F.E] = 378.45;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_12_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_12_ST, F.VITESSE] = 1800;
            //SP75S_15_ST    -   5
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_15_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_15_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_15_ST, F.C] = -0.032;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_15_ST, F.D] = 0.2201;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_15_ST, F.E] = 464.32;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_15_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_15_ST, F.VITESSE] = 1800;
            //SP75S_16_ST    -   6
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_16_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_16_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_16_ST, F.C] = -0.0326;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_16_ST, F.D] = 0.0864;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_16_ST, F.E] = 494.45;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_16_ST, F.MAX] = 90;
            formules[F.GRUNDFOS, F.SP75S, F.SP75S_16_ST, F.VITESSE] = 3600;
            //**************SERIE SP85S****************************************//
            //SP85S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP85S, F.SP85S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP85S, F.SP85S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP85S, F.SP85S_1_ST, F.C] = -0.0021;
            formules[F.GRUNDFOS, F.SP85S, F.SP85S_1_ST, F.D] = -0.0561;
            formules[F.GRUNDFOS, F.SP85S, F.SP85S_1_ST, F.E] = 56.5;
            formules[F.GRUNDFOS, F.SP85S, F.SP85S_1_ST, F.MAX] = 100;
            formules[F.GRUNDFOS, F.SP85S, F.SP85S_1_ST, F.VITESSE] = 3600;
            //**************SERIE SP150S****************************************//
            //SP150S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP150S, F.SP150S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP150S, F.SP150S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP150S, F.SP150S_1_ST, F.C] = -0.0007;
            formules[F.GRUNDFOS, F.SP150S, F.SP150S_1_ST, F.D] = -0.0345;
            formules[F.GRUNDFOS, F.SP150S, F.SP150S_1_ST, F.E] = 55.161;
            formules[F.GRUNDFOS, F.SP150S, F.SP150S_1_ST, F.MAX] = 200;
            formules[F.GRUNDFOS, F.SP150S, F.SP150S_1_ST, F.VITESSE] = 3600;
            //**************SERIE SP230S****************************************//
            //SP230S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP230S, F.SP230S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP230S, F.SP230S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP230S, F.SP230S_1_ST, F.C] = -0.0003;
            formules[F.GRUNDFOS, F.SP230S, F.SP230S_1_ST, F.D] = -0.0364;
            formules[F.GRUNDFOS, F.SP230S, F.SP230S_1_ST, F.E] = 64.143;
            formules[F.GRUNDFOS, F.SP230S, F.SP230S_1_ST, F.MAX] = 300;
            formules[F.GRUNDFOS, F.SP230S, F.SP230S_1_ST, F.VITESSE] = 3600;
            //**************SERIE SP300S****************************************//
            //SP300S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP300S, F.SP300S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP300S, F.SP300S_1_ST, F.B] = -0.0000002;
            formules[F.GRUNDFOS, F.SP300S, F.SP300S_1_ST, F.C] = -0.0004;
            formules[F.GRUNDFOS, F.SP300S, F.SP300S_1_ST, F.D] = -0.0215;
            formules[F.GRUNDFOS, F.SP300S, F.SP300S_1_ST, F.E] = 64.424;
            formules[F.GRUNDFOS, F.SP300S, F.SP300S_1_ST, F.MAX] = 400;
            formules[F.GRUNDFOS, F.SP300S, F.SP300S_1_ST, F.VITESSE] = 3600;
            //**************SERIE SP385S****************************************//
            //SP385S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP385S, F.SP385S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP385S, F.SP385S_1_ST, F.B] = -0.0000007;
            formules[F.GRUNDFOS, F.SP385S, F.SP385S_1_ST, F.C] = 0.0004;
            formules[F.GRUNDFOS, F.SP385S, F.SP385S_1_ST, F.D] = -0.1502;
            formules[F.GRUNDFOS, F.SP385S, F.SP385S_1_ST, F.E] = 101.83;
            formules[F.GRUNDFOS, F.SP385S, F.SP385S_1_ST, F.MAX] = 500;
            formules[F.GRUNDFOS, F.SP385S, F.SP385S_1_ST, F.VITESSE] = 1800;
            //**************SERIE SP475S****************************************//
            //SP475S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP475S, F.SP475S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP475S, F.SP475S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP475S, F.SP475S_1_ST, F.C] = -0.00004;
            formules[F.GRUNDFOS, F.SP475S, F.SP475S_1_ST, F.D] = -0.0921;
            formules[F.GRUNDFOS, F.SP475S, F.SP475S_1_ST, F.E] = 105.88;
            formules[F.GRUNDFOS, F.SP475S, F.SP475S_1_ST, F.MAX] = 600;
            formules[F.GRUNDFOS, F.SP475S, F.SP475S_1_ST, F.VITESSE] = 1800;
            //**************SERIE SP625S****************************************//
            //SP625S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP625S, F.SP625S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP625S, F.SP625S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP625S, F.SP625S_1_ST, F.C] = -0.00006;
            formules[F.GRUNDFOS, F.SP625S, F.SP625S_1_ST, F.D] = -0.0393;
            formules[F.GRUNDFOS, F.SP625S, F.SP625S_1_ST, F.E] = 147.55;
            formules[F.GRUNDFOS, F.SP625S, F.SP625S_1_ST, F.MAX] = 800;
            formules[F.GRUNDFOS, F.SP625S, F.SP625S_1_ST, F.VITESSE] = 1800;
            //**************SERIE SP800S****************************************//
            //SP800S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP800S, F.SP800S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP800S, F.SP800S_1_ST, F.B] = -0.00000004;
            formules[F.GRUNDFOS, F.SP800S, F.SP800S_1_ST, F.C] = 0.00001;
            formules[F.GRUNDFOS, F.SP800S, F.SP800S_1_ST, F.D] = -0.0709;
            formules[F.GRUNDFOS, F.SP800S, F.SP800S_1_ST, F.E] = 164.88;
            formules[F.GRUNDFOS, F.SP800S, F.SP800S_1_ST, F.MAX] = 1100;
            formules[F.GRUNDFOS, F.SP800S, F.SP800S_1_ST, F.VITESSE] = 3600;
            //**************SERIE SP1100S****************************************//
            //SP1100S_1_ST    -   0
            formules[F.GRUNDFOS, F.SP1100S, F.SP1100S_1_ST, F.A] = 0;
            formules[F.GRUNDFOS, F.SP1100S, F.SP1100S_1_ST, F.B] = 0;
            formules[F.GRUNDFOS, F.SP1100S, F.SP1100S_1_ST, F.C] = -0.00003;
            formules[F.GRUNDFOS, F.SP1100S, F.SP1100S_1_ST, F.D] = -0.0449;
            formules[F.GRUNDFOS, F.SP1100S, F.SP1100S_1_ST, F.E] = 186.58;
            formules[F.GRUNDFOS, F.SP1100S, F.SP1100S_1_ST, F.MAX] = 1400;
            formules[F.GRUNDFOS, F.SP1100S, F.SP1100S_1_ST, F.VITESSE] = 3600;

            //=================================================================
            ////NOUVELLE_MARQUE
            //=================================================================


            ////**************SERIE NOUVELLE_SERIE****************************************//
            ////NOUVEAU_MODELE    -   0
            //formules[F.NOUVELLE_MARQUE, F.NOUVELLE_SERIE, F.NOUVEAU_MODELE, F.A] = 0;
            //formules[F.NOUVELLE_MARQUE, F.NOUVELLE_SERIE, F.NOUVEAU_MODELE, F.B] = 0;
            //formules[F.NOUVELLE_MARQUE, F.NOUVELLE_SERIE, F.NOUVEAU_MODELE, F.C] = -0.00003;
            //formules[F.NOUVELLE_MARQUE, F.NOUVELLE_SERIE, F.NOUVEAU_MODELE, F.D] = -0.0449;
            //formules[F.NOUVELLE_MARQUE, F.NOUVELLE_SERIE, F.NOUVEAU_MODELE, F.E] = 186.58;
            //formules[F.NOUVELLE_MARQUE, F.NOUVELLE_SERIE, F.NOUVEAU_MODELE, F.MAX] = 1400;
            //formules[F.NOUVELLE_MARQUE, F.NOUVELLE_SERIE, F.NOUVEAU_MODELE, F.VITESSE] = 3600;
        }
    }
}
