using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System_Curve__4._0
{
    public partial class Page_Principale : Form
    {

        /*==================Constantes=======================================*/
        #region Constantes
        const int LARGEUR = 0;
        const int HAUTEUR = 1;
        //Constantes de proportion
        const int PROPORTION_DEUX = 2;
        const int PROPORTION_TROIS = 3;
        const int PROPORTION_QUATRE = 4;
        const int PROPORTION_CINQ = 5;
        const int PROPORTION_SIX = 6;
        const int PROPORTION_SEPT = 7;
        const int PROPORTION_HUIT = 8;
        const int PROPORTION_NEUF = 9;
        const int PROPORTION_DIX = 10;
        const int PROPORTION_SEIZE = 18;
        //Constantes de bordures pour les panneaux
        const int ESPACE_PNL_MATERIEL = 13;
        const int BORDURE_PNL = 6;
        /*Constantes pour les tableaux de string tampons pour les fonctions de 
         * split*/
        const int MATERIEL = 0;
        const int HAZENWILLIAMS = 1;
        const int PIPE_TYPE = 0;
        const int DIAMETRE = 1;

        const double CONSTANTE_HW = 0.002082;

        const int MIN_SELECT = 0;

        //Constantes pour les cases du tableau, selon chaque section
        const int SECTION_A = 1;
        const int SECTION_B = 2;
        const int SECTION_C = 3;
        const int SECTION_D = 4;
        const int SECTION_E = 5;

        const int MAX_SERIES = 5;

        #endregion //Constantes


        /*==================Variables========================================*/
        #region Variables

        int nombre_Serie = 0;

        //Tableau contenant le suivi des sections sélectionnées, la case 0 sert
        //pour l'initialisation (si aucun cBox, n'a été sélectionné, bool[0] == FALSE)
        bool[] materiel_select = new bool[6];
        bool[] type_select = new bool[6];

        /*============ Tableau statique de type t_section, pour insérer le 
         * contenu des cBox une fois converti au format =====================*/
        //Chaque case représente une section
        /*Chaques section contient le materiel, la constante HazenWilliam, le
           type de tuyau et le diamètre intérieur*/
        t_section[] section = new t_section[6];

        int[] dimensions = new int[2];

        //Tableau 2 dimensions pour contenir les valeur de TDH pour chaque 
        //section
        double[,] TDH = new double[6, 11];
        double[] TDH_tot = new double[11];

        #endregion  //Variables

        //=====================================================================
        public Page_Principale()
        {
            InitializeComponent();
        }

        //À l'ouverture de la page principale
        private void Page_Principale_Load(object sender, EventArgs e)
        {
            pnl_Page_Principale.BringToFront();
            r_Btn_RAZ.Checked = true;

            //Ajustement des dimensions selon les dimensions de l'écran
            obtention_dimensions_ecran();
            ajustement_ecran();
            ajustement_pnl_bas();
            ajustement_boutons();
            ajustement_pnl_materiel_type();
            init_tab_2d_double(TDH, 6, 11);
            init_tab_bool(materiel_select, 6);
            init_tab_bool(type_select, 6);

            


            //OUverture de la page principale à l'ouverture du programme
        }

        /*****************************************************************************/
        //Région contenant les fonctions d'ajustement, dimensions et locations
        #region AJUSTEMENT

        void obtention_dimensions_ecran()
        {
            dimensions[LARGEUR] = Screen.PrimaryScreen.Bounds.Width;
            dimensions[HAUTEUR] = Screen.PrimaryScreen.Bounds.Height;
        }
        //Ajustement de la fenêtre selon les dimensions de l'écran
        void ajustement_ecran()
        {
            //Ajustement de l'écran selon la résolution
            this.Width = dimensions[LARGEUR];
            this.Height = dimensions[HAUTEUR];
        }

        //Ajustement du panneau bleu au bas de la fenêtre
        void ajustement_pnl_bas()
        {
            pnl_Bas.Height = dimensions[HAUTEUR] / PROPORTION_HUIT;
        }

        //Ajustement des boutons au bas de l'écran
        void ajustement_boutons()
        {
            /*Calcul des dimensions des boutons selon les dimensions de 
              l'écran*/
            int hauteur_bouton = dimensions[HAUTEUR] / PROPORTION_DIX;
            int largeur_bouton = dimensions[LARGEUR] / PROPORTION_CINQ;
            int demi_largeur = largeur_bouton / 2;

            //Ajustement de la grosseur du bouton hydraulique
            btn_selection_hydraulique.Height = hauteur_bouton;
            btn_selection_hydraulique.Width = largeur_bouton;

            //Ajustement de la grosserur du bouton accueil
            btn_Acceuil.Height = hauteur_bouton;
            btn_Acceuil.Width = largeur_bouton;

            //Ajustement de la grosseur du bouton toto
            button1.Height = hauteur_bouton;
            button1.Width = largeur_bouton;

            //Ajustement grosseur bouton tata
            button3.Height = hauteur_bouton;
            button3.Width = largeur_bouton;

            /*Ajustement de sa position dans l'écran*/
            //Au quart de l'écran
            int pos_x_btn_gauche = (dimensions[LARGEUR] / PROPORTION_QUATRE) -
                                        demi_largeur;
            //À la moitié de l'écran
            int pos_x_btn_centre = (dimensions[LARGEUR] / PROPORTION_DEUX) -
                                        demi_largeur;
            //Au trois quart de l'écran
            int pos_x_btn_droite = ((dimensions[LARGEUR] * PROPORTION_TROIS) /
                                        PROPORTION_QUATRE) - demi_largeur;

            //Position en hauteur pour tout les boutons
            int pos_y_btn = pnl_Bas.Height / PROPORTION_QUATRE;

            //Positionnement du bouton hydraulique
            btn_selection_hydraulique.Location = new Point(pos_x_btn_gauche,
                                                            pos_y_btn);
            button1.Location = new Point(pos_x_btn_centre, pos_y_btn);
            button3.Location = new Point(pos_x_btn_droite, pos_y_btn);
        }

        //Ajustement du tableur
        void ajustement_tableur()
        {
            //Largeur écran divisé par 2.5
            int largeur_tableur = (dimensions[LARGEUR] * 2) / 5;
            int hauteur_tableur = (dimensions[HAUTEUR] * 14) / 5;

            pnl_Tableur.Height = hauteur_tableur;
            pnl_Tableur.Width = largeur_tableur;

            int largeur_case = (largeur_tableur - 14) / 6;
            pnl_t_1.Width = largeur_case;
            pnl_t_2.Width = largeur_case;
            pnl_t_3.Width = largeur_case;
            pnl_t_4.Width = largeur_case;
            pnl_t_5.Width = largeur_case;
            pnl_t_6.Width = largeur_case;

            int hauteur_case_t = 40;
            pnl_t_1.Height = hauteur_case_t;
            pnl_t_2.Height = hauteur_case_t;
            pnl_t_3.Height = hauteur_case_t;
            pnl_t_4.Height = hauteur_case_t;
            pnl_t_5.Height = hauteur_case_t;
            pnl_t_6.Height = hauteur_case_t;
        }

        //Ajustement des panneaus en haut de l'écran contenant les cBox
        private void ajustement_pnl_materiel_type()
        {
            //Region Contenant les hateur des composants dans Form
            #region Hauteur_Composants Graphiques

            #region Hauteur Panneaux
            //Obtention de la hauteur des panneaux
            int hauteur_pnl = dimensions[HAUTEUR] / PROPORTION_SEIZE;
            //AJustement de la hauteur des panneaux Materiel
            pnl_Materiel1.Height = hauteur_pnl;
            pnl_Materiel2.Height = hauteur_pnl;
            pnl_Materiel3.Height = hauteur_pnl;
            pnl_Materiel4.Height = hauteur_pnl;
            pnl_Materiel5.Height = hauteur_pnl;
            //Ajustement de la hauteur des panneaux Type
            pnl_Type1.Height = hauteur_pnl;
            pnl_Type2.Height = hauteur_pnl;
            pnl_Type3.Height = hauteur_pnl;
            pnl_Type4.Height = hauteur_pnl;
            pnl_Type5.Height = hauteur_pnl;
            //AJustement de la hauteur des panneaux Titre
            pnl_Titre1.Height = hauteur_pnl;
            pnl_Titre2.Height = hauteur_pnl;
            pnl_Titre3.Height = hauteur_pnl;
            pnl_Titre4.Height = hauteur_pnl;
            pnl_Titre5.Height = hauteur_pnl;
            //Ajustement de la hauteur des panneaux Lignes Paralleles
            pnl_lignes_par1.Height = hauteur_pnl;
            pnl_lignes_par2.Height = hauteur_pnl;
            pnl_lignes_par3.Height = hauteur_pnl;
            pnl_lignes_par4.Height = hauteur_pnl;
            pnl_lignes_par5.Height = hauteur_pnl;
            //Ajustement de la hauteur des panneaux Longeur de section
            pnl_long_section1.Height = hauteur_pnl;
            pnl_long_section2.Height = hauteur_pnl;
            pnl_long_section3.Height = hauteur_pnl;
            pnl_long_section4.Height = hauteur_pnl;
            pnl_long_section5.Height = hauteur_pnl;
            //Ajustement de la hauteur des panneaux Static Head
            pnl_static_head1.Height = hauteur_pnl;
            pnl_static_head2.Height = hauteur_pnl;
            pnl_static_head3.Height = hauteur_pnl;
            pnl_static_head4.Height = hauteur_pnl;
            pnl_static_head5.Height = hauteur_pnl;

            //

            //Ajustement hauteur du panneau System
            pnl_system.Height = (hauteur_pnl * 6) + (BORDURE_PNL * 10) + pnl_Tableur.Height + pnl_increment.Height;

            //Ajustemnt de la hauteur du tab page
            //tab_Control.Height = (hauteur_pnl * 6) + (BORDURE_PNL * 10) + pnl_Tableur.Height + pnl_increment.Height;
            tab_Control.Height = pnl_SystemInput.Height - (BORDURE_PNL * 4);

            #endregion //Hauteur des panneaux

            //Hauteur du graphique
            graphique_1.Height = pnl_SystemInput.Height - (BORDURE_PNL * 11);

            #endregion //Region hauteur des composants

            //Region contenant les largeurs des composants dans Form
            #region Largeur Composants Graphiques

            #region Largeur Panneaux
            /*Obtention de la largeur des panneaux  -  Le cinquième de l'écran 
              moins une constante d'espacement.*/
            int largeur_pnl = (dimensions[LARGEUR] / PROPORTION_DIX)
                                - ESPACE_PNL_MATERIEL;
            //Ajustement de la largeur des panneaux Materiel
            pnl_Materiel1.Width = largeur_pnl;
            pnl_Materiel2.Width = largeur_pnl;
            pnl_Materiel3.Width = largeur_pnl;
            pnl_Materiel4.Width = largeur_pnl;
            pnl_Materiel5.Width = largeur_pnl;
            //Ajustement de la largeur des panneaux Type
            pnl_Type1.Width = largeur_pnl;
            pnl_Type2.Width = largeur_pnl;
            pnl_Type3.Width = largeur_pnl;
            pnl_Type4.Width = largeur_pnl;
            pnl_Type5.Width = largeur_pnl;
            //Ajustement de la largeur des panneaux Titre
            pnl_Titre1.Width = largeur_pnl;
            pnl_Titre2.Width = largeur_pnl;
            pnl_Titre3.Width = largeur_pnl;
            pnl_Titre4.Width = largeur_pnl;
            pnl_Titre5.Width = largeur_pnl;
            //Ajustement de la largeur des panneaux Lignes Paralleles
            pnl_lignes_par1.Width = largeur_pnl;
            pnl_lignes_par2.Width = largeur_pnl;
            pnl_lignes_par3.Width = largeur_pnl;
            pnl_lignes_par4.Width = largeur_pnl;
            pnl_lignes_par5.Width = largeur_pnl;
            //Ajustement de la largeur des panneaux Longeur de section
            pnl_long_section1.Width = largeur_pnl;
            pnl_long_section2.Width = largeur_pnl;
            pnl_long_section3.Width = largeur_pnl;
            pnl_long_section4.Width = largeur_pnl;
            pnl_long_section5.Width = largeur_pnl;
            //Ajustement de la largeur des panneaux Static Head
            pnl_static_head1.Width = largeur_pnl;
            pnl_static_head2.Width = largeur_pnl;
            pnl_static_head3.Width = largeur_pnl;
            pnl_static_head4.Width = largeur_pnl;
            pnl_static_head5.Width = largeur_pnl;

            //Ajustement de la largeur du panneau system
            pnl_system.Width = (largeur_pnl * 5) + (BORDURE_PNL * 6);

            //Ajustment de la largeur du tab_page
            tab_Control.Width = (largeur_pnl * 5) + (BORDURE_PNL * 6);

            #endregion //Largeur Panneau

            #region Largeur cBox et UpDown
            /*Tout les inputs on la même largeur, calculé selon les dimensions 
             * de l'écran*/
            int largeur_num_input = largeur_pnl - (BORDURE_PNL * 2);

            cBox_materiel1.Width = largeur_num_input;
            cBox_materiel2.Width = largeur_num_input;
            cBox_materiel3.Width = largeur_num_input;
            cBox_materiel4.Width = largeur_num_input;
            cBox_materiel5.Width = largeur_num_input;

            cBox_Type1.Width = largeur_num_input;
            cBox_Type2.Width = largeur_num_input;
            cBox_Type3.Width = largeur_num_input;
            cBox_Type4.Width = largeur_num_input;
            cBox_Type5.Width = largeur_num_input;

            nUD_LignesParr1.Width = largeur_num_input;
            nUD_LignesParr2.Width = largeur_num_input;
            nUD_LignesParr3.Width = largeur_num_input;
            nUD_LignesParr4.Width = largeur_num_input;
            nUD_LignesParr5.Width = largeur_num_input;

            nUD_Long1.Width = largeur_num_input;
            nUD_Long2.Width = largeur_num_input;
            nUD_Long3.Width = largeur_num_input;
            nUD_Long4.Width = largeur_num_input;
            nUD_Long5.Width = largeur_num_input;

            nUD_Static1.Width = largeur_num_input;
            nUD_Static2.Width = largeur_num_input;
            nUD_Static3.Width = largeur_num_input;
            nUD_Static4.Width = largeur_num_input;
            nUD_Static5.Width = largeur_num_input;

            #endregion //Largeur Composants

            //Largeur du graphique
            graphique_1.Width = dimensions[LARGEUR] - pnl_system.Width - (BORDURE_PNL * 5);

            //Localisation des composants dans Form
            #region Positionnement

            #region Positionnement des panneaux
            /*Variables calculées pour la position des panneaux en X*/
            int x_pnl_1 = BORDURE_PNL;
            int x_pnl_2 = (largeur_pnl) + BORDURE_PNL * 2;
            int x_pnl_3 = (largeur_pnl * 2) + BORDURE_PNL * 3;
            int x_pnl_4 = (largeur_pnl * 3) + BORDURE_PNL * 4;
            int x_pnl_5 = (largeur_pnl * 4) + BORDURE_PNL * 5;

            //===Ajustemnt de location des panneaux Titre=======Premiere rangee
            int y_pnl_titre = BORDURE_PNL;
            //Panneau 1
            pnl_Titre1.Location = new Point(x_pnl_1, y_pnl_titre);
            //Panneau 2
            pnl_Titre2.Location = new Point(x_pnl_2, y_pnl_titre);
            //Panneau 3
            pnl_Titre3.Location = new Point(x_pnl_3, y_pnl_titre);
            //Panneau 4
            pnl_Titre4.Location = new Point(x_pnl_4, y_pnl_titre);
            //Panneau 5
            pnl_Titre5.Location = new Point(x_pnl_5, y_pnl_titre);

            //===Ajustement de location des panneaux Materiel===Deuxieme Rangee
            //Variable calculée pour la location en Y des panneaux
            int y_pnl_materiel = (BORDURE_PNL * 2) + hauteur_pnl;
            //Panneau 1
            pnl_Materiel1.Location = new Point(x_pnl_1, y_pnl_materiel);
            //Panneau 2
            pnl_Materiel2.Location = new Point(x_pnl_2, y_pnl_materiel);
            //Panneau 3
            pnl_Materiel3.Location = new Point(x_pnl_3, y_pnl_materiel);
            //Panneau 4
            pnl_Materiel4.Location = new Point(x_pnl_4, y_pnl_materiel);
            //Panneau 5
            pnl_Materiel5.Location = new Point(x_pnl_5, y_pnl_materiel);

            //===Ajustemnt de location des panneaux Type=======Troisieme Rangee
            //Variable calculée pour la location en Y des panneaux
            int y_pnl_type = (BORDURE_PNL * 3) + (hauteur_pnl * 2);
            //Panneau 1
            pnl_Type1.Location = new Point(x_pnl_1, y_pnl_type);
            //Panneau 2
            pnl_Type2.Location = new Point(x_pnl_2, y_pnl_type);
            //Panneau 3
            pnl_Type3.Location = new Point(x_pnl_3, y_pnl_type);
            //Panneau 4
            pnl_Type4.Location = new Point(x_pnl_4, y_pnl_type);
            //Panneau 5
            pnl_Type5.Location = new Point(x_pnl_5, y_pnl_type);

            //===Ajustement de location des panneaux Lignes Paralleles===
            //Quatrieme Rangee
            //Variable calculée pour la location en Y des panneaux
            int y_pnl_lignes = (BORDURE_PNL * 4) + (hauteur_pnl * 3);
            //Panneau 1
            pnl_lignes_par1.Location = new Point(x_pnl_1, y_pnl_lignes);
            //Panneau 2
            pnl_lignes_par2.Location = new Point(x_pnl_2, y_pnl_lignes);
            //Panneau 3
            pnl_lignes_par3.Location = new Point(x_pnl_3, y_pnl_lignes);
            //Panneau 4
            pnl_lignes_par4.Location = new Point(x_pnl_4, y_pnl_lignes);
            //Panneau 5
            pnl_lignes_par5.Location = new Point(x_pnl_5, y_pnl_lignes);

            //===Ajustemnt de location des panneaux Longueur Section======
            //Cinquieme Rangee
            //Variable calculée pour la location en Y des panneaux
            int y_pnl_long = (BORDURE_PNL * 5) + (hauteur_pnl * 4);
            //Panneau 1location
            pnl_long_section1.Location = new Point(x_pnl_1, y_pnl_long);
            //Panneau 2
            pnl_long_section2.Location = new Point(x_pnl_2, y_pnl_long);
            //Panneau 3
            pnl_long_section3.Location = new Point(x_pnl_3, y_pnl_long);
            //Panneau 4
            pnl_long_section4.Location = new Point(x_pnl_4, y_pnl_long);
            //Panneau 5
            pnl_long_section5.Location = new Point(x_pnl_5, y_pnl_long);

            //===Ajustemnt de location des panneaux Static Head==Sixieme Rangee
            //Variable calculée pour la location en Y des panneaux
            int y_pnl_static = (BORDURE_PNL * 6) + (hauteur_pnl * 5);
            //Panneau 1
            pnl_static_head1.Location = new Point(x_pnl_1, y_pnl_static);
            //Panneau 2
            pnl_static_head2.Location = new Point(x_pnl_2, y_pnl_static);
            //Panneau 3
            pnl_static_head3.Location = new Point(x_pnl_3, y_pnl_static);
            //Panneau 4
            pnl_static_head4.Location = new Point(x_pnl_4, y_pnl_static);
            //Panneau 5
            pnl_static_head5.Location = new Point(x_pnl_5, y_pnl_static);

            //Ajustement location du panneau pnl_increment
            pnl_increment.Location = new Point((pnl_system.Width / 2) - pnl_increment.Width / 2, (hauteur_pnl * 6) + (BORDURE_PNL * 7));

            pnl_Tableur.Location = new Point(BORDURE_PNL, (hauteur_pnl * 6) + (BORDURE_PNL * 8) + pnl_increment.Height);

            #endregion //Position Panneaux

            #region Positionnement des num Inputs
            int y_num_In = hauteur_pnl - cBox_materiel1.Height - BORDURE_PNL;

            cBox_materiel1.Location = new Point(BORDURE_PNL, y_num_In);
            cBox_materiel2.Location = new Point(BORDURE_PNL, y_num_In);
            cBox_materiel3.Location = new Point(BORDURE_PNL, y_num_In);
            cBox_materiel4.Location = new Point(BORDURE_PNL, y_num_In);
            cBox_materiel5.Location = new Point(BORDURE_PNL, y_num_In);

            cBox_Type1.Location = new Point(BORDURE_PNL, y_num_In);
            cBox_Type2.Location = new Point(BORDURE_PNL, y_num_In);
            cBox_Type3.Location = new Point(BORDURE_PNL, y_num_In);
            cBox_Type4.Location = new Point(BORDURE_PNL, y_num_In);
            cBox_Type5.Location = new Point(BORDURE_PNL, y_num_In);

            nUD_LignesParr1.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_LignesParr2.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_LignesParr3.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_LignesParr4.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_LignesParr5.Location = new Point(BORDURE_PNL, y_num_In);

            nUD_Long1.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_Long2.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_Long3.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_Long4.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_Long5.Location = new Point(BORDURE_PNL, y_num_In);

            nUD_Static1.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_Static2.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_Static3.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_Static4.Location = new Point(BORDURE_PNL, y_num_In);
            nUD_Static5.Location = new Point(BORDURE_PNL, y_num_In);
            #endregion //Position des num Inputs

            //Positionnement du graphique
            graphique_1.Location = new Point(pnl_system.Width + (BORDURE_PNL * 2), BORDURE_PNL);

            #endregion //Positionnement Graphique

            //==============Ajustement texte=============
            int demi_pnl = largeur_pnl / 2;
            int centre_pnl_mat = demi_pnl - (lbl_Materiel1.Width / 2);
            int y_texte_pnl = BORDURE_PNL / 3;
            //Texte pnl materiel
            lbl_Materiel1.Location = new Point(centre_pnl_mat, y_texte_pnl);
            lbl_Materiel2.Location = new Point(centre_pnl_mat, y_texte_pnl);
            lbl_Materiel3.Location = new Point(centre_pnl_mat, y_texte_pnl);
            lbl_Materiel4.Location = new Point(centre_pnl_mat, y_texte_pnl);
            lbl_Materiel5.Location = new Point(centre_pnl_mat, y_texte_pnl);
            //Texte pnl type
            int centre_pnl_type = demi_pnl - (lbl_PipeType1.Width / 2);
            lbl_PipeType1.Location = new Point(centre_pnl_type, y_texte_pnl);
            lbl_PipeType2.Location = new Point(centre_pnl_type, y_texte_pnl);
            lbl_PipeType3.Location = new Point(centre_pnl_type, y_texte_pnl);
            lbl_PipeType4.Location = new Point(centre_pnl_type, y_texte_pnl);
            lbl_PipeType5.Location = new Point(centre_pnl_type, y_texte_pnl);
            //Texte pnl lignes paralleles
            int centre_pnl_par = demi_pnl - (lbl_lignes_par1.Width / 2);
            lbl_lignes_par1.Location = new Point(centre_pnl_par, y_texte_pnl);
            lbl_lignes_par2.Location = new Point(centre_pnl_par, y_texte_pnl);
            lbl_lignes_par3.Location = new Point(centre_pnl_par, y_texte_pnl);
            lbl_lignes_par4.Location = new Point(centre_pnl_par, y_texte_pnl);
            lbl_lignes_par5.Location = new Point(centre_pnl_par, y_texte_pnl);
            //Texte pnl longueur section
            int centre_pnl_long = demi_pnl - (lbl_long_sect1.Width / 2);
            lbl_long_sect1.Location = new Point(centre_pnl_long, y_texte_pnl);
            lbl_long_sect2.Location = new Point(centre_pnl_long, y_texte_pnl);
            lbl_long_sect3.Location = new Point(centre_pnl_long, y_texte_pnl);
            lbl_long_sect4.Location = new Point(centre_pnl_long, y_texte_pnl);
            lbl_long_sect5.Location = new Point(centre_pnl_long, y_texte_pnl);
            //Texte pnl tête statique
            int centre_pnl_head = demi_pnl - (lbl_static_head1.Width / 2);
            lbl_static_head1.Location = new Point(centre_pnl_head, y_texte_pnl);
            lbl_static_head2.Location = new Point(centre_pnl_head, y_texte_pnl);
            lbl_static_head3.Location = new Point(centre_pnl_head, y_texte_pnl);
            lbl_static_head4.Location = new Point(centre_pnl_head, y_texte_pnl);
            lbl_static_head5.Location = new Point(centre_pnl_head, y_texte_pnl);
            //Texte de section
            int centre_pnl_section = demi_pnl - (lbl_Section1.Width / 2);
            lbl_Section1.Location = new Point(centre_pnl_section, y_texte_pnl);
            lbl_Section2.Location = new Point(centre_pnl_section, y_texte_pnl);
            lbl_Section3.Location = new Point(centre_pnl_section, y_texte_pnl);
            lbl_Section4.Location = new Point(centre_pnl_section, y_texte_pnl);
            lbl_Section5.Location = new Point(centre_pnl_section, y_texte_pnl);


            //===Ajustement du panneau, parent des panneaux materiel et type===
            pnl_SystemInput.Height = dimensions[HAUTEUR] - pnl_Bas.Height - 40;
            pnl_SystemInput.Width = dimensions[LARGEUR];
            pnl_SystemInput.Location = new Point(0, 0);
        }
        #endregion //Région AJUSTEMENT


        #endregion //Ajustement

        /*****************************************************************************/
        //Région contenant les fonctions d'évènement CLICK
        #region CLICK
        //Click du bouton hydraulique sur la page principale
        private void btn_selection_hydraulique_Click(object sender,
                                                    EventArgs e)
        {
            pnl_Section_Hydraulique.BringToFront();
        }
        //Click du bouton acceuil sur la page hydraulique
        private void btn_Acceuil_Click(object sender, EventArgs e)
        {
            pnl_Page_Principale.BringToFront();
        }


        private void btn_Calcul_Click(object sender, EventArgs e)
        {
            update_resultats_et_graphique();
        }

        private void r_Btn_RAZ_Click(object sender, EventArgs e)
        {
            r_Btn_Comp.Checked = false;
        }

        private void r_Btn_Comp_Click(object sender, EventArgs e)
        {
            r_Btn_RAZ.Checked = false;
        }

        private void r_Btn_EchelleAuto_Click(object sender, EventArgs e)
        {
            r_Btn_Echelle_Man.Checked = false;            
            nUD_Echelle.Enabled = false;
            graphique_1.ChartAreas[0].RecalculateAxesScale();
        }

        private void r_Btn_Echelle_Man_Click(object sender, EventArgs e)
        {
            r_Btn_EchelleAuto.Checked = false;
            nUD_Echelle.Enabled = true;
            graphique_1.ChartAreas[0].AxisY.Maximum = (int)nUD_Echelle.Value;
        }


        #endregion //Région CLICK

        /*****************************************************************************/
        /*Région contenant les évènements de changement de valeur dans les 
        ComboBox*/
        #region cBox

        /*cBox Materiel - Appellent tous la fonction 
         * "mise_a_jour_tableau_section_materiel" avec le numero de SECTION 
         * approprié et le nouveau contenu sélectionné*/
        //Section 1
        private void cBox_materiel1_SelectedIndexChanged(object sender,
                                                        EventArgs e)
        {
            update_tab_section_materiel((string)cBox_materiel1.SelectedItem,
                                        SECTION_A);
            materiel_select[MIN_SELECT] = true;
            materiel_select[SECTION_A] = true;

            if(type_select[SECTION_A] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_A();
            }
        }
        //Section 2
        private void cBox_materiel2_SelectedIndexChanged(object sender,
                                                         EventArgs e)
        {
            update_tab_section_materiel((string)cBox_materiel2.SelectedItem,
                                        SECTION_B);
            materiel_select[MIN_SELECT] = true;
            materiel_select[SECTION_B] = true;

            if (type_select[SECTION_B] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_B();
            }
        }
        //Section 3
        private void cBox_materiel3_SelectedIndexChanged(object sender,
                                                         EventArgs e)
        {
            update_tab_section_materiel((string)cBox_materiel3.SelectedItem,
                                        SECTION_C);
            materiel_select[MIN_SELECT] = true;
            materiel_select[SECTION_C] = true;

            if (type_select[SECTION_C] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_C();
            }
        }
        //section 4
        private void cBox_materiel4_SelectedIndexChanged(object sender,
                                                         EventArgs e)
        {
            update_tab_section_materiel((string)cBox_materiel4.SelectedItem,
                                        SECTION_D);
            materiel_select[MIN_SELECT] = true;
            materiel_select[SECTION_D] = true;

            if (type_select[SECTION_D] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_D();
            }
        }
        //Section 5
        private void cBox_materiel5_SelectedIndexChanged(object sender,
                                                         EventArgs e)
        {
            update_tab_section_materiel((string)cBox_materiel5.SelectedItem,
                                        SECTION_E);
            materiel_select[MIN_SELECT] = true;
            materiel_select[SECTION_E] = true;

            if (type_select[SECTION_E] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_E();
            }
        }

        /*cBox Materiel - Appellent tous la fonction 
         * "mise_a_jour_tableau_section_type" avec le numero de SECTION 
         * approprié et le nouveau contenu sélectionné*/
        //Section 1
        private void cBox_Type1_SelectedIndexChanged(object sender,
                                                     EventArgs e)
        {
            update_tab_section_type((string)cBox_Type1.SelectedItem,
                                    SECTION_A);
            type_select[MIN_SELECT] = true;
            type_select[SECTION_A] = true;

            if (type_select[SECTION_A] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_A();
            }
        }
        //Section 2
        private void cBox_Type2_SelectedIndexChanged(object sender,
                                                     EventArgs e)
        {
            update_tab_section_type((string)cBox_Type2.SelectedItem,
                                    SECTION_B);
            type_select[MIN_SELECT] = true;
            type_select[SECTION_B] = true;

            if (type_select[SECTION_B] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_B();
            }
        }
        //Section 3
        private void cBox_Type3_SelectedIndexChanged(object sender,
                                                     EventArgs e)
        {
            update_tab_section_type((string)cBox_Type3.SelectedItem,
                                    SECTION_C);
            type_select[MIN_SELECT] = true;
            type_select[SECTION_C] = true;

            if (type_select[SECTION_C] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_C();
            }
        }
        //Section 4
        private void cBox_Type4_SelectedIndexChanged(object sender,
                                                     EventArgs e)
        {
            update_tab_section_type((string)cBox_Type4.SelectedItem,
                                    SECTION_D);
            type_select[MIN_SELECT] = true;
            type_select[SECTION_D] = true;

            if (type_select[SECTION_E] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_D();
            }
        }
        //Section 5
        private void cBox_Type5_SelectedIndexChanged(object sender,
                                                     EventArgs e)
        {
            update_tab_section_type((string)cBox_Type5.SelectedItem,
                                    SECTION_E);
            type_select[MIN_SELECT] = true;
            type_select[SECTION_E] = true;

            if (type_select[SECTION_E] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_E();
            }
        }
        #endregion //Région cBOX

        /*****************************************************************************/
        //Région contenant les fonctions d'attibution de valeurs dans t_section
        #region Remplissage t_section

        //Fonction recevant un materiel en string provenant du cBox et un 
        //numero de section
        /*Le string est split pour avoir le materiel et la constante*/
        void update_tab_section_materiel(string materiel, int no_section)
        {
            //Tableau de variables tampons pour recevoir le contenu du cBox
            string[] item_materiel;

            //Séparation des deux items dans le combo box.
            //Format : MATERIEL - HAZENWILLIAMS
            //item[0] - MATERIEL      item[1] - HAZENWILLLIAMS
            item_materiel = materiel.Split('/');

            /*Les espaces ne sont là que pour la lisibilité de la sélection*/
            //Retrait de l'espace suivant le materiel seulement s'il y en a un.
            if (item_materiel[MATERIEL].EndsWith(" "))
            {
                //Met le string dans la case du type t_section(struct)
                section[no_section].materiel =          //Case struct
                item_materiel[MATERIEL].Remove          //Retrait
                (item_materiel[MATERIEL].Length - 1);   //de l'espace
            }

            /*Retrait de l'espace précédant la constant HazenWilliams seulement
              s'il y en a un.*/
            if (item_materiel[HAZENWILLIAMS].StartsWith(" "))
            {
                item_materiel[HAZENWILLIAMS] =                 //Retrait de 
                item_materiel[HAZENWILLIAMS].Remove(0, 1);     //de l'espace
                /*Met la constante converti en nombre entier dans la case du 
                 * type t_section(struct)*/
                section[no_section].constante_hazen_williams =
                    Int32.Parse(item_materiel[HAZENWILLIAMS]);
            }
        }

        //Fonction recevant un type en string provenant du cBox et un 
        //numero de section
        /*Le string est split pour avoir le materiel et la constante*/
        void update_tab_section_type(string type, int no_section)
        {
            //Tableau de variables tampons pour recevoir le contenu du cBox
            string[] item_type;

            //Séparation des deux items dans le combo box.
            //Format : MATERIEL - HAZENWILLIAMS
            //item[0] - MATERIEL      item[1] - HAZENWILLLIAMS
            item_type = type.Split('/');

            /*Les espaces ne sont là que pour la lisibilité de la sélection*/
            //Retrait de l'espace suivant le materiel seulement s'il y en a un.
            if (item_type[PIPE_TYPE].EndsWith(" "))
            {
                //Met le string dans la case du type t_section(struct)
                section[no_section].pipe_type =
                  item_type[PIPE_TYPE].Remove(item_type[PIPE_TYPE].Length - 1);
            }

            /*Retrait de l'espace précédant la constant HazenWilliams seulement
              s'il y en a un.*/
            if (item_type[DIAMETRE].StartsWith(" "))
            {
                item_type[DIAMETRE] = item_type[DIAMETRE].Remove(0, 1);

                /*Met la constante converti en type double dans la case du type
                 * t_section(struct)*/
                section[no_section].diametre_interne =
                    Convert.ToDouble(item_type[DIAMETRE]);
            }
        }


        /*=Insertion du nombre de lignes parallèles dans la case du t_section*/
        //Section A
        private void nUD_LignesParr1_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_A].num_parallel_lines = (int)nUD_LignesParr1.Value;
        }
        //Section B
        private void nUD_LignesParr2_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_B].num_parallel_lines = (int)nUD_LignesParr2.Value;
        }
        //Section C
        private void nUD_LignesParr3_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_C].num_parallel_lines = (int)nUD_LignesParr3.Value;
        }
        //Section D
        private void nUD_LignesParr4_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_D].num_parallel_lines = (int)nUD_LignesParr4.Value;
        }
        //Section E
        private void nUD_LignesParr5_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_E].num_parallel_lines = (int)nUD_LignesParr5.Value;
        }

        /*===Insertion de la longueur de section dans la case d t_section====*/
        //Section A
        private void nUD_Long1_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_A].longueur_section = (int)nUD_Long1.Value;
            if (materiel_select[SECTION_A] && type_select[SECTION_A] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_A();
            }
        }
        //Section B
        private void nUD_Long2_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_B].longueur_section = (int)nUD_Long2.Value;
            if (materiel_select[SECTION_B] && type_select[SECTION_B] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_B();
            }
        }
        //Section C
        private void nUD_Long3_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_C].longueur_section = (int)nUD_Long3.Value;
            if (materiel_select[SECTION_C] && type_select[SECTION_C] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_C();
            }
        }
        //Section D
        private void nUD_Long4_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_D].longueur_section = (int)nUD_Long4.Value;
            if (materiel_select[SECTION_D] && type_select[SECTION_D] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_D();
            }
        }
        //Section E
        private void nUD_Long5_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_E].longueur_section = (int)nUD_Long5.Value;
            if (materiel_select[SECTION_E] && type_select[SECTION_E] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_E();
            }
        }

        /*===Insertion de la hauteur statique dans la case du t_section===*/
        //Section A
        private void nUD_Static1_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_A].static_head = (int)nUD_Static1.Value;
            if (materiel_select[SECTION_A] && type_select[SECTION_A] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_A();
            }
        }
        //Section B
        private void nUD_Static2_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_B].static_head = (int)nUD_Static2.Value;
            if (materiel_select[SECTION_B] && type_select[SECTION_B] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_B();
            }
        }
        //Section C
        private void nUD_Static3_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_C].static_head = (int)nUD_Static3.Value;
            if (materiel_select[SECTION_C] && type_select[SECTION_C] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_C();
            }
        }
        //SEction D
        private void nUD_Static4_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_D].static_head = (int)nUD_Static4.Value;
            if (materiel_select[SECTION_D] && type_select[SECTION_D] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_D();
            }
        }
        //Section E
        private void nUD_Static5_ValueChanged(object sender, EventArgs e)
        {
            section[SECTION_E].static_head = (int)nUD_Static5.Value;
            if (materiel_select[SECTION_E] && type_select[SECTION_E] && r_Btn_RAZ.Checked)
            {
                update_resultats_et_graphique();
                update_tableur_section_E();
            }
        }

        #endregion

        /*****************************************************************************/
        //Region contenant les fonctions
        #region Fonctions et Calculs

        void calcul_TDH_section(bool[] matiere_select, bool[] type_select)
        {
            if (matiere_select[0] && type_select[0])
            {
                //Variables d'incrémentation, 
                //1er dimension : Section, 2e dimension : Incrément de débit  
                int segment_i;
                int debit_j;

                //L'incrémenteur i représente les sections
                for (segment_i = SECTION_A; segment_i <= SECTION_E; segment_i++)
                {
                    double L = section[segment_i].longueur_section;
                    double d = section[segment_i].diametre_interne;
                    double coefficient = 100 / section[segment_i].constante_hazen_williams;

                    for (debit_j = 0; debit_j <= 10; debit_j++)
                    {
                        //Débit q = incrément * 100 : 
                        //Augmente de tranches de 100 pour 10 cases
                        int q = debit_j * 100;



                        if (materiel_select[segment_i] && type_select[segment_i])
                        {
                            //TDH = 0.002083 * L * (100/C)^1.85 * (gpm^1.85 / d^4.8655)
                            //L = Longueur de section en pieds
                            //C = Coefficient de friction
                            //gpm = US
                            //d = diamètre intérieur du tuyau en pouce
                            TDH[segment_i, debit_j] =
                                (CONSTANTE_HW * L * Math.Pow(coefficient, 1.85)
                                    * Math.Pow(q, 1.85) / Math.Pow(d, 4.8655))
                                        + section[segment_i].static_head;
                        }
                    }
                }

        }
            else
            {
                MessageBox.Show("Vous devez remplir un moins une section");
            }


}

        void calcul_TDH_total(bool[] matiere_select, bool[] type_select)
        {
            //Variables d'incrémentation, 
            //1er dimension : Section, 2e dimension : Incrément de débit  
            int segment_i;
            int debit_j;


            init_tab_1d(TDH_tot, 10);

            for (debit_j = 0; debit_j <= 10; debit_j++)
            {
                int q = debit_j * 100;


                for (segment_i = SECTION_A; segment_i <= SECTION_E; segment_i++)
                {
                    TDH_tot[debit_j] += TDH[segment_i, debit_j];
                }

                //graphique_1.Series(0).Points.AddXY(q, TDH_tot[debit_j]);

                graphique_1.Series[nombre_Serie].Points.AddXY(q, TDH_tot[debit_j]);
            }
        }
        //Initialisation ou remise à zéro d'un tableau 1 dimension
        void init_tab_1d(double[] tab, int longueur)
        {
            int i;

            for (i = 0; i <= longueur; i++)
            {
                tab[i] = 0;
            }
        }

        void init_tab_2d_double(double[,] tab_2d, int longueur_d1, int longueur_d2)
        {
            int i;
            int j;

            for (i = 0; i <= longueur_d1; i++)
            {
                for (j = 0; j <= longueur_d2; j++)
                {
                    tab_2d[i, j] = 8;
                }
            }
        }

        void init_tab_bool(bool[] tab_bool, int longueur)
        {
            int i;

            for (i = 0; i <= longueur; i++)
            {
                tab_bool[i] = false;
            }
        }

        void raz_serie(int nombre_Serie)
        {
            int i; 
            //Supprime les points de toutes les série du graphique
            for (i = 0; i <= nombre_Serie; i++)
            {
                graphique_1.Series[i].Points.Clear();
            }
        }

        private void update_resultats_et_graphique()
        {
            if (r_Btn_RAZ.Checked)
            {
                raz_serie(nombre_Serie);
                nombre_Serie = 0;
            }
            else if (r_Btn_Comp.Checked)
            {
                if (nombre_Serie >= MAX_SERIES)
                {
                    MessageBox.Show("Il y a trop de courbes dans le graphique");
                }
                else
                {
                    nombre_Serie++;
                }
            }
            calcul_TDH_section(materiel_select, type_select);
            calcul_TDH_total(materiel_select, type_select);

            //if (r_Btn_EchelleAuto.Checked)
            //{
            //    graphique_1.ChartAreas[0].RecalculateAxesScale();
            //}
        }


        #endregion //Fonctions et Calculs

        private void nUD_inc_long_Validating(object sender, CancelEventArgs e)
        {
            nUD_Long1.Increment = nUD_inc_long.Value;
            nUD_Long2.Increment = nUD_inc_long.Value;
            nUD_Long3.Increment = nUD_inc_long.Value;
            nUD_Long4.Increment = nUD_inc_long.Value;
            nUD_Long5.Increment = nUD_inc_long.Value;
        }
        private void nUD_inc_static_Validating(object sender, CancelEventArgs e)
        {
            nUD_Static1.Increment = nUD_inc_static.Value;
            nUD_Static2.Increment = nUD_inc_static.Value;
            nUD_Static3.Increment = nUD_inc_static.Value;
            nUD_Static4.Increment = nUD_inc_static.Value;
            nUD_Static5.Increment = nUD_inc_static.Value;
        }
        private void nUD_Long1_Validated(object sender, EventArgs e)
        {

        }

        void update_tableur_section_A()
        {

            lbl_A_1.Text = Math.Round(TDH[SECTION_A, 1], 1).ToString();
            lbl_A_2.Text = Math.Round(TDH[SECTION_A, 2], 1).ToString();
            lbl_A_3.Text = Math.Round(TDH[SECTION_A, 3], 1).ToString();
            lbl_A_4.Text = Math.Round(TDH[SECTION_A, 4], 1).ToString();
            lbl_A_5.Text = Math.Round(TDH[SECTION_A, 5], 1).ToString();
            lbl_A_6.Text = Math.Round(TDH[SECTION_A, 6], 1).ToString();
            lbl_A_7.Text = Math.Round(TDH[SECTION_A, 7], 1).ToString();
            lbl_A_8.Text = Math.Round(TDH[SECTION_A, 8], 1).ToString();
            lbl_A_9.Text = Math.Round(TDH[SECTION_A, 9], 1).ToString();
            lbl_A_10.Text = Math.Round(TDH[SECTION_A, 10], 1).ToString();
        }
        void update_tableur_section_B()
        {
            lbl_B_1.Text = Math.Round(TDH[SECTION_B, 1], 1).ToString();
            lbl_B_2.Text = Math.Round(TDH[SECTION_B, 2], 1).ToString();
            lbl_B_3.Text = Math.Round(TDH[SECTION_B, 3], 1).ToString();
            lbl_B_4.Text = Math.Round(TDH[SECTION_B, 4], 1).ToString();
            lbl_B_5.Text = Math.Round(TDH[SECTION_B, 5], 1).ToString();
            lbl_B_6.Text = Math.Round(TDH[SECTION_B, 6], 1).ToString();
            lbl_B_7.Text = Math.Round(TDH[SECTION_B, 7], 1).ToString();
            lbl_B_8.Text = Math.Round(TDH[SECTION_B, 8], 1).ToString();
            lbl_B_9.Text = Math.Round(TDH[SECTION_B, 9], 1).ToString();
            lbl_B_10.Text = Math.Round(TDH[SECTION_B, 10], 1).ToString();
        }
        void update_tableur_section_C()
        {
            lbl_C_1.Text = Math.Round(TDH[SECTION_C, 1], 1).ToString();
            lbl_C_2.Text = Math.Round(TDH[SECTION_C, 2], 1).ToString();
            lbl_C_3.Text = Math.Round(TDH[SECTION_C, 3], 1).ToString();
            lbl_C_4.Text = Math.Round(TDH[SECTION_C, 4], 1).ToString();
            lbl_C_5.Text = Math.Round(TDH[SECTION_C, 5], 1).ToString();
            lbl_C_6.Text = Math.Round(TDH[SECTION_C, 6], 1).ToString();
            lbl_C_7.Text = Math.Round(TDH[SECTION_C, 7], 1).ToString();
            lbl_C_8.Text = Math.Round(TDH[SECTION_C, 8], 1).ToString();
            lbl_C_9.Text = Math.Round(TDH[SECTION_C, 9], 1).ToString();
            lbl_C_10.Text = Math.Round(TDH[SECTION_C, 10], 1).ToString();
        }
        void update_tableur_section_D()
        {
            lbl_D_1.Text = TDH[SECTION_D, 1].ToString();
            lbl_D_2.Text = TDH[SECTION_D, 2].ToString();
            lbl_D_3.Text = TDH[SECTION_D, 3].ToString();
            lbl_D_4.Text = TDH[SECTION_D, 4].ToString();
            lbl_D_5.Text = TDH[SECTION_D, 5].ToString();
            lbl_D_6.Text = TDH[SECTION_D, 6].ToString();
            lbl_D_7.Text = TDH[SECTION_D, 7].ToString();
            lbl_D_8.Text = TDH[SECTION_D, 8].ToString();
            lbl_D_9.Text = TDH[SECTION_D, 9].ToString();
            lbl_D_10.Text = TDH[SECTION_D, 10].ToString();
        }
        void update_tableur_section_E()
        {
            lbl_E_1.Text = TDH[SECTION_E, 1].ToString().PadLeft(5,'0');
            lbl_E_2.Text = TDH[SECTION_E, 2].ToString();
            lbl_E_3.Text = TDH[SECTION_E, 3].ToString();
            lbl_E_4.Text = TDH[SECTION_E, 4].ToString();
            lbl_E_5.Text = TDH[SECTION_E, 5].ToString();
            lbl_E_6.Text = TDH[SECTION_E, 6].ToString();
            lbl_E_7.Text = TDH[SECTION_E, 7].ToString();
            lbl_E_8.Text = TDH[SECTION_E, 8].ToString();
            lbl_E_9.Text = TDH[SECTION_E, 9].ToString();
            lbl_E_10.Text = TDH[SECTION_E, 10].ToString();
        }

        private void nUD_Echelle_ValueChanged(object sender, EventArgs e)
        {
           // graphique_1.ChartAreas[0].AxisY.Maximum = (int)nUD_Echelle.Value;
        }


    }
}
