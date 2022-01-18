using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace System_Curve__4._0
{
    public static class k
    {
        //Constantes pour determiner le nombre d'elements du tableau de formules
        public const int NB_MARQUES = 5;
        public const int NB_SERIE = 34;
        public const int NB_MODELE = 19;
        public const int NB_PARAMETRES = 18;
        //Constantes pour identifier les case du tableau dimensions
        public const int LARGEUR = 0;
        public const int HAUTEUR = 1;
        //Constantes de proportion
        public const int PROPORTION_DEUX = 2;
        public const int PROPORTION_TROIS = 3;
        public const int PROPORTION_QUATRE = 4;
        public const int PROPORTION_CINQ = 5;
        public const int PROPORTION_SIX = 6;
        public const int PROPORTION_SEPT = 7;
        public const int PROPORTION_HUIT = 8;
        public const int PROPORTION_NEUF = 9;
        public const int PROPORTION_DIX = 10;
        public const int PROPORTION_DOUZE = 12;
        public const int PROPORTION_TREIZE = 13;
        public const int PROPORTION_QUATORZE = 14;
        public const int PROPORTION_DIXSEPT = 17;
        public const int PROPORTION_DIXHUIT = 18;
        //Constantes de bordures pour les panneaux
        public const int ESPACE_PNL_MATERIEL = 13;
        public const int BORDURE_PNL = 6;
        public const int BORDURE_NUM_IN = 1;
        /*Constantes pour les tableaux de string tampons pour les fonctions de 
         * split*/
        public const int MATERIEL = 0;
        public const int HAZENWILLIAMS = 1;
        public const int PIPE_TYPE = 0;
        public const int DIAMETRE = 1;
        //Constante dans la formule HazenWilliams
        public const double CONSTANTE_HW = 0.002082;
        //Selection minimum de section
        public const int MIN_SELECT = 0;
        //Constantes pour les cases du tableau, selon chaque section
        public const int SECTION_A = 1;
        public const int SECTION_B = 2;
        public const int SECTION_C = 3;
        public const int SECTION_D = 4;
        public const int SECTION_E = 5;
        //?? Peut etre pas
        public const int MAX_SERIES = 5;
        //Pour le tableau 3 dimensions de courbes de pompe
        public const int MAX_POMPE = 25;
        public const int MAX_POINTS = 25;
        public const int COORDS = 2;
        //Coordonnees dans le tableau de courbes de pompe
        public const int X = 0;
        public const int Y = 1;
        //Constantes de colonnes pour la grille Excel
        public const int COLONNE_A = 1;
        public const int COLONNE_B = 2;
        public const int COLONNE_C = 3;
        public const int COLONNE_D = 4;
        public const int COLONNE_G = 7;
        public const int COLONNE_V = 22;

        public const int LEGENDE_DEBIT = 0;
        public const int LEGENDE_PRESSION = 1;
        //Constante pour les serie du graphique
        public const int SERIE_TUYAUTERIE = 0;
        public const int SERIE_POINT_ACTION_UN = 1;
        public const int SERIE_POINT_ACTION_DEUX = 2;
        public const int SERIE_POINT_ACTION_TROIS = 3;
        public const int SERIE_POMPE_1 = 4;
        public const int SERIE_POMPE_2 = 5;
        public const int SERIE_POMPE_3 = 6;
        public const int SERIE_POMPE_4 = 7;
        public const int SERIE_POMPE_5 = 8;
        public const int SERIE_POMPE_6 = 9;
        public const int SERIE_POMPE_7 = 10;
        public const int SERIE_POMPE_8 = 11;
        public const int SERIE_POMPE_9 = 12;
        public const int SERIE_POMPE_10 = 13;
        public const int SERIE_POMPE_11 = 14;
        public const int SERIE_POMPE_12 = 15;
        public const int SERIE_EFF_1 = 16;
        public const int SERIE_EFF_2 = 17;
        public const int SERIE_EFF_3 = 18;
        public const int SERIE_EFF_4 = 19;
        public const int SERIE_EFF_5 = 20;
        public const int SERIE_EFF_6 = 21;
        public const int SERIE_EFF_7 = 22;
        public const int SERIE_EFF_8 = 23;
        public const int SERIE_EFF_9 = 24;
        public const int SERIE_EFF_10 = 25;
        public const int SERIE_EFF_11 = 26;
        public const int SERIE_EFF_12 = 27;
        public const int SERIE_PUISS_1 = 28;
        public const int SERIE_PUISS_2 = 29;
        public const int SERIE_PUISS_3 = 30;
        public const int SERIE_PUISS_4 = 31;
        public const int SERIE_PUISS_5 = 32;
        public const int SERIE_PUISS_6 = 33;
        public const int SERIE_PUISS_7 = 34;
        public const int SERIE_PUISS_8 = 35;
        public const int SERIE_PUISS_9 = 36;
        public const int SERIE_PUISS_10 = 37;
        public const int SERIE_PUISS_11 = 38;
        public const int SERIE_PUISS_12 = 39;

        public const int LIGNE_MARQUE = 4;
        public const int LIGNE_NB_MARQUE = 2;
        public const int LIGNE_PAR_MODELE = 3;
        //Constantes pour identifier les pompes
        public const int POMPE_1 = 1;
        public const int POMPE_2 = 2;
        public const int POMPE_3 = 3;
        public const int POMPE_4 = 4;
        public const int POMPE_5 = 5;
        public const int POMPE_6 = 6;
        public const int POMPE_7 = 7;
        public const int POMPE_8 = 8;
        public const int POMPE_9 = 9;
        public const int POMPE_10 = 10;
        public const int POMPE_11 = 11;
        public const int POMPE_12 = 12;
        //Constantes de fittings
        public const int FITTING_1 = 0;
        public const int FITTING_2 = 1;
        public const int FITTING_3 = 2;
        public const int FITTING_4 = 3;
        public const int FITTING_5 = 4;
        public const int FITTING_6 = 5;
        public const int FITTING_7 = 6;
        public const int FITTING_8 = 7;
        public const int FITTING_9 = 8;
        public const int FITTING_10 = 9;
        //Constante de disposition des pompes
        public const int PARALLELE = 0;
        public const int SERIE = 1;
        //Constantes pour les offset - 
        //OFFSET_1 : pour les tableaux commencants a zero
        //OFFSET_3 : pour les series, les pompes commencent a 4
        //OFFSET_JONCTION : les series de jonction commencent a 15
        //serie 0 - courbe system
        //serie 1,2,3 - points d'action
        //4 a 14, les 12 pompes
        //15 et +, les jonctions
        public const int OFFSET_1 = 1;
        public const int OFFSET_3 = 3;
        public const int OFFSET_EFF = 15;
        public const int OFFSET_PUIS = 27;
        //Bits booleens pour la disposition des pompes
        public const bool DISPO_PARR = true;
        public const bool DISPO_SERIE = false;
        //Constante pour le nb de fintting
        public const int NB_MAX_FITTING = 10;
        //Constante de largeur du trait de la serie de points d'action, ligne graphique
        public const int EPAISSEUR_DROITE = 8;
        public const int EPAISSERUR_POINT = 15;
        //Constantes boolleenes pour le mode de tracage du point d'action
        public const bool ACTION_DROITE = true;
        public const bool ACTION_POINT = false;
        //Constantes pour les calcul des valeurs du tableur et les intervalles
        public const int NB_POINTS_TABLEUR = 10;
        public const int NB_POINTS_TABLEUR_INTERVALLE = 11;
        //Constantes de valeurs par defaut
        public const string MSG_DEPART = "";
        public const int VALEUR_DEPART = 0;
        public const int NB_LIGNE_DEPART = 1;
        public const int NB_POMPE_DEPART = 1;
        public const int NB_STAGE_DEPART = 1;
        public const int RATIO_DEPART = 100;
        public const int NB_POMPE_PAR_DEFAUT = 1;
        public const int NB_STAGES_PAR_DEFAUT = 1;
        public const int RATIO_PAR_DEFAUT = 100;
        public const int VITESSE_PAR_DEFAUT = 3600;
        public const int POURCENTAGE = 100;
        //Constante pour le tableau inputs de la page Accueil
        public const int NOM_CLIENT = 0;
        public const int PROJET = 1;
        public const int DESCRIPTION = 2;
        public const int FAITPAR = 3;
        public const int REVISION = 4;
        public const int DATE = 5;
        //Constante pour le tableau inputs de la page Point d'action
        public const int DEBIT_AXE_X = 0;
        public const int DEBIT_P1 = 1;
        public const int DEBIT_P2 = 2;
        public const int DEBIT_P3 = 3;

        public const int PRESSION_P1 = 1;
        public const int PRESSION_P2 = 2;
        public const int PRESSION_P3 = 3;

        public const int DROITE_P1 = 1;
        public const int DROITE_P2 = 2;
        public const int DROITE_P3 = 3;

        public const int LEGENDE_P1 = 1;
        public const int LEGENDE_P2 = 2;
        public const int LEGENDE_P3 = 3;


        public const int LIGNE_X = 0;
        public const int LIGNE_Y = 1;

        public const int PREMIER_POINT = 1;
        public const int DERNIER_POINT = 12;

        public const double RATIO_GPM = 4.402868;

        public const int PAGE_ACCUEIL = 0;
        public const int PAGE_ACTION = 1;
        public const int PAGE_TUYAUTERIE = 2;
        public const int PAGE_POMPE = 3;

        public const int LARGEUR_UNITES = 30;

        //Longueur
        public const double METRES_A_PIEDS = 3.28084;
        //public const double METRES_A_PIEDS = 0.3048;
        public const double PIEDS = 1;

        //Pression
        public const double FTH20_A_PSI = 0.433515;
        public const double FTH20 = 1;
        public const double FTH20_A_MH20 = 0.304878048;
        //public const double FTH20_A_MH20 = 3.28084;
        public const double FTH20_A_KPA = 2.98898;
        //Debit
        public const double USPGM = 1;
        public const double M3H_A_USGPM = 4.402868;
        //Puissance
        public const double HP = 1;
        public const double WATTS = 745.7;

        public const int GRAPH_PRINCIPAL = 0;
        public const int GRAPH_SECONDAIRE = 1;
        public const int GRAPH_TROIS = 2;

        public const int VIDE = -1;
    }

    public struct t_section
    {
        //Etat de la section pour les le calcul
        public bool actif;

        //Selection de materiel qui est le contenu du cBox
        public string selection_materiel;
        //Le materiel selectionne contient un nom de materiel et une constante
        public string materiel;
        public double constante_hazen_williams;

        //Selection de type de tuyau qui est le contenu cBox
        public string selection_pipe_type;
        //Le type de tuyauterie comporte une type de tuyau et un diametre interne
        public string pipe_type;
        public double diametre_interne;

        //Nombre de lignes parallele
        public int num_parallel_lines;
        //Longueur de la section en pieds
        public double longueur_section;
        //Hauteur de tete statique
        public double static_head;
        //La longueur equivalente des fittings
        public double fitting_tot;
        //Tableau des fittings selectionnes
        public t_fitting[] fitting;
        //Le facteur de securite
        public double safety_factor;
    }

    public struct t_sauvegarde_segment
    {
        public string actif;
        public string materiel;
        public string pipe_type;
        public string num_parallele_lines;
        public string longueur;
        public string static_head;
        public t_save_fitting[] fitting;
        public string safety_factor;
    }

    public struct t_save_fitting
    {
        public string quantite;
        public string fitting;
    }

    public struct t_fitting
    {
        public double quantite;
        public string fitting;
        public double equivalent;
    }

    public struct t_modeles
    {
        public string modele;
        public int ligne;
    }

    public struct t_marques
    {
        public string marque;
        public int ligne_commencement;
        public int nb_modeles;
        public t_modeles[] modeles;
    }

    public struct t_page_acceuil
    {
        public string[] info_client;
        public string langue;
        public string unit_longueur_string;
        public double unit_longueur;
        public string unit_pression_string;
        public double unit_pression;
        public string unit_debit_string;
        public double unit_debit;
        public string unit_puissance_string;
        public double unit_puissance;
    }

    public struct t_page_action
    {
        public string[] debit;
        public string[] pression;
        public string[] droite_ou_point;
        public string[] legende_auto;
        public string[] legende;
    }
    public struct t_inputs_action
    {
        public double[] debit;
        public double[] pression;
        public bool[] droite;
        public bool[] legende_auto;
        public string[] legende;
        public bool graphique_secondaire;
    }

    public struct t_page_tuyauterie
    {
        public t_sauvegarde_segment[] segment;
    }

    public struct t_save_pompe
    {
        public string marque;
        public string modele;
        public string nb_pompe;
        public string disposition;
    }

    public struct t_pompe
    {
        public string marque;
        public int index_marque;
        public string serie;
        public int index_serie;
        public string modele;
        public int index_modele;
        public int nb_pompe;
        public int nb_stages;
        public bool disposition;
        public double ratio_diametre;
        public double vitesse;
    }

    public struct t_page_pompe
    {
        public t_save_pompe[] pompe;
    }

    public struct t_sauvegarde
    {
        public t_page_acceuil save_acceuil;
        public t_page_action save_action;
        public t_page_tuyauterie save_tuyauterie;
        public t_page_pompe save_pompe;
    }


    public struct t_inputs
    {
        public t_page_acceuil acceuil;
        public t_inputs_action action;
        public t_section[] section;
        public t_pompe[] pompes;
    }


    public struct Dossier_Excel
    {
        public Excel.Application App;
        public Excel.Workbook Workbooks;
        public Excel._Worksheet WorkSheet;
        public Excel.Range Range;
        public int Nombre_Lignes;
        public int Nombre_Colonnes;
    }


    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Page_Principale());
        }
    }

    public class ajust
    {
        public static void obtention_dimensions_ecran(int[] dimensions)
        {
            int bordure = Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;

            dimensions[k.LARGEUR] = Screen.PrimaryScreen.WorkingArea.Width;
            //dimensions[k.HAUTEUR] = Screen.PrimaryScreen.WorkingArea.Height - bordure;
            dimensions[k.HAUTEUR] = Screen.PrimaryScreen.WorkingArea.Height - bordure;
        }

        //Ajustement du panneau bleu au bas de la fenêtre
        public static int ajustement_pnl_bas(int dimensions)
        {
            return 0;
            //return dimensions / k.PROPORTION_HUIT;
        }
    }

    public class init
    {
        public static void demarrage_inputs(t_inputs inputs)
        {
            page_accueil(inputs);
            page_action(inputs);
            page_section(inputs);
            page_pompes(inputs);
        }
         
        public static void page_accueil(t_inputs inputs)
        {
            init.tab_string(inputs.acceuil.info_client, 6, "");
            inputs.acceuil.langue = "francais";
            inputs.acceuil.unit_debit_string = "usgpm";
            inputs.acceuil.unit_pression_string = "fth20";
            inputs.acceuil.unit_puissance_string = "hp";
            inputs.acceuil.unit_longueur_string = "pieds";
        }

        public static void page_action(t_inputs inputs)
        {
            init.tab_1d_double(inputs.action.debit, 4);
            init.tab_1d_double(inputs.action.pression, 4);
            init.tab_bool_true(inputs.action.droite, 4);
            init.tab_bool_true(inputs.action.legende_auto, 4);
            init.tab_string(inputs.action.legende, 4, "Point d'action");
            //inputs.action.graphique_secondaire = true;
        }

        public static void page_section(t_inputs inputs)
        {
            for (int i = k.SECTION_A; i <= k.SECTION_E; i++)
            {
                inputs.section[i].actif = false;
                inputs.section[i].selection_materiel = k.MSG_DEPART;
                inputs.section[i].materiel = k.MSG_DEPART;
                inputs.section[i].constante_hazen_williams = k.VALEUR_DEPART;
                inputs.section[i].selection_pipe_type = k.MSG_DEPART;
                inputs.section[i].pipe_type = k.MSG_DEPART;
                inputs.section[i].diametre_interne = k.VALEUR_DEPART;
                inputs.section[i].num_parallel_lines = k.NB_LIGNE_DEPART;
                inputs.section[i].longueur_section = k.VALEUR_DEPART;
                inputs.section[i].static_head = k.VALEUR_DEPART;
                inputs.section[i].fitting_tot = k.VALEUR_DEPART;
                init.tab_t_fitting(inputs, i);
                inputs.section[i].safety_factor = k.VALEUR_DEPART;
            }
        }
        public static void page_pompes(t_inputs inputs)
        {
            for (int i = k.POMPE_1; i <= k.POMPE_12; i++)
            {
                inputs.pompes[i].marque = null;
                inputs.pompes[i].index_marque = k.VIDE;
                inputs.pompes[i].serie = null;
                inputs.pompes[i].index_serie = k.VIDE;
                inputs.pompes[i].modele = null;
                inputs.pompes[i].index_modele = k.VIDE;
                inputs.pompes[i].nb_pompe = k.NB_POMPE_DEPART;
                inputs.pompes[i].disposition = k.DISPO_PARR;
            }
        }

        public static void tab_t_fitting(t_inputs inputs, int no_section)
        {
            for (int i = 0; i < k.NB_MAX_FITTING; i++)
            {
                inputs.section[no_section].fitting[i].quantite = 0;
                inputs.section[no_section].fitting[i].fitting = null;
            }
        }

        //Initialisation ou remise à zéro d'un tableau 1 dimension
        public static void tab_1d_double(double[] tab, int longueur)
        {
            int i;

            for (i = 0; i < longueur; i++)
            {
                tab[i] = 0;
            }
        }

        public static void tab_1d_int(int[] tab, int longueur)
        {
            int i;

            for (i = 0; i < longueur; i++)
            {
                tab[i] = 0;
            }
        }

        public static void tab_2d_double(double[,] tab_2d, int longueur_d1,
                                    int longueur_d2)
        {
            int i;
            int j;

            for (i = 0; i < longueur_d1; i++)
            {
                for (j = 0; j < longueur_d2; j++)
                {
                    tab_2d[i, j] = 0;
                }
            }
        }

        public static void tab_3d_int(int[,,] tab_3d, int longueur_d1, int longueur_d2, int longueur_d3)
        {
            int i;
            int j;
            int k;

            for (i = 0; i < longueur_d1; i++)
            {
                for (j = 0; j < longueur_d2; j++)
                {
                    for (k = 0; k < longueur_d3; k++)
                    {
                        tab_3d[i, j, k] = 0;
                    }
                }
            }

        }

        public static void tab_bool_false(bool[] tab_bool, int longueur)
        {
            int i;

            for (i = 0; i < longueur; i++)
            {
                tab_bool[i] = false;
            }
        }

        public static void tab_bool_true(bool[] tab_bool, int longueur)
        {
            int i;

            for (i = 0; i < longueur; i++)
            {
                tab_bool[i] = true;
            }
        }

        public static void tab_string(string[] tab_string, int longueur, string msg_init)
        {
            int i;

            for (i = 0; i < longueur; i++)
            {
                tab_string[i] = msg_init;
            }
        }

        //Methode pour initialiser le nombre de ligne parallele a la valeur min
        // de 1, pour eviter la division par zero
        public static void ligne_parrallel_a_un(t_section[] section)
        {
            for (int i = 1; i <= 5; i++)
            {
                section[i].num_parallel_lines = 1;
            }
        }
        /*Methode pour initialiser le nombre de pompe a la valeur minimale de 1
         pour eviter la multiplication par 0*/
        public static void nb_pompes_a_un(t_pompe[] pompe)
        {
            for (int i = 0; i <= k.POMPE_12; i++)
            {
                pompe[i].nb_pompe = 1;
            }
        }
        /*Methode pour initialiser le nombre de stage a la valeur minimale de 1
         pour eviter la multiplication par 0*/
        public static void nb_stages_a_un(t_pompe[] pompe)
        {
            for (int i = 0; i <= k.POMPE_12; i++)
            {
                pompe[i].nb_stages = 1;
            }
        }
        /*Methode pour initialiser le ratio a la valeur maximale de 1, pour que
         le tracage de la courbe se fasse a la valeur par defaut*/
        public static void ratio_a_un(t_pompe[] pompe)
        {
            for (int i = 0; i <= k.POMPE_12; i++)
            {
                pompe[i].ratio_diametre = 1;
            }
        }
        /*Methode pour initialiser les unite a 1, donc valeur par defaut qui 
         equivaut a la longueur en pied, la pression en ftH20 et le debit en 
         USGPM*/
        public static t_page_acceuil unite_a_un(t_page_acceuil acceuil)
        {
            acceuil.unit_debit = k.USPGM;
            acceuil.unit_pression = k.FTH20;
            acceuil.unit_longueur = k.PIEDS;
            acceuil.unit_puissance = k.HP;

            return acceuil;
        }
    }

    public class declaration
    {
        //Declaration des tableau contenus dans le struc t_inputs
        public static t_inputs declaration_inputs()
        {
            t_inputs inputs = new t_inputs();

            //Page accueil
            inputs.acceuil.info_client = new string[6];
            //Page de point d'action
            inputs.action.debit = new double[4];
            inputs.action.pression = new double[4];
            inputs.action.droite = new bool[4];
            inputs.action.legende_auto = new bool[4];
            inputs.action.legende = new string[4];
            //Page de tuyauterie
            inputs.section = new t_section[6];
            //Section des fittings
            for (int i = k.SECTION_A; i <= k.SECTION_E; i++)
            {
                inputs.section[i].fitting = new t_fitting[k.NB_MAX_FITTING];
            }
            //Page de pompes
            inputs.pompes = new t_pompe[13];
            //Retour du struct declare
            return inputs;
        }
    }

    public class calcul
    {
        public static double obtenir_equivalent_fitting(string fitting)
        {
            //Reception du contenu du cBox
            string[] equivalent = new string[2];
            //Split pou separer le nom du fittin de son equivalence
            equivalent = fitting.Split('/');
            //Suppression des espaces devant
            if (equivalent[1].StartsWith("   "))
            {
                equivalent[1].Remove(3);
            }
            //Retour de l'equivalence convertie en double
            return double.Parse(equivalent[1]);
        }


        public static double longueur_equiv(t_inputs inputs, int section, int no_fitting)
        {
            return (inputs.section[section].fitting[no_fitting].quantite *
                        calcul.obtenir_equivalent_fitting(inputs.section[section].fitting[no_fitting].fitting));
        }


        //Prend le max et le divise pas le nombre d'intervalle.  Utile pour le tableur
        //qui prend le debit maxiamal divise par le nombre de case
        public static double intervalle_en_x(double max, int nb_intervalles)
        {
            return max / nb_intervalles;
        }

        //REcoit un intervalle et la taille du tableau et le rempli
        public static double[] intervalle_debit(double intervalle, int taille)
        {
            double[] intervalles_debit = new double[taille];

            //Remplissage des cases du tableau intervalles
            for (int i = 0; i < taille; i++)
            {
                intervalles_debit[i] = intervalle * i;
            }
            return intervalles_debit;
        }

        public static void TDH_section(double[,] TDH, t_section[] section, bool[] matiere_select, bool[] type_select, double[] intervalles, bool[] sections_actives, double unit_pression, double unit_longueur, double unit_debit)
        {

            //Variables d'incrémentation, 
            //1er dimension : Section, 2e dimension : Incrément de débit  
            int segment_i;
            int debit_j;

            double valeur_L;
            double valeur_L_unite;

            double valeur_tete_static;
            double valeur_tete_static_unite;

            double TDH_simple;
            double TDH_static;
            double TDH_safety;
            double TDH_unite;

            //L'incrémenteur i représente les sections
            for (segment_i = k.SECTION_A; segment_i <= k.SECTION_E; segment_i++)
            {
                if (section[segment_i].actif)
                {
                    //Addition de la longueur de section et des fittings
                    valeur_L = (section[segment_i].longueur_section + section[segment_i].fitting_tot);
                    //Conversion a l'unite selectionne
                    valeur_L_unite = L_unite(valeur_L, unit_longueur);

                    valeur_tete_static = L_unite(section[segment_i].static_head, unit_longueur);
                    //valeur_tete_static_unite = tete_static_unite(valeur_tete_static, unit_longueur);

                    //Copie du diametre - simplement pour aider la lecture du code
                    double d = section[segment_i].diametre_interne;
                    //Coefficient mis en centieme
                    double coefficient = 100 /
                        section[segment_i].constante_hazen_williams;
                    //Facteur de securite mis en centieme
                    double safety = 1 + (section[segment_i].safety_factor / 100);

                    for (debit_j = 0; debit_j < 11; debit_j++)
                    {
                        //Débit q = incrément * 100 : 
                        //Augmente de tranches de 100 pour 10 cases
                        //int q = debit_j * intervalle;

                        double q = ((int)intervalles[debit_j] / section[segment_i].num_parallel_lines) * unit_debit;

                        if (section[segment_i].actif)
                        {
                            //TDH = 0.002083 * L * (100/C)^1.85 * 
                            //(gpm^1.85 / d^4.8655)
                            //L = Longueur de section en pieds
                            //C = Coefficient de friction
                            //gpm = US
                            //d = diamètre intérieur du tuyau en pouce


                            TDH_simple = (k.CONSTANTE_HW * valeur_L_unite * Math.Pow(coefficient, 1.852)
                                    * Math.Pow(q, 1.852) / Math.Pow(d, 4.8655));

                            TDH_safety = TDH_simple * safety;

                            TDH_static = TDH_safety + valeur_tete_static;                        

                            TDH_unite = TDH_static * unit_pression;

                            TDH[segment_i, debit_j] = TDH_unite;
                        }
                    }
                }
                else
                {
                    for (debit_j = 0; debit_j < 11; debit_j++)
                    {
                        TDH[segment_i, debit_j] = 0;
                    }
                }
            }
        }

        private static double L_unite(double valeur_L, double unit_longueur)
        {
            return valeur_L * unit_longueur;
        }

        private static double tete_static_unite(double valeur_tete_static, double unit_longueur)
        {
            return valeur_tete_static * unit_longueur;
        }

        public static double TDH_en_un_point(double x, t_section[] section, double unit_pression, double unit_longueur, double unit_debit)
        {
            double TDH = 0;
            double TDH_tot_en_un_point = 0;

            for (int segment_i = k.SECTION_A; segment_i <= k.SECTION_E; segment_i++)
            {
                if (section[segment_i].actif == true)
                {
                    double L = (section[segment_i].longueur_section + section[segment_i].fitting_tot) * unit_longueur;
                    double d = section[segment_i].diametre_interne;
                    double coefficient = 100 /
                        section[segment_i].constante_hazen_williams;
                    double safety = 1 + (section[segment_i].safety_factor / 100);

                    //TDH = 0.002083 * L * (100/C)^1.85 * 
                    //(gpm^1.85 / d^4.8655)
                    //L = Longueur de section en pieds
                    //C = Coefficient de friction
                    //gpm = US
                    //d = diamètre intérieur du tuyau en pouce
                    TDH =
                        (((k.CONSTANTE_HW * L * Math.Pow(coefficient, 1.852)
                            * Math.Pow(x, 1.852) / Math.Pow(d, 4.8655) * safety)
                                    + (section[segment_i].static_head) * unit_longueur) * unit_pression);

                    TDH_tot_en_un_point = TDH_tot_en_un_point + TDH;
                }
            }
            return TDH_tot_en_un_point;
        }


        public static void TDH_total(double[] TDH_tot, double[,] TDH, bool[] matiere_select, bool[] type_select, double[] intervalles, bool[] sections_actives)
        {
            //Variables d'incrémentation, 
            //1er dimension : Section, 2e dimension : Incrément de débit  
            int segment_i;
            int debit_j;

            init.tab_1d_double(TDH_tot, 12);

            for (debit_j = 0; debit_j < 11; debit_j++)
            {
                //int q = (int)intervalles[debit_j];

                for (segment_i = k.SECTION_A; segment_i <= k.SECTION_E;
                        segment_i++)
                {
                    if (sections_actives[segment_i])
                    {
                        //Ne pas arrondir (cela provoque un bog dans le tracage de la courbe graphique)
                        TDH_tot[debit_j] += TDH[segment_i, debit_j];
                    }
                }
            }
        }
    }

    public class selection
    {
        //Recoit un tableau t_marque et une marque
        //La fonction recherche cette marque et retourne la position dans le tableau
        public static int recherche_marque(string contenur_cBox, t_marques[] marques)
        {
            int i = 0;
            //Recherche la marque
            while (string.Compare(contenur_cBox, marques[i].marque) != 0)
            {
                i++;
            }
            return i;
        }
        //REcoit un tableau t_marque et une position(represente une marque) et un modele
        //La fontion recherche ce modele et retourne la position dans le tableau
        public static int recherche_modele(string contenu_cBox, t_marques[] marques, int pos_marque)
        {
            int i = 0;
            //Recherche le modele dans la marque
            while (string.Compare(contenu_cBox, marques[pos_marque].modeles[i].modele) != 0)
            {
                i++;
            }
            return i;
        }


    }

    public class fonction
    {
        //Obtient la date du jour et cree une chaine de caractere contenant cette date.
        public static string obtenir_date()
        {
            DateTime Date = DateTime.Now;
            return String.Concat(Date.Year.ToString(), '-', Date.Month.ToString().PadLeft(2, '0'), '-', Date.Day.ToString().PadLeft(2, '0'));
        }
    }

    public class manip_string
    {
        //Obtention du materiel et sa constante HW d'apres un split du cBox.
        public static void update_tab_section_materiel(string materiel, int no_section, t_inputs inputs)
        {
            inputs.section[no_section].selection_materiel = materiel;

            //Tableau de variables tampons pour recevoir le contenu du cBox
            string[] item_materiel;

            //Séparation des deux items dans le combo box.
            //Format : MATERIEL - HAZENWILLIAMS
            //item[0] - MATERIEL      item[1] - HAZENWILLLIAMS
            item_materiel = materiel.Split('/');

            /*Les espaces ne sont là que pour la lisibilité de la sélection*/
            //Retrait de l'espace suivant le materiel seulement s'il y en a un.
            if (item_materiel[k.MATERIEL].EndsWith(" "))
            {
                //Met le string dans la case du type t_section(struct)
                inputs.section[no_section].materiel =          //Case struct
                item_materiel[k.MATERIEL].Remove          //Retrait
                (item_materiel[k.MATERIEL].Length - 1);   //de l'espace
            }

            /*Retrait de l'espace précédant la constant HazenWilliams seulement
              s'il y en a un.*/
            if (item_materiel[k.HAZENWILLIAMS].StartsWith(" "))
            {
                item_materiel[k.HAZENWILLIAMS] =                 //Retrait de 
                item_materiel[k.HAZENWILLIAMS].Remove(0, 1);     //de l'espace
                /*Met la constante converti en nombre entier dans la case du 
                 * type t_section(struct)*/
                inputs.section[no_section].constante_hazen_williams =
                    double.Parse(item_materiel[k.HAZENWILLIAMS]);
            }
        }
        //Obtention du type de tuyau et son diametre d'apres un split du cBox.
        public static void update_tab_section_type(string type, int no_section, t_inputs inputs)
        {
            inputs.section[no_section].selection_pipe_type = type;

            //Tableau de variables tampons pour recevoir le contenu du cBox
            string[] item_type;

            //Séparation des deux items dans le combo box.
            //Format : MATERIEL - HAZENWILLIAMS
            //item[0] - MATERIEL      item[1] - HAZENWILLLIAMS
            item_type = type.Split('/');

            /*Les espaces ne sont là que pour la lisibilité de la sélection*/
            //Retrait de l'espace suivant le materiel seulement s'il y en a un.
            if (item_type[k.PIPE_TYPE].EndsWith(" "))
            {
                //Met le string dans la case du type t_section(struct)
                inputs.section[no_section].pipe_type =
                  item_type[k.PIPE_TYPE].Remove(item_type[k.PIPE_TYPE].Length - 1);
            }

            /*Retrait de l'espace précédant la constant HazenWilliams seulement
              s'il y en a un.*/
            if (item_type[k.DIAMETRE].StartsWith(" "))
            {
                item_type[k.DIAMETRE] = item_type[k.DIAMETRE].Remove(0, 1);

                /*Met la constante converti en type double dans la case du type
                 * t_section(struct)*/
                //inputs.section[no_section].diametre_interne =
                //    (float)(Convert.ToDouble(item_type[k.DIAMETRE]));

                 
                

                inputs.section[no_section].diametre_interne =
    float.Parse(item_type[k.DIAMETRE]);
            }
        }
    }
}









