using PdfSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

//Using pour ouvrir et fermer correctement un fichier Excel,
//il faut aussi rajouter une reference dans :
//  /Projet /Ajouter une reference /Com /Excel...


namespace System_Curve__4._0
{
    public partial class Page_Principale : Form
    {
        /*==================Variables========================================*/
        #region Variables        

        //Tabluea 4 dimensions permettant de selectionner l'equation de la pompe voulue
        double[,,,] matrice_formule = new double[k.NB_MARQUES,
                                                    k.NB_SERIE,
                                                        k.NB_MODELE,
                                                            k.NB_PARAMETRES];

        //Map de pixel representant une image.  Utilisee pour la creation de PDF
        Bitmap bmp;

        //Tableau contenant le suivi des sections sélectionnées, la case 0 sert
        //pour l'initialisation (si aucun cBox, n'a été sélectionné, 
        //bool[0] == FALSE)
        bool[] materiel_select = new bool[6];
        bool[] type_select = new bool[6];
        bool[] sections_actives = new bool[6];

        /*============ Tableau statique de type t_section, pour insérer le 
         * contenu des cBox une fois converti au format =====================*/
        //Chaque case représente une section
        /*Chaques section contient le materiel, la constante HazenWilliam, le
           type de tuyau et le diamètre intérieur*/
        //Tableau pour les dimensions de l'ecran
        int[] dimensions = new int[2];
        //Tableau 2 dimensions pour contenir les valeur de TDH pour chaque 
        //section
        double[,] TDH = new double[6, 12];
        double[] TDH_tot = new double[12];

        //Bool de validation de donnees
        bool[] valide = new bool[12];
        //Tableau contenant les intervalles, donc les coordonnees en X pour la 
        //courbe de tuyauterie.
        double[] intervalles_debit = new double[12];

        //Bit interne pour connaitre l'etat de l'ecran
        bool fullscreen = false;

        //Date du jour, recueuillie a l'ouverture du logiciel
        string date;

        //Disposition des pompes - serie ou parallele
        int[] dispositon = new int[13];

        //Point de positionnement de la souris
        private Point MouseDownLocation;

        //Struct des inputs, modifies a chaque entrees d'utilisateur
        //Entrees d'utilisateur classees dans plusieurs tableaux
        t_inputs inputs;

        //Path du fichier en cours, dernier fichier ouvert ou enregistre
        string pathstring = null;

        //Fichier pour l'enregistrement des inputs
        Stream myStream = null;

        int nb_ecrans;
        Screen[] ecrans;

        string sfd_name;

        bool capture_ecran = false;


        #endregion  //Variables

        //=====================================================================
        public Page_Principale()
        {
            InitializeComponent();
        }

        //À l'ouverture de la page principale
        private void Page_Principale_Load(object sender, EventArgs e)
        {
            ecrans = Screen.AllScreens;

            nb_ecrans = ecrans.Length;

            //Remplissage du tableau de matrice des formules dans le modules Tendance
            Tendance.remplissage_formules(matrice_formule);

            //Ajustement des dimensions selon les dimensions de l'écran
            ajust.obtention_dimensions_ecran(dimensions);

            //Cache le programme a l'ouverture en affichant le logo technosub
            pnl_ouverture.BringToFront();

            //Declaration et definition de la taille des tableau pour le struct t_inputs
            inputs = declaration.declaration_inputs();
            //Initialisation du struct t_inputs
            init.demarrage_inputs(inputs);

            unite_en_USGPM();
            unite_en_fth20();
            unite_en_hp();
            unite_en_pieds();
            inputs.acceuil.langue = "francais";

            //La methode ouvre les pages du tab_control pour accelerer le changement de page
            //La premiere ouverture du la page est la plus longue.
            rafaichissement_tab_control();

            //Cache les panneaux de fittings
            pnl_Equiv_A.SendToBack();
            pnl_Equiv_B.SendToBack();
            pnl_Equiv_C.SendToBack();
            pnl_Equiv_D.SendToBack();
            pnl_Equiv_E.SendToBack();

            //Ajustement necessaire a l'interface
            ajustement_ecran();
            ajustement_pnl_materiel_type();
            ajustement_boutons();
            ajustment_pnl_equiv();
            ajustement_pnl_edition();
            ajustement_graphique_normalsize();
            ajustement_images();
            ajustement_lbl();

            //Positionnement par default du panneau client pour le graphique en fullscreen
            int x_pnl_client_graph = dimensions[k.LARGEUR]
                                        - pnl_client_graph.Width
                                            - (5 * k.BORDURE_PNL);
            int y_pnl_client_graph = (pnl_EnTete_Graph.Location.Y
                                        + (k.BORDURE_PNL * 5));
            pnl_client_graph.Location = new Point(x_pnl_client_graph, y_pnl_client_graph);

            //Positionnement du panneau contenant la legende automatique des    
            //composantes systeme affichee en graphique fullscreen
            lbl_Section.Location = new Point(
                                    dimensions[k.LARGEUR]           //X
                                        - lbl_Section.Width
                                            - (k.BORDURE_PNL * 3),
                                    pnl_client_graph.Location.Y     //Y
                                        + pnl_client_graph.Height
                                            + k.BORDURE_PNL);
            //Ajuste le graphique en demi ecran d'apres les dimensions obtenues
            ajustement_graphique_normalsize();
            //Initialisation des tableaux - Valeur a zero et false
            init.tab_2d_double(TDH, 6, 12);
            init.tab_bool_false(materiel_select, 6);
            init.tab_bool_false(type_select, 6);
            init.tab_bool_false(valide, 12);
            init.tab_1d_double(TDH_tot, 1);
            init.tab_1d_int(dispositon, 13);

            //Initialisation du t_section lignes paralleles a 1 pour eviter la
            //division et multiplication par zero                     
            init.ligne_parrallel_a_un(inputs.section);
            init.nb_pompes_a_un(inputs.pompes);
            init.nb_stages_a_un(inputs.pompes);
            init.ratio_a_un(inputs.pompes);
            //Initialisation par fonction de retour parce qu'il n'est pas un tableau
            inputs.acceuil = init.unite_a_un(inputs.acceuil);

            inputs.action.graphique_secondaire = false;

            //Remplissages des comboBox de marque selon le t_marques recueuillie
            remplissage_cBox_Marques(brands.nb);
            //Remplissage des combobox de type de tuyaux
            remplissage_cBox_Pipes(Pipes.nb);
            //Remplissage des combobox de materiel
            remplissage_cBox_Materiel(Materiel.nb);


            //Obtention de la date du jour
            date = fonction.obtenir_date();
            //Ecriture de cette date dans le textbox dans l'accueil
            tBox_client_Date.Text = date;

            //Mise en evidence de la page principale du logiciel
            pnl_SystemInput.BringToFront();

            nUD_Debit.Value = 1000;

            WindowState = FormWindowState.Maximized;

            graphique_1.ChartAreas[0].Position.Height = 82;
            graphique_1.ChartAreas[1].Visible = false;
            graphique_1.ChartAreas[2].Visible = false;


        }

        //A la fermeture du programme, le fichier Excel doit etre ferme et sauvegarde
        private void Page_Principale_FormClosing(object sender, FormClosingEventArgs e)
        {
            ////Offre de sauvegarde avant de quitter
            ////Fermeture directe si Non est selectionne et une sauvegarde est 
            ////executee si Oui est selectionne
            //if (MessageBox.Show("Voulez-vous sauvegarder avant de quitter?",
            //                        "SAUVEGARDE",
            //                            MessageBoxButtons.YesNo,
            //                                MessageBoxIcon.Question)
            //                                    == DialogResult.Yes)
            //{
            //    ecriture_sauvegarde_dossier();
            //}
        }


        /*********************************************************************/
        //Région contenant les fonctions d'ajustement, dimensions et locations
        #region Ajustement dimensions selon l'ecran
        //Ajustement de la fenêtre selon les dimensions de l'écran
        void ajustement_ecran()
        {
            //Ajustement de l'écran selon la résolution
            this.Width = dimensions[k.LARGEUR];
            this.Height = dimensions[k.HAUTEUR];
        }

        //Ajustement des boutons au bas de l'écran
        void ajustement_boutons()
        {
            /*Calcul des dimensions des boutons selon les dimensions de 
              l'écran*/
            int hauteur_bouton = dimensions[k.HAUTEUR] / k.PROPORTION_DIX;
            int largeur_bouton = dimensions[k.LARGEUR] / k.PROPORTION_CINQ;
            int demi_largeur = largeur_bouton / 2;

            /*Ajustement de sa position dans l'écran*/
            //Au quart de l'écran
            int pos_x_btn_gauche = (dimensions[k.LARGEUR] / k.PROPORTION_QUATRE) -
                                        demi_largeur;
            //À la moitié de l'écran
            int pos_x_btn_centre = (dimensions[k.LARGEUR] / k.PROPORTION_DEUX) -
                                        demi_largeur;
            //Au trois quart de l'écran
            int pos_x_btn_droite = ((dimensions[k.LARGEUR] * k.PROPORTION_TROIS) /
                                        k.PROPORTION_QUATRE) - demi_largeur;

            /*Ajustment des boutons ouvrant la modification des fittings*/
            //Largeur des boutons selon la largeur du pnl parent
            int largeur_btn_fitting = pnl_affich_Equiv_A.Width - (k.BORDURE_PNL * 3);
            btn_Fitting_A.Width = largeur_btn_fitting;
            btn_Fitting_B.Width = largeur_btn_fitting;
            btn_Fitting_C.Width = largeur_btn_fitting;
            btn_Fitting_D.Width = largeur_btn_fitting;
            btn_Fitting_E.Width = largeur_btn_fitting;
            //Hauteur des boutons selon la hauter du pnl pareny
            int hauteur_btn_fitting = (pnl_affich_Equiv_A.Height
                                        / (k.BORDURE_PNL / 3))
                                            - k.BORDURE_PNL;
            btn_Fitting_A.Height = hauteur_btn_fitting;
            btn_Fitting_B.Height = hauteur_btn_fitting;
            btn_Fitting_C.Height = hauteur_btn_fitting;
            btn_Fitting_D.Height = hauteur_btn_fitting;
            btn_Fitting_E.Height = hauteur_btn_fitting;
            //Position des boutons fitting dans le pnl parent
            int x_btn_Fitting = (pnl_affich_Equiv_A.Width - btn_Fitting_A.Width) / 2;
            int y_btn_Fitting = k.BORDURE_PNL / 3;
            btn_Fitting_A.Location = new Point(x_btn_Fitting, y_btn_Fitting);
            btn_Fitting_B.Location = new Point(x_btn_Fitting, y_btn_Fitting);
            btn_Fitting_C.Location = new Point(x_btn_Fitting, y_btn_Fitting);
            btn_Fitting_D.Location = new Point(x_btn_Fitting, y_btn_Fitting);
            btn_Fitting_E.Location = new Point(x_btn_Fitting, y_btn_Fitting);
        }

        /*Ajustement des lbl affichant la longueur equivalente des fittings*/
        void ajustement_lbl()
        {
            //Position en X selon la largeur du pnl
            int x_lbl_Fitting = (pnl_affich_Equiv_A.Width - lbl_Fitting_A.Width) / 2;
            //Position en Y selon la hauteur du pnl
            int y_lbl_Fitting = btn_Fitting_A.Height + k.BORDURE_PNL;
            //Ajustement
            lbl_Fitting_A.Location = new Point(x_lbl_Fitting, y_lbl_Fitting);
            lbl_Fitting_B.Location = new Point(x_lbl_Fitting, y_lbl_Fitting);
            lbl_Fitting_C.Location = new Point(x_lbl_Fitting, y_lbl_Fitting);
            lbl_Fitting_D.Location = new Point(x_lbl_Fitting, y_lbl_Fitting);
            lbl_Fitting_E.Location = new Point(x_lbl_Fitting, y_lbl_Fitting);
            //Determine la position des label des unites
            int x_lbl_unit = nUD_Long1.Location.X + nUD_Long1.Size.Width + k.BORDURE_PNL;
            int y_lbl_unit = nUD_Long1.Location.Y;

            lbl_UnitLong1.Location = new Point(x_lbl_unit, y_lbl_unit);
            lbl_UnitLong2.Location = new Point(x_lbl_unit, y_lbl_unit);
            lbl_UnitLong3.Location = new Point(x_lbl_unit, y_lbl_unit);
            lbl_UnitLong4.Location = new Point(x_lbl_unit, y_lbl_unit);
            lbl_UnitLong5.Location = new Point(x_lbl_unit, y_lbl_unit);

            lbl_UnitStatic1.Location = new Point(x_lbl_unit, y_lbl_unit);
            lbl_UnitStatic2.Location = new Point(x_lbl_unit, y_lbl_unit);
            lbl_UnitStatic3.Location = new Point(x_lbl_unit, y_lbl_unit);
            lbl_UnitStatic4.Location = new Point(x_lbl_unit, y_lbl_unit);
            lbl_UnitStatic5.Location = new Point(x_lbl_unit, y_lbl_unit);
        }

        //Ajustement du tableur
        void ajustement_tableur()
        {
            //Largeur écran divisé par 2.5    int...=3
            int largeur_tableur = (dimensions[k.LARGEUR] * k.PROPORTION_DEUX)
                                    / k.PROPORTION_CINQ;
            //int hauteur_tableur = (dimensions[k.HAUTEUR] * k.PROPORTION_QUATORZE)
            //                        / k.PROPORTION_CINQ
            //                            - k.BORDURE_PNL;
            int hauteur_tableur = (dimensions[k.HAUTEUR] / 5)
                            - k.BORDURE_PNL;
            //Hauteur et largeur des paneaux du tableur
            pnl_Tableur.Height = hauteur_tableur;
            pnl_Tableur.Width = largeur_tableur;
            //Determine la largeur des case du tableur
            int largeur_case = (largeur_tableur - k.PROPORTION_QUATORZE)
                                    / k.PROPORTION_SIX;
            pnl_t_1.Width = largeur_case;
            pnl_t_2.Width = largeur_case;
            pnl_t_3.Width = largeur_case;
            pnl_t_4.Width = largeur_case;
            pnl_t_5.Width = largeur_case;
            pnl_t_6.Width = largeur_case;

            //Hauteur de valeur de 36
            int hauteur_case_t = k.BORDURE_PNL * k.BORDURE_PNL;
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
            //Obtention de la hauteur des panneaux
            int hauteur_pnl = dimensions[k.HAUTEUR] / k.PROPORTION_DIXSEPT;
            //AJustement de la hauteur des panneaux Materiel
            pnl_Materiel1.Height = hauteur_pnl * k.PROPORTION_DEUX;
            pnl_Materiel2.Height = hauteur_pnl * k.PROPORTION_DEUX;
            pnl_Materiel3.Height = hauteur_pnl * k.PROPORTION_DEUX;
            pnl_Materiel4.Height = hauteur_pnl * k.PROPORTION_DEUX;
            pnl_Materiel5.Height = hauteur_pnl * k.PROPORTION_DEUX;
            //Ajustement de la hauteur des panneaux Type
            pnl_Type1.Height = hauteur_pnl * k.PROPORTION_DEUX;
            pnl_Type2.Height = hauteur_pnl * k.PROPORTION_DEUX;
            pnl_Type3.Height = hauteur_pnl * k.PROPORTION_DEUX;
            pnl_Type4.Height = hauteur_pnl * k.PROPORTION_DEUX;
            pnl_Type5.Height = hauteur_pnl * k.PROPORTION_DEUX;
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
            //Ajustemnt de la hauteru des panneaux affichage longueur equivalente
            pnl_affich_Equiv_A.Height = hauteur_pnl;
            pnl_affich_Equiv_B.Height = hauteur_pnl;
            pnl_affich_Equiv_C.Height = hauteur_pnl;
            pnl_affich_Equiv_D.Height = hauteur_pnl;
            pnl_affich_Equiv_E.Height = hauteur_pnl;
            //Ajustement de la hauteur des panneaux facteur de securite
            pnl_safety_factor1.Height = hauteur_pnl;
            pnl_safety_factor2.Height = hauteur_pnl;
            pnl_safety_factor3.Height = hauteur_pnl;
            pnl_safety_factor4.Height = hauteur_pnl;
            pnl_safety_factor5.Height = hauteur_pnl;
            //Panneau contenant la grandeur "maximale" avant d'elargir par une barre defilement
            pnl_Scroll_Tuyauterie.Height =
                pnl_Titre1.Height
                    + pnl_Materiel1.Height
                        + pnl_Type1.Height
                            + pnl_lignes_par1.Height
                                + pnl_long_section1.Height
                                    + pnl_static_head1.Height
                                        + pnl_affich_Equiv_A.Height
                                            + pnl_safety_factor1.Height
                                                + (k.BORDURE_PNL * 20);
            //Hauteur du tab_control
            tab_Control.Height = this.Height - (k.BORDURE_PNL * 10);

            //Hauteur du panneau system selon les dimensions du tab control
            pnl_system.Height = tab_Control.Bounds.Height;

            /*Obtention de la largeur des panneaux  -  Le cinquième de l'écran 
              moins une constante d'espacement.*/
            int largeur_pnl = (dimensions[k.LARGEUR] / k.PROPORTION_SEPT)
                                - k.ESPACE_PNL_MATERIEL;
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
            //Ajustement de la largeur des panneaux affichage longueur equivalente
            pnl_affich_Equiv_A.Width = largeur_pnl;
            pnl_affich_Equiv_B.Width = largeur_pnl;
            pnl_affich_Equiv_C.Width = largeur_pnl;
            pnl_affich_Equiv_D.Width = largeur_pnl;
            pnl_affich_Equiv_E.Width = largeur_pnl;

            //Ajustement de la largeur des panneaux facteur de securite
            pnl_safety_factor1.Width = largeur_pnl;
            pnl_safety_factor2.Width = largeur_pnl;
            pnl_safety_factor3.Width = largeur_pnl;
            pnl_safety_factor4.Width = largeur_pnl;
            pnl_safety_factor5.Width = largeur_pnl;

            //Ajustment de la largeur du tab_page
            tab_Control.Width = pnl_pompe_1.Width
                                    + (k.BORDURE_PNL * k.BORDURE_PNL);

            //Ajustement de la largeur du panneau system
            //pnl_system.Width = (largeur_pnl * 5) + (k.BORDURE_PNL * 6);
            pnl_system.Width = tab_Control.Width - 10;

            pnl_Param_Curseur.Width = tab_Control.Width - (k.BORDURE_PNL * 4);
            pnl_Param_Curseur.Location = new Point(k.BORDURE_PNL, 420);

            lbl_Titre_Curseur.Width = pnl_Param_Curseur.Width - (k.BORDURE_PNL * 2);
            lbl_Titre_Curseur.Location = new Point(k.BORDURE_PNL, k.BORDURE_PNL);

            int largeur_pnl_Param_Curseur = (pnl_Param_Curseur.Width - (k.BORDURE_PNL * 10)) / 4;

            pnl_Param_Curseur1.Width = largeur_pnl_Param_Curseur;
            pnl_Param_Curseur2.Width = largeur_pnl_Param_Curseur;
            pnl_Param_Curseur3.Width = largeur_pnl_Param_Curseur;
            pnl_Param_Curseur4.Width = largeur_pnl_Param_Curseur;

            pnl_Param_Curseur1.Location = new Point(k.BORDURE_PNL * 2, 51);
            pnl_Param_Curseur2.Location = new Point((k.BORDURE_PNL * 4) + (largeur_pnl_Param_Curseur), 51);
            pnl_Param_Curseur3.Location = new Point((k.BORDURE_PNL * 6) + (largeur_pnl_Param_Curseur * 2), 51);
            pnl_Param_Curseur4.Location = new Point((k.BORDURE_PNL * 8) + (largeur_pnl_Param_Curseur * 3), 51);

            /*Tout les inputs on la même largeur, calculé selon les dimensions 
             * de l'écran*/
            int largeur_num_input = largeur_pnl - (k.BORDURE_PNL);
            //Largeur combobox selectionant le materiel du tuyau
            cBox_materiel1.Width = largeur_num_input;
            cBox_materiel2.Width = largeur_num_input;
            cBox_materiel3.Width = largeur_num_input;
            cBox_materiel4.Width = largeur_num_input;
            cBox_materiel5.Width = largeur_num_input;
            //Largeur combobox selectionant le type de tuyau
            cBox_Type1.Width = largeur_num_input;
            cBox_Type2.Width = largeur_num_input;
            cBox_Type3.Width = largeur_num_input;
            cBox_Type4.Width = largeur_num_input;
            cBox_Type5.Width = largeur_num_input;
            //Largeur numeric up/down selectionnant le nombre de lignes paralleles
            nUD_LignesParr1.Width = largeur_num_input;
            nUD_LignesParr2.Width = largeur_num_input;
            nUD_LignesParr3.Width = largeur_num_input;
            nUD_LignesParr4.Width = largeur_num_input;
            nUD_LignesParr5.Width = largeur_num_input;
            //Largeur numeric up/down selectionant 
            nUD_Long1.Width = largeur_num_input - k.LARGEUR_UNITES;
            nUD_Long2.Width = largeur_num_input - k.LARGEUR_UNITES;
            nUD_Long3.Width = largeur_num_input - k.LARGEUR_UNITES;
            nUD_Long4.Width = largeur_num_input - k.LARGEUR_UNITES;
            nUD_Long5.Width = largeur_num_input - k.LARGEUR_UNITES;
            //Largeur numeric up/down selectionant la tete statique
            nUD_Static1.Width = largeur_num_input - k.LARGEUR_UNITES;
            nUD_Static2.Width = largeur_num_input - k.LARGEUR_UNITES;
            nUD_Static3.Width = largeur_num_input - k.LARGEUR_UNITES;
            nUD_Static4.Width = largeur_num_input - k.LARGEUR_UNITES;
            nUD_Static5.Width = largeur_num_input - k.LARGEUR_UNITES;
            //Largeur des numeric up/down selectionnant le facteur de securite
            nUD_Safety_Factor1.Width = largeur_num_input;
            nUD_Safety_Factor2.Width = largeur_num_input;
            nUD_Safety_Factor3.Width = largeur_num_input;
            nUD_Safety_Factor4.Width = largeur_num_input;
            nUD_Safety_Factor5.Width = largeur_num_input;
            //Localisation des composants dans Form

            /*Variables calculées pour la position des panneaux en X*/
            int x_pnl_1 = k.BORDURE_PNL;
            int x_pnl_2 = (largeur_pnl * k.COLONNE_A) + k.BORDURE_PNL * 4;
            int x_pnl_3 = (largeur_pnl * k.COLONNE_B) + k.BORDURE_PNL * 7;
            int x_pnl_4 = (largeur_pnl * k.COLONNE_C) + k.BORDURE_PNL * 10;
            int x_pnl_5 = (largeur_pnl * k.COLONNE_D) + k.BORDURE_PNL * 13;

            //===Ajustemnt de location des panneaux Titre=======Premiere rangee
            int y_pnl_titre = k.BORDURE_PNL + (k.BORDURE_PNL);
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
            int y_pnl_materiel = (k.BORDURE_PNL * 4) + hauteur_pnl;
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
            int y_pnl_type = (k.BORDURE_PNL * 6) + (hauteur_pnl * 3);
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
            int y_pnl_lignes = (k.BORDURE_PNL * 8) + (hauteur_pnl * 5);
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
            int y_pnl_long = (k.BORDURE_PNL * 10) + (hauteur_pnl * 6);
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
            int y_pnl_static = (k.BORDURE_PNL * 12) + (hauteur_pnl * 7);
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

            //===Ajustemnt de location des panneaux affich euqiv==Septieme Rangee
            //Variable calculée pour la location en Y des panneaux
            int y_pnl_affic_equiv = (k.BORDURE_PNL * 14) + (hauteur_pnl * 8);
            //Panneau 1
            pnl_affich_Equiv_A.Location = new Point(x_pnl_1, y_pnl_affic_equiv);
            //Panneau 2
            pnl_affich_Equiv_B.Location = new Point(x_pnl_2, y_pnl_affic_equiv);
            //Panneau 3
            pnl_affich_Equiv_C.Location = new Point(x_pnl_3, y_pnl_affic_equiv);
            //Panneau 4
            pnl_affich_Equiv_D.Location = new Point(x_pnl_4, y_pnl_affic_equiv);
            //Panneau 5
            pnl_affich_Equiv_E.Location = new Point(x_pnl_5, y_pnl_affic_equiv);

            //Ajustment de la position des panneaux facteur de securite
            int y_pnl_security_factor = (k.BORDURE_PNL * 16) + (hauteur_pnl * 9);
            //Panneau 1
            pnl_safety_factor1.Location = new Point(x_pnl_1, y_pnl_security_factor);
            //Panneau 2
            pnl_safety_factor2.Location = new Point(x_pnl_2, y_pnl_security_factor);
            //Panneau 3
            pnl_safety_factor3.Location = new Point(x_pnl_3, y_pnl_security_factor);
            //Panneau 4
            pnl_safety_factor4.Location = new Point(x_pnl_4, y_pnl_security_factor);
            //Panneau 5
            pnl_safety_factor5.Location = new Point(x_pnl_5, y_pnl_security_factor);

            //Ajustement de position du panneau comportant le tableur des TDH
            pnl_Tableur.Location = new Point(k.BORDURE_PNL, (hauteur_pnl * 11) +
                (k.BORDURE_PNL * 11));

            //Position pour tout les numeric up/down et combobox dans les 
            //panneaux a la page tuyauterie
            int y_num_In = hauteur_pnl - cBox_materiel1.Height - k.BORDURE_PNL;

            //ComboBox selectionnant le materiel
            cBox_materiel1.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            cBox_materiel2.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            cBox_materiel3.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            cBox_materiel4.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            cBox_materiel5.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            //ComboBox selectionnant le type de tuyau
            cBox_Type1.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            cBox_Type2.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            cBox_Type3.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            cBox_Type4.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            cBox_Type5.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            //Numeric up/down selectionnant le nombre de lignes paralleles
            nUD_LignesParr1.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_LignesParr2.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_LignesParr3.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_LignesParr4.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_LignesParr5.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            //Numeric up/down selectionnant la longueur de section
            nUD_Long1.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Long2.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Long3.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Long4.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Long5.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            //Numeric up/down selectionnant la hauteur de tete statique
            nUD_Static1.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Static2.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Static3.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Static4.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Static5.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            //Numeric up/down selectionnant le poucentage de 
            nUD_Safety_Factor1.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Safety_Factor2.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Safety_Factor3.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Safety_Factor4.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            nUD_Safety_Factor5.Location = new Point(k.BORDURE_NUM_IN, y_num_In);
            //CheckBox d'activation de section
            int x_centre_pnl_titre = (pnl_Titre1.Width / k.PROPORTION_DEUX) -
                                        (checkBox_Active1.Width / k.PROPORTION_DEUX);
            checkBox_Active1.Location = new Point(x_centre_pnl_titre, y_num_In);
            checkBox_Active2.Location = new Point(x_centre_pnl_titre, y_num_In);
            checkBox_Active3.Location = new Point(x_centre_pnl_titre, y_num_In);
            checkBox_Active4.Location = new Point(x_centre_pnl_titre, y_num_In);
            checkBox_Active5.Location = new Point(x_centre_pnl_titre, y_num_In);

            //Positionnement du graphique
            graphique_1.Location = new Point(
                                        pnl_system.Width    //X
                                            + (k.BORDURE_PNL * k.PROPORTION_TROIS),
                                        k.BORDURE_PNL);     //Y


            //==============Ajustement texte=============
            int demi_pnl = largeur_pnl / 2;
            int centre_pnl_mat = demi_pnl - (lbl_Materiel1.Width / 2);
            int y_text_pnl = k.BORDURE_PNL / 3;
            //Texte pnl materiel
            lbl_Materiel1.Location = new Point(centre_pnl_mat, y_text_pnl);
            lbl_Materiel2.Location = new Point(centre_pnl_mat, y_text_pnl);
            lbl_Materiel3.Location = new Point(centre_pnl_mat, y_text_pnl);
            lbl_Materiel4.Location = new Point(centre_pnl_mat, y_text_pnl);
            lbl_Materiel5.Location = new Point(centre_pnl_mat, y_text_pnl);
            //Texte pnl type
            int centre_pnl_type = demi_pnl - (lbl_PipeType1.Width / 2);
            lbl_PipeType1.Location = new Point(centre_pnl_type, y_text_pnl);
            lbl_PipeType2.Location = new Point(centre_pnl_type, y_text_pnl);
            lbl_PipeType3.Location = new Point(centre_pnl_type, y_text_pnl);
            lbl_PipeType4.Location = new Point(centre_pnl_type, y_text_pnl);
            lbl_PipeType5.Location = new Point(centre_pnl_type, y_text_pnl);
            //Texte pnl lignes paralleles
            int centre_pnl_par = demi_pnl - (lbl_lignes_par1.Width / 2);
            lbl_lignes_par1.Location = new Point(centre_pnl_par, y_text_pnl);
            lbl_lignes_par2.Location = new Point(centre_pnl_par, y_text_pnl);
            lbl_lignes_par3.Location = new Point(centre_pnl_par, y_text_pnl);
            lbl_lignes_par4.Location = new Point(centre_pnl_par, y_text_pnl);
            lbl_lignes_par5.Location = new Point(centre_pnl_par, y_text_pnl);
            //Texte pnl longueur section
            int centre_pnl_long = demi_pnl - (lbl_long_sect1.Width / 2);
            lbl_long_sect1.Location = new Point(centre_pnl_long, y_text_pnl);
            lbl_long_sect2.Location = new Point(centre_pnl_long, y_text_pnl);
            lbl_long_sect3.Location = new Point(centre_pnl_long, y_text_pnl);
            lbl_long_sect4.Location = new Point(centre_pnl_long, y_text_pnl);
            lbl_long_sect5.Location = new Point(centre_pnl_long, y_text_pnl);
            //Texte pnl tête statique
            int centre_pnl_head = demi_pnl - (lbl_static_head1.Width / 2);
            lbl_static_head1.Location = new Point(centre_pnl_head, y_text_pnl);
            lbl_static_head2.Location = new Point(centre_pnl_head, y_text_pnl);
            lbl_static_head3.Location = new Point(centre_pnl_head, y_text_pnl);
            lbl_static_head4.Location = new Point(centre_pnl_head, y_text_pnl);
            lbl_static_head5.Location = new Point(centre_pnl_head, y_text_pnl);
            //Texte de section
            int centre_pnl_section = demi_pnl - (lbl_Section1.Width / 2);
            lbl_Section1.Location = new Point(centre_pnl_section, y_text_pnl);
            lbl_Section2.Location = new Point(centre_pnl_section, y_text_pnl);
            lbl_Section3.Location = new Point(centre_pnl_section, y_text_pnl);
            lbl_Section4.Location = new Point(centre_pnl_section, y_text_pnl);
            lbl_Section5.Location = new Point(centre_pnl_section, y_text_pnl);
            //Message d'erreur de debit nul
            int x_lbl_debit_nul = graphique_1.Location.X + k.BORDURE_PNL * 3;
            int y_lbl_debit_nul = graphique_1.Location.Y
                                    + graphique_1.Height
                                        + (k.BORDURE_PNL * k.PROPORTION_TROIS);
            lbl_Debit_Nul.Location = new Point(x_lbl_debit_nul, y_lbl_debit_nul);

            //===Ajustement du panneau, parent des panneaux materiel et type===
            pnl_SystemInput.Height = dimensions[k.HAUTEUR];
            pnl_SystemInput.Width = dimensions[k.LARGEUR];
            pnl_SystemInput.Location = new Point(0, 0);

            //Ajustement du texte du panneau facteur
            int x_lbl_facteur = (pnl_safety_factor1.Width / k.PROPORTION_DEUX)
                                    - (lbl_facteur1.Width / k.PROPORTION_DEUX);
            lbl_facteur1.Location = new Point(x_lbl_facteur, y_text_pnl);
            lbl_facteur2.Location = new Point(x_lbl_facteur, y_text_pnl);
            lbl_facteur3.Location = new Point(x_lbl_facteur, y_text_pnl);
            lbl_facteur4.Location = new Point(x_lbl_facteur, y_text_pnl);
            lbl_facteur5.Location = new Point(x_lbl_facteur, y_text_pnl);
        }

        /*Ajustment des pnl des fitting qui s'ouvrent en cliquant sur le bouton 
         Fitting pour ajouter et modifier les fittings*/
        void ajustment_pnl_equiv()
        {
            //Au centre de l'ecran
            int pnl_X = (dimensions[k.LARGEUR] / k.PROPORTION_DEUX)
                                    - (pnl_Equiv_A.Width / k.PROPORTION_DEUX);
            int pnl_Y = (dimensions[k.HAUTEUR] / k.PROPORTION_DEUX)
                                    - (pnl_Equiv_A.Height / k.PROPORTION_DEUX);
            //Attribution des positions
            pnl_Equiv_A.Location = new Point(pnl_X, pnl_Y);
            pnl_Equiv_B.Location = new Point(pnl_X, pnl_Y);
            pnl_Equiv_C.Location = new Point(pnl_X, pnl_Y);
            pnl_Equiv_D.Location = new Point(pnl_X, pnl_Y);
            pnl_Equiv_E.Location = new Point(pnl_X, pnl_Y);
        }

        void ajustement_pnl_edition()
        {
            int pnl_x = (dimensions[k.LARGEUR] / 2) - (pnl_Edition_Materiel.Width / 2);
            int pnl_y = (dimensions[k.HAUTEUR] / 2) - (pnl_Edition_Materiel.Height / 2);

            pnl_Edition_Materiel.Location = new Point(pnl_x, pnl_y);
            pnl_Edition_Pipe.Location = new Point(pnl_x, pnl_y);
        }

        /*Ajustement des images et picturebox*/
        void ajustement_images()
        {
            //*Ajustement du logo de la page d'acceuil*//
            //Largeur
            pic_Logo_Acceuil_FR.Width = Page_Acceuil.Width;
            pic_Logo_AcceuiL_EN.Width = Page_Acceuil.Width;
            //Hauteur
            pic_Logo_Acceuil_FR.Height = pic_Logo_Acceuil_FR.Width / 4;
            pic_Logo_AcceuiL_EN.Height = pic_Logo_Acceuil_FR.Width / 4;
            //POsition en Y
            int y_pic_logo_Acceuil = (Page_Acceuil.Height / 4)
                                                - (pic_Logo_Acceuil_FR.Height / 2);
            //Positionnement
            pic_Logo_Acceuil_FR.Location = new Point(0, y_pic_logo_Acceuil);
            pic_Logo_AcceuiL_EN.Location = new Point(0, y_pic_logo_Acceuil);

            //*Ajustement du logo sur le graphique*//
            //Largeur
            pic_Logo_graph_FR.Width = (pnl_EnTete_Graph.Width * k.PROPORTION_CINQ)
                                                        / k.PROPORTION_DIXHUIT;
            pic_Logo_graph_EN.Width = pic_Logo_graph_FR.Width;
            //Hauteur
            pic_Logo_graph_FR.Height = pic_Logo_graph_FR.Width / k.PROPORTION_QUATRE;
            pic_Logo_graph_EN.Height = pic_Logo_graph_FR.Height;
            //Position en X
            int x_pic_Logo_graph = (pnl_EnTete_Graph.Width / k.PROPORTION_QUATRE)
                                                - (pic_Logo_graph_FR.Width / 2);
        }
        #endregion

        /*********************************************************************/
        //Region contenant les fonctions
        #region Fonctions et Calculs

        //Algorithme de lecture d'un fichier de sauvegare
        private void ouvrir_fichier_sauvegarde()
        {
            string buffer = "";
            int i = 0;

            myStream = null;
            //Ouverture d'une boite de dialogue pour browser le fichier a ouvrir
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //Pointe vers le dernier fichier ouvert
            if (pathstring != null)
            {
                openFileDialog1.InitialDirectory = pathstring;
            }
            else
            {
                openFileDialog1.InitialDirectory = "c:\\";
            }
            //Pointe vers l'extension curve files ou tous les fichiers
            openFileDialog1.Filter = "curve files (*.curv)|*.curv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            //Une fois le fichier selectionne et le bouton OK appuye
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Tentative d'ouverture du fichier
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        //Tentative reussi, alors creation d'un lecteur
                        using (StreamReader sr = new StreamReader(myStream))
                        {
                            //Commence par lire la premiere ligne avec iterateur a 0
                            buffer = sr.ReadLine();
                            i = 0;
                            //Tant que la ligne lue ne commence pas par /
                            while (buffer.StartsWith("/") == false)
                            {
                                //Appel de la fonction pour infoclient
                                ouverture_sauvegarde_infoClient(buffer, i);
                                //Iteration
                                i++;
                                //Lecture de la prochaine ligne
                                buffer = sr.ReadLine();
                            }

                            //Saut a la prochaine ligne pour eviter la ligne commencant par /
                            buffer = sr.ReadLine();
                            //Iterateur a 0
                            i = 0;
                            //tant que la ligne lue ne commence pas par /
                            while (buffer.StartsWith("/") == false)
                            {
                                ouverture_sauvegarde_action_debit(buffer, i);
                                //Iteration
                                i++;
                                //Lecture de la prochaine ligne
                                buffer = sr.ReadLine();
                            }
                            //Lecture des lignes 11 a 13, les pression de la page point d'action
                            //Saut a la prochaine ligne pour eviter la ligne commencant par /
                            buffer = sr.ReadLine();
                            //Iterateur a 0
                            i = 0;
                            //tant que la ligne lue ne commence pas par /
                            while (buffer.StartsWith("/") == false)
                            {
                                ouverture_sauvegarde_action_pression(buffer, i);
                                //Iteration
                                i++;
                                //Lecture de la prochaine ligne
                                buffer = sr.ReadLine();
                            }
                            //Lecture des lignes 14 a 16, les lignes pour le mode de point action
                            //Saut a la prochaine ligne pour eviter la ligne commencant par /
                            buffer = sr.ReadLine();
                            //Iterateur a 0
                            i = 0;
                            //tant que la ligne lue ne commence pas par /
                            while (buffer.StartsWith("/") == false)
                            {
                                ouverture_sauvegarde_action_droite_ou_point(buffer, i);
                                //Iteration
                                i++;
                                //Lecture de la prochaine ligne
                                buffer = sr.ReadLine();
                            }
                            //Lecture des lignes 17 a 19, les lignes pour les legendes auto ou manuelles
                            //Saut a la prochaine ligne pour eviter la ligne commencant par /
                            buffer = sr.ReadLine();
                            //Iterateur a 0
                            i = 0;
                            //tant que la ligne lue ne commence pas par /
                            while (buffer.StartsWith("/") == false)
                            {
                                ouverture_sauvegarde_action_mode_legende(buffer, i);
                                //Iteration
                                i++;
                                //Lecture de la prochaine ligne
                                buffer = sr.ReadLine();
                            }
                            //Lecture des lignes 20 a 22, les lignes contenant les legendes manuelles
                            //Saut a la prochaine ligne pour eviter la ligne commencant par /
                            buffer = sr.ReadLine();
                            //Iterateur a 0
                            i = 0;
                            //tant que la ligne lue ne commence pas par /
                            while (buffer.StartsWith("/") == false)
                            {
                                ouverture_sauvegarde_action_legende(buffer, i);
                                //Iteration
                                i++;
                                //Lecture de la prochaine ligne
                                buffer = sr.ReadLine();
                            }
                            //Lecture des info de tuyauterie
                            //Saut a la prochaine ligne pour eviter la ligne commencant par /
                            buffer = sr.ReadLine();
                            //Iterateur a 0
                            i = 0;
                            //tant que la ligne lue ne commence pas par /
                            while (buffer.StartsWith("/") == false)
                            {
                                ouverture_sauvegarde_tuyauterie(buffer, i);
                                //Iteration
                                i++;
                                //Lecture de la prochaine ligne
                                buffer = sr.ReadLine();
                            }
                            //Lecture des info de pompe
                            raz_page_pompe();
                            //Saut a la prochaine ligne pour eviter la ligne commencant par /
                            buffer = sr.ReadLine();
                            //Iterateur a 0
                            i = 0;
                            //tant que la ligne lue ne commence pas par /
                            while (buffer.StartsWith("/") == false)
                            {
                                ouverture_sauvegarde_pompe(buffer, i);
                                //Iteration
                                i++;
                                //Lecture de la prochaine ligne
                                buffer = sr.ReadLine();
                            }
                            //Travage des courbes de pompes importees et valide
                            //ouverture_sauvegarde_tracage_pompe();
                            retracage_pompe();
                        }
                        myStream.Close();
                    }
                }
                //Message d'erreur
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Erreur: Incapable de lire de fichier sur le disque: "
                            + ex.Message);
                }
            }
        }
        //Lecture des lignes 1 a 6 pour obtenir les informations client
        private void ouverture_sauvegarde_infoClient(string ligne, int i)
        {
            switch (i)
            {
                //Nom du client
                case 0:
                    tBox_client_Nom.Text = ligne; break;
                //Nom du projet
                case 1:
                    tBox_client_Projet.Text = ligne; break;
                //Description du projet
                case 2:
                    tBox_client_Description.Text = ligne; break;
                //Mention fait par
                case 3:
                    tBox_client_DoneBy.Text = ligne; break;
                //Revision de fichier
                case 4:
                    tBox_client_Revision.Text = ligne; break;
                //Date du fichier
                case 5:
                    tBox_client_Date.Text = ligne; break;
                //Langage
                case 6:
                    if (ligne == "francais")
                    {
                        rBtn_Francais.Checked = true;
                        rBtn_English.Checked = false;
                        traduction_francais();
                    }
                    else if (ligne == "english")
                    {
                        rBtn_Francais.Checked = false;
                        rBtn_English.Checked = true;
                        traduction_anglais();
                    }
                    break;
                    //Unite de debit
                case 7:
                    if (ligne == "usgpm")
                    {
                        unite_en_USGPM();
                        changement_texte_unites();
                    }
                    else if (ligne == "m3/hr")
                    {
                        unite_en_m3hr();
                        changement_texte_unites();
                    }
                    break;
                //Unite de longueur
                case 8:
                    if (ligne == "metres")
                    {
                        unite_en_metres();
                        changement_texte_unites();
                    }
                    else if (ligne == "pieds")
                    {
                        unite_en_pieds();
                        changement_texte_unites();
                    }
                    break;
                //Unite de pression
                case 9:
                    if (ligne == "fth20")
                    {
                        unite_en_fth20();
                        changement_texte_unites();
                    }
                    else if (ligne == "mh20")
                    {
                        unite_en_mh20();
                        changement_texte_unites();
                    }
                    else if (ligne == "psi")
                    {
                        unite_en_psi();
                        changement_texte_unites();
                    }
                    else if (ligne == "kpa")
                    {
                        unite_en_pascal();
                        changement_texte_unites();
                    }
                    break;
                    //Unite de puissance
                case 10:
                    if (ligne == "hp")
                    {
                        unite_en_hp();
                        changement_texte_unites();
                    }
                    else if (ligne == "watts")
                    {
                        unite_en_watts();
                        changement_texte_unites();
                    }
                    break;
            }
        }
        //Lecture des lignes 7 a 10 pour obtenir les informations Debit
        private void ouverture_sauvegarde_action_debit(string ligne, int i)
        {
            if (ligne != "")
            {
                switch (i)
                {
                    case 0:
                        nUD_Debit.Value = int.Parse(ligne); break;
                    case 1:
                        nUD_X_action1.Value = int.Parse(ligne); break;
                    case 2:
                        nUD_X_action2.Value = int.Parse(ligne); break;
                    case 3:
                        nUD_X_action3.Value = int.Parse(ligne); break;
                }
            }
        }
        //Lecture des lignes 11 a 13 pour obtenir les informations Pression
        private void ouverture_sauvegarde_action_pression(string ligne, int i)
        {
            if (ligne != "")
            {
                switch (i)
                {
                    case 0:
                        nUD_Y_action1.Value = int.Parse(ligne); break;
                    case 1:
                        nUD_Y_action2.Value = int.Parse(ligne); break;
                    case 2:
                        nUD_Y_action3.Value = int.Parse(ligne); break;
                }
            }
        }
        //Lecture des lignes 14 a 16 pour obtenir les modes de points d'action
        private void ouverture_sauvegarde_action_droite_ou_point(string ligne, int i)
        {
            switch (i)
            {
                case 0:
                    if (bool.Parse(ligne) == true)
                    {
                        droite(k.DROITE_P1);
                    }
                    else if (bool.Parse(ligne) == false)
                    {
                        point(k.DROITE_P1);
                    }
                    break;
                case 1:
                    if (bool.Parse(ligne) == true)
                    {
                        droite(k.DROITE_P2);
                    }
                    else if (bool.Parse(ligne) == false)
                    {
                        point(k.DROITE_P2);
                    }
                    break;
                case 2:
                    if (bool.Parse(ligne) == true)
                    {
                        droite(k.DROITE_P3);
                    }
                    else if (bool.Parse(ligne) == false)
                    {
                        point(k.DROITE_P3);
                    }
                    break;
            }
        }
        //Lecture des lignes 17 a 19 pour obtenir les mode de legende
        private void ouverture_sauvegarde_action_mode_legende(string ligne, int i)
        {
            switch (i)
            {
                case 0:
                    checkBox_Legend_Auto1.Checked = (bool.Parse(ligne) == true) ? true : false;
                    break;
                case 1:
                    checkBox_Legend_Auto2.Checked = (bool.Parse(ligne) == true) ? true : false;
                    break;
                case 2:
                    checkBox_Legend_Auto3.Checked = (bool.Parse(ligne) == true) ? true : false;
                    break;
            }
        }
        //Lecture des lignes 20 a 22 pour obtenir les legendes points d'action
        private void ouverture_sauvegarde_action_legende(string ligne, int i)
        {
            switch (i)
            {
                case 0:
                    textBox_Action1.Text = ligne; break;
                case 1:
                    textBox_Action2.Text = ligne; break;
                case 2:
                    textBox_Action3.Text = ligne; break;
            }
        }
        //Lecture des 135 prochaines lignes pour obtenir les info de tuyauterie
        private void ouverture_sauvegarde_tuyauterie(string ligne, int i)
        {
            //Premierement, on verifie si la ligne lu a un contenu ou si elle est vide.
            //Par la suite, les switch case sont divises pour limiter les operations
            if (ligne != "")
            {

                switch (i)
                {
                    //==================Section A==============================
                    //Section active ou non
                    case 0:
                        checkBox_Active1.Checked =
                            (bool.Parse(ligne) == true) ? true : false; break;
                    //Materiel utilise
                    case 1:
                        cBox_materiel1.SelectedItem = ligne; break;
                    //Type de tuyau
                    case 2:
                        cBox_Type1.SelectedItem = ligne; break;
                    //Nombre de lignes parrallele
                    case 3:
                        nUD_LignesParr1.Value = int.Parse(ligne); break;
                    //Longueur de section
                    case 4:
                        nUD_Long1.Value = int.Parse(ligne); break;
                    //Hauteur statique de la section
                    case 5:
                        nUD_Static1.Value = int.Parse(ligne); break;
                    //Nb Fitting 1
                    case 6:
                        nUD_A_1.Value = int.Parse(ligne); break;
                    //Fitting 1
                    case 7:
                        cBox_Fitting_A_1.SelectedItem = ligne; break;
                    //Nb Fitting 2
                    case 8:
                        nUD_A_2.Value = int.Parse(ligne); break;
                    //Fitting 2
                    case 9:
                        cBox_Fitting_A_2.SelectedItem = ligne; break;
                    //Nb Fitting 3
                    case 10:
                        nUD_A_3.Value = int.Parse(ligne); break;
                    //Fitting 3
                    case 11:
                        cBox_Fitting_A_3.SelectedItem = ligne; break;
                    //Nb Fitting 4
                    case 12:
                        nUD_A_4.Value = int.Parse(ligne); break;
                    //Fitting 4
                    case 13:
                        cBox_Fitting_A_4.SelectedItem = ligne; break;
                    //Nb Fitting 5
                    case 14:
                        nUD_A_5.Value = int.Parse(ligne); break;
                    //Fitting 5
                    case 15:
                        cBox_Fitting_A_5.SelectedItem = ligne; break;
                    //Nb Fitting 6
                    case 16:
                        nUD_A_6.Value = int.Parse(ligne); break;
                    //Fitting 6
                    case 17:
                        cBox_Fitting_A_6.SelectedItem = ligne; break;
                    //Nb Fitting 7
                    case 18:
                        nUD_A_7.Value = int.Parse(ligne); break;
                    //FItting 7
                    case 19:
                        cBox_Fitting_A_7.SelectedItem = ligne; break;
                    //Nb Fitting 8
                    case 20:
                        nUD_A_8.Value = int.Parse(ligne); break;
                    //Fitting 8
                    case 21:
                        cBox_Fitting_A_8.SelectedItem = ligne; break;
                    //Nb Fitting 9
                    case 22:
                        nUD_A_9.Value = int.Parse(ligne); break;
                    //Fitting 9
                    case 23:
                        cBox_Fitting_A_9.SelectedItem = ligne; break;
                    //Nb Fitting 10
                    case 24:
                        nUD_A_10.Value = int.Parse(ligne); break;
                    //Fitting 10
                    case 25:
                        cBox_Fitting_A_10.SelectedItem = ligne; break;
                    //Facteur de securite 
                    case 26:
                        nUD_Safety_Factor1.Value = int.Parse(ligne);
                        break;


                    //==================Section B==============================
                    case 27:
                        checkBox_Active2.Checked =
                            (bool.Parse(ligne) == true) ? true : false; break;
                    case 28:
                        cBox_materiel2.SelectedItem = ligne; break;
                    case 29:
                        cBox_Type2.SelectedItem = ligne; break;
                    case 30:
                        nUD_LignesParr2.Value = int.Parse(ligne); break;
                    case 31:
                        nUD_Long2.Value = int.Parse(ligne); break;
                    case 32:
                        nUD_Static2.Value = int.Parse(ligne); break;
                    ///1
                    case 33:
                        nUD_B_1.Value = int.Parse(ligne); break;
                    case 34:
                        cBox_Fitting_B_1.SelectedItem = ligne; break;
                    //2
                    case 35:
                        nUD_B_2.Value = int.Parse(ligne); break;
                    case 36:
                        cBox_Fitting_B_2.SelectedItem = ligne; break;
                    //3
                    case 37:
                        nUD_B_3.Value = int.Parse(ligne); break;
                    case 38:
                        cBox_Fitting_B_3.SelectedItem = ligne; break;
                    //4
                    case 39:
                        nUD_B_4.Value = int.Parse(ligne); break;
                    case 40:
                        cBox_Fitting_B_4.SelectedItem = ligne; break;
                    //5
                    case 41:
                        nUD_B_5.Value = int.Parse(ligne); break;
                    case 42:
                        cBox_Fitting_B_5.SelectedItem = ligne; break;
                    //6
                    case 43:
                        nUD_B_6.Value = int.Parse(ligne); break;
                    case 44:
                        cBox_Fitting_B_6.SelectedItem = ligne; break;
                    //7
                    case 45:
                        nUD_B_7.Value = int.Parse(ligne); break;
                    case 46:
                        cBox_Fitting_B_7.SelectedItem = ligne; break;
                    //8
                    case 47:
                        nUD_B_8.Value = int.Parse(ligne); break;
                    case 48:
                        cBox_Fitting_B_8.SelectedItem = ligne; break;
                    //9
                    case 49:
                        nUD_B_9.Value = int.Parse(ligne); break;
                    case 50:
                        cBox_Fitting_B_9.SelectedItem = ligne; break;
                    //10
                    case 51:
                        nUD_B_10.Value = int.Parse(ligne); break;
                    case 52:
                        cBox_Fitting_B_10.SelectedItem = ligne; break;
                    //11
                    case 53:
                        nUD_Safety_Factor2.Value = int.Parse(ligne); break;

                    //==================Section C==============================
                    case 54:
                        checkBox_Active3.Checked =
                            (bool.Parse(ligne) == true) ? true : false; break;
                    case 55:
                        cBox_materiel3.SelectedItem = ligne; break;
                    case 56:
                        cBox_Type3.SelectedItem = ligne; break;
                    case 57:
                        nUD_LignesParr3.Value = int.Parse(ligne); break;
                    case 58:
                        nUD_Long3.Value = int.Parse(ligne); break;
                    case 59:
                        nUD_Static3.Value = int.Parse(ligne); break;
                    ///1
                    case 60:
                        nUD_C_1.Value = int.Parse(ligne); break;
                    case 61:
                        cBox_Fitting_C_1.SelectedItem = ligne; break;
                    //2
                    case 62:
                        nUD_C_2.Value = int.Parse(ligne); break;
                    case 63:
                        cBox_Fitting_C_2.SelectedItem = ligne; break;
                    //3
                    case 64:
                        nUD_C_3.Value = int.Parse(ligne); break;
                    case 65:
                        cBox_Fitting_C_3.SelectedItem = ligne; break;
                    //4
                    case 66:
                        nUD_C_4.Value = int.Parse(ligne); break;
                    case 67:
                        cBox_Fitting_C_4.SelectedItem = ligne; break;
                    //5
                    case 68:
                        nUD_C_5.Value = int.Parse(ligne); break;
                    case 69:
                        cBox_Fitting_C_5.SelectedItem = ligne; break;
                    //6
                    case 70:
                        nUD_C_6.Value = int.Parse(ligne); break;
                    case 71:
                        cBox_Fitting_C_6.SelectedItem = ligne; break;
                    //7
                    case 72:
                        nUD_C_7.Value = int.Parse(ligne); break;
                    case 73:
                        cBox_Fitting_C_7.SelectedItem = ligne; break;
                    //8
                    case 74:
                        nUD_C_8.Value = int.Parse(ligne); break;
                    case 75:
                        cBox_Fitting_C_8.SelectedItem = ligne; break;
                    //9
                    case 76:
                        nUD_C_9.Value = int.Parse(ligne); break;
                    case 77:
                        cBox_Fitting_C_9.SelectedItem = ligne; break;
                    //10
                    case 78:
                        nUD_C_10.Value = int.Parse(ligne); break;
                    case 79:
                        cBox_Fitting_C_10.SelectedItem = ligne; break;
                    //Facteur securite
                    case 80:
                        nUD_Safety_Factor3.Value = int.Parse(ligne); break;

                    //==================Section D==============================
                    case 81:
                        checkBox_Active4.Checked =
                            (bool.Parse(ligne) == true) ? true : false; break;
                    case 82:
                        cBox_materiel4.SelectedItem = ligne; break;
                    case 83:
                        cBox_Type4.SelectedItem = ligne; break;
                    case 84:
                        nUD_LignesParr4.Value = int.Parse(ligne); break;
                    case 85:
                        nUD_Long4.Value = int.Parse(ligne); break;
                    case 86:
                        nUD_Static4.Value = int.Parse(ligne); break;
                    ///1
                    case 87:
                        nUD_D_1.Value = int.Parse(ligne); break;
                    case 88:
                        cBox_Fitting_D_1.SelectedItem = ligne; break;
                    //2
                    case 89:
                        nUD_D_2.Value = int.Parse(ligne); break;
                    case 90:
                        cBox_Fitting_D_2.SelectedItem = ligne; break;
                    //3
                    case 91:
                        nUD_D_3.Value = int.Parse(ligne); break;
                    case 92:
                        cBox_Fitting_D_3.SelectedItem = ligne; break;
                    //4
                    case 93:
                        nUD_D_4.Value = int.Parse(ligne); break;
                    case 94:
                        cBox_Fitting_D_4.SelectedItem = ligne; break;
                    //5
                    case 95:
                        nUD_D_5.Value = int.Parse(ligne); break;
                    case 96:
                        cBox_Fitting_D_5.SelectedItem = ligne; break;
                    //6
                    case 97:
                        nUD_D_6.Value = int.Parse(ligne); break;
                    case 98:
                        cBox_Fitting_D_6.SelectedItem = ligne; break;
                    //7
                    case 99:
                        nUD_D_7.Value = int.Parse(ligne); break;
                    case 100:
                        cBox_Fitting_D_7.SelectedItem = ligne; break;
                    //8
                    case 101:
                        nUD_D_8.Value = int.Parse(ligne); break;
                    case 102:
                        cBox_Fitting_D_8.SelectedItem = ligne; break;
                    //9
                    case 103:
                        nUD_D_9.Value = int.Parse(ligne); break;
                    case 104:
                        cBox_Fitting_D_9.SelectedItem = ligne; break;
                    //10
                    case 105:
                        nUD_D_10.Value = int.Parse(ligne); break;
                    case 106:
                        cBox_Fitting_D_10.SelectedItem = ligne; break;
                    //11
                    case 107:
                        nUD_Safety_Factor4.Value = int.Parse(ligne); break;

                    //==================Section E==============================
                    case 108:
                        checkBox_Active5.Checked =
                            (bool.Parse(ligne) == true) ? true : false; break;
                    case 109:
                        cBox_materiel5.SelectedItem = ligne; break;
                    case 110:
                        cBox_Type5.SelectedItem = ligne; break;
                    case 111:
                        nUD_LignesParr5.Value = int.Parse(ligne); break;
                    case 112:
                        nUD_Long5.Value = int.Parse(ligne); break;
                    case 113:
                        nUD_Static5.Value = int.Parse(ligne); break;
                    ///1
                    case 114:
                        nUD_E_1.Value = int.Parse(ligne); break;
                    case 115:
                        cBox_Fitting_E_1.SelectedItem = ligne; break;
                    //2
                    case 116:
                        nUD_E_2.Value = int.Parse(ligne); break;
                    case 117:
                        cBox_Fitting_E_2.SelectedItem = ligne; break;
                    //3
                    case 118:
                        nUD_E_3.Value = int.Parse(ligne); break;
                    case 119:
                        cBox_Fitting_E_3.SelectedItem = ligne; break;
                    //4
                    case 120:
                        nUD_E_4.Value = int.Parse(ligne); break;
                    case 121:
                        cBox_Fitting_E_4.SelectedItem = ligne; break;
                    //5
                    case 122:
                        nUD_E_5.Value = int.Parse(ligne); break;
                    case 123:
                        cBox_Fitting_E_5.SelectedItem = ligne; break;
                    //6
                    case 124:
                        nUD_E_6.Value = int.Parse(ligne); break;
                    case 125:
                        cBox_Fitting_E_6.SelectedItem = ligne; break;
                    //7
                    case 126:
                        nUD_E_7.Value = int.Parse(ligne); break;
                    case 127:
                        cBox_Fitting_E_7.SelectedItem = ligne; break;
                    //8
                    case 128:
                        nUD_E_8.Value = int.Parse(ligne); break;
                    case 129:
                        cBox_Fitting_E_8.SelectedItem = ligne; break;
                    //9
                    case 130:
                        nUD_E_9.Value = int.Parse(ligne); break;
                    case 131:
                        cBox_Fitting_E_9.SelectedItem = ligne; break;
                    //10
                    case 132:
                        nUD_E_10.Value = int.Parse(ligne); break;
                    case 133:
                        cBox_Fitting_E_10.SelectedItem = ligne; break;
                    //Facteur de securite
                    case 134:
                        nUD_Safety_Factor5.Value = int.Parse(ligne); break;
                }
            }
        }
        //Lecture des info de pompe
        private void ouverture_sauvegarde_pompe(string ligne, int i)
        {
            switch (i)
            {
                /*===========Pompe 1=================*/
                //Marque
                case 0: cBox_Marque1.SelectedIndex = int.Parse(ligne); break;
                case 1:
                    if ((string)cBox_Marque1.SelectedItem != ligne && cBox_Marque1.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 1");
                    }
                    break;
                //Serie
                case 2: cBox_Serie1.SelectedIndex = int.Parse(ligne); break;
                case 3:
                    if ((string)cBox_Serie1.SelectedItem != ligne && cBox_Serie1.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 2");
                    }
                    break;
                //Modele
                case 4: cBox_Modele1.SelectedIndex = int.Parse(ligne); break;
                case 5:
                    if ((string)cBox_Modele1.SelectedItem != ligne && cBox_Modele1.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 3");
                    }
                    break;
                //Facteur de modification
                case 6: nUD_Nb_Pompe1.Value = int.Parse(ligne); break;
                case 7:
                    r_Btn_Parr1.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie1.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_1, r_Btn_Parr1.Checked);
                    break;
                case 8:
                    nUD_Nb_Stage1.Value = int.Parse(ligne);
                    break;
                case 9: nUD_Ratio1.Value = int.Parse(ligne); break;
                case 10: nUD_Vit_P1.Value = int.Parse(ligne); break;
                /*======================Pompe 2=======================*/
                //Marque
                case 11: cBox_Marque2.SelectedIndex = int.Parse(ligne); break;
                case 12:
                    if ((string)cBox_Marque2.SelectedItem != ligne && cBox_Marque2.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 4");
                    }
                    break;
                //Serie
                case 13: cBox_Serie2.SelectedIndex = int.Parse(ligne); break;
                case 14:
                    if ((string)cBox_Serie2.SelectedItem != ligne && cBox_Serie2.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 5");
                    }
                    break;
                //Modele
                case 15: cBox_Modele2.SelectedIndex = int.Parse(ligne); break;
                case 16:
                    if ((string)cBox_Modele2.SelectedItem != ligne && cBox_Modele2.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 6");
                    }
                    break;
                //Facteur de modification
                case 17: nUD_Nb_Pompe2.Value = int.Parse(ligne); break;
                case 18:
                    r_Btn_Parr2.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie2.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_2, r_Btn_Parr2.Checked);
                    break;
                case 19:
                    nUD_Nb_Stage2.Value = int.Parse(ligne);
                    break;
                case 20: nUD_Ratio2.Value = int.Parse(ligne); break;
                case 21: nUD_Vit_P2.Value = int.Parse(ligne); break;
                /*=================Pompe 3==================================*/
                //Marque
                case 22: cBox_Marque3.SelectedIndex = int.Parse(ligne); break;
                case 23:
                    if ((string)cBox_Marque3.SelectedItem != ligne && cBox_Marque3.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 7");
                    }
                    break;
                //Serie
                case 24: cBox_Serie3.SelectedIndex = int.Parse(ligne); break;
                case 25:
                    if ((string)cBox_Serie3.SelectedItem != ligne && cBox_Serie3.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 7");
                    }
                    break;
                //Modele
                case 26: cBox_Modele3.SelectedIndex = int.Parse(ligne); break;
                case 27:
                    if ((string)cBox_Modele3.SelectedItem != ligne && cBox_Modele3.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 7");
                    }
                    break;
                //Facteur de modification
                case 28: nUD_Nb_Pompe3.Value = int.Parse(ligne); break;
                case 29:
                    r_Btn_Parr3.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie3.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_3, r_Btn_Parr3.Checked);
                    break;
                case 30:
                    nUD_Nb_Stage3.Value = int.Parse(ligne);
                    break;
                case 31: nUD_Ratio3.Value = int.Parse(ligne); break;
                case 32: nUD_Vit_P3.Value = int.Parse(ligne); break;
                /*=============Pompe 4=====================*/
                //Marque
                case 33: cBox_Marque4.SelectedIndex = int.Parse(ligne); break;
                case 34:
                    if ((string)cBox_Marque4.SelectedItem != ligne && cBox_Marque4.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 8");
                    }
                    break;
                //Serie
                case 35: cBox_Serie4.SelectedIndex = int.Parse(ligne); break;
                case 36:
                    if ((string)cBox_Serie4.SelectedItem != ligne && cBox_Serie4.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 9");
                    }
                    break;
                //Modele
                case 37: cBox_Modele4.SelectedIndex = int.Parse(ligne); break;
                case 38:
                    if ((string)cBox_Modele4.SelectedItem != ligne && cBox_Modele4.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 10");
                    }
                    break;
                //Facteur de modification
                case 39: nUD_Nb_Pompe4.Value = int.Parse(ligne); break;
                case 40:
                    r_Btn_Parr4.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie4.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_4, r_Btn_Parr4.Checked);
                    break;
                case 41:
                    nUD_Nb_Stage4.Value = int.Parse(ligne);
                    break;
                case 42: nUD_Ratio4.Value = int.Parse(ligne); break;
                case 43: nUD_Vit_P4.Value = int.Parse(ligne); break;



                /*=======Pompe 5=======================*/
                //Marque
                case 44: cBox_Marque5.SelectedIndex = int.Parse(ligne); break;
                case 45:
                    if ((string)cBox_Marque5.SelectedItem != ligne && cBox_Marque5.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture 11");
                    }
                    break;
                //Serie
                case 46: cBox_Serie5.SelectedIndex = int.Parse(ligne); break;
                case 47:
                    if ((string)cBox_Serie5.SelectedItem != ligne && cBox_Serie5.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Modele
                case 48: cBox_Modele5.SelectedIndex = int.Parse(ligne); break;
                case 49:
                    if ((string)cBox_Modele5.SelectedItem != ligne && cBox_Modele5.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Facteur de modification
                case 50: nUD_Nb_Pompe5.Value = int.Parse(ligne); break;
                case 51:
                    r_Btn_Parr5.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie5.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_5, r_Btn_Parr5.Checked);
                    break;
                case 52:
                    nUD_Nb_Stage5.Value = int.Parse(ligne);
                    break;
                case 53: nUD_Ratio5.Value = int.Parse(ligne); break;
                case 54: nUD_Vit_P5.Value = int.Parse(ligne); break;

                /*================Pompe 6=======================*/
                //Marque
                case 55: cBox_Marque6.SelectedIndex = int.Parse(ligne); break;
                case 56:
                    if ((string)cBox_Marque6.SelectedItem != ligne && cBox_Marque6.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Serie
                case 57: cBox_Serie6.SelectedIndex = int.Parse(ligne); break;
                case 58:
                    if ((string)cBox_Serie6.SelectedItem != ligne && cBox_Serie6.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Modele
                case 59: cBox_Modele6.SelectedIndex = int.Parse(ligne); break;
                case 60:
                    if ((string)cBox_Modele6.SelectedItem != ligne && cBox_Modele6.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Facteur de modification
                case 61: nUD_Nb_Pompe6.Value = int.Parse(ligne); break;
                case 62:
                    r_Btn_Parr6.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie6.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_6, r_Btn_Parr6.Checked);
                    break;
                case 63:
                    nUD_Nb_Stage6.Value = int.Parse(ligne);
                    break;
                case 64: nUD_Ratio6.Value = int.Parse(ligne); break;
                case 65: nUD_Vit_P6.Value = int.Parse(ligne); break;
                /*=================Pompe 7======================*/
                //Marque
                case 66: cBox_Marque7.SelectedIndex = int.Parse(ligne); break;
                case 67:
                    if ((string)cBox_Marque7.SelectedItem != ligne && cBox_Marque7.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Serie
                case 68: cBox_Serie7.SelectedIndex = int.Parse(ligne); break;
                case 69:
                    if ((string)cBox_Serie7.SelectedItem != ligne && cBox_Serie7.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Modele
                case 70: cBox_Modele7.SelectedIndex = int.Parse(ligne); break;
                case 71:
                    if ((string)cBox_Modele7.SelectedItem != ligne && cBox_Modele7.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Facteur de modification
                case 72: nUD_Nb_Pompe7.Value = int.Parse(ligne); break;
                case 73:
                    r_Btn_Parr7.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie7.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_7, r_Btn_Parr7.Checked);
                    break;
                case 74:
                    nUD_Nb_Stage7.Value = int.Parse(ligne);
                    break;
                case 75: nUD_Ratio7.Value = int.Parse(ligne); break;
                case 76: nUD_Vit_P7.Value = int.Parse(ligne); break;
                /*==================Pompe 8=======================*/
                //Marque
                case 77: cBox_Marque8.SelectedIndex = int.Parse(ligne); break;
                case 78:
                    if ((string)cBox_Marque8.SelectedItem != ligne  && cBox_Marque8.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Serie
                case 79: cBox_Serie8.SelectedIndex = int.Parse(ligne); break;
                case 80:
                    if ((string)cBox_Serie8.SelectedItem != ligne && cBox_Serie8.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Modele
                case 81: cBox_Modele8.SelectedIndex = int.Parse(ligne); break;
                case 82:
                    if ((string)cBox_Modele8.SelectedItem != ligne && cBox_Modele8.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Facteur de modification
                case 83: nUD_Nb_Pompe8.Value = int.Parse(ligne); break;
                case 84:
                    r_Btn_Parr8.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie8.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_8, r_Btn_Parr8.Checked);
                    break;
                case 85:
                    nUD_Nb_Stage8.Value = int.Parse(ligne);
                    break;
                case 86: nUD_Ratio8.Value = int.Parse(ligne); break;
                case 87: nUD_Vit_P8.Value = int.Parse(ligne); break;
                /*==================Pompe 9====================*/
                //Marque
                case 88: cBox_Marque9.SelectedIndex = int.Parse(ligne); break;
                case 89:
                    if ((string)cBox_Marque9.SelectedItem != ligne && cBox_Marque9.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Serie
                case 90: cBox_Serie9.SelectedIndex = int.Parse(ligne); break;
                case 91:
                    if ((string)cBox_Serie9.SelectedItem != ligne && cBox_Serie9.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Modele
                case 92: cBox_Modele9.SelectedIndex = int.Parse(ligne); break;
                case 93:
                    if ((string)cBox_Modele9.SelectedItem != ligne && cBox_Modele9.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Facteur de modification
                case 94: nUD_Nb_Pompe9.Value = int.Parse(ligne); break;
                case 95:
                    r_Btn_Parr9.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie9.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_9, r_Btn_Parr9.Checked);
                    break;
                case 96:
                    nUD_Nb_Stage9.Value = int.Parse(ligne);
                    break;
                case 97: nUD_Ratio9.Value = int.Parse(ligne); break;
                case 98: nUD_Vit_P9.Value = int.Parse(ligne); break;
                /*======================Pompe 10*/
                //Marque
                case 99: cBox_Marque10.SelectedIndex = int.Parse(ligne); break;
                case 100:
                    if ((string)cBox_Marque10.SelectedItem != ligne && cBox_Marque10.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Serie
                case 101: cBox_Serie10.SelectedIndex = int.Parse(ligne); break;
                case 102:
                    if ((string)cBox_Serie10.SelectedItem != ligne && cBox_Serie10.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Modele
                case 103: cBox_Modele10.SelectedIndex = int.Parse(ligne); break;
                case 104:
                    if ((string)cBox_Modele10.SelectedItem != ligne && cBox_Modele10.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Facteur de modification
                case 105: nUD_Nb_Pompe10.Value = int.Parse(ligne); break;
                case 106:
                    r_Btn_Parr10.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie10.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_10, r_Btn_Parr10.Checked);
                    break;
                case 107:
                    nUD_Nb_Stage10.Value = int.Parse(ligne);
                    break;
                case 108: nUD_Ratio10.Value = int.Parse(ligne); break;
                case 109: nUD_Vit_P10.Value = int.Parse(ligne); break;
                /*===================Pompe 11================*/
                //Marque
                case 110: cBox_Marque11.SelectedIndex = int.Parse(ligne); break;
                case 111:
                    if ((string)cBox_Marque11.SelectedItem != ligne && cBox_Marque11.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Serie
                case 112: cBox_Serie11.SelectedIndex = int.Parse(ligne); break;
                case 113:
                    if ((string)cBox_Serie11.SelectedItem != ligne && cBox_Serie11.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Modele
                case 114: cBox_Modele11.SelectedIndex = int.Parse(ligne); break;
                case 115:
                    if ((string)cBox_Modele11.SelectedItem != ligne && cBox_Modele11.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Facteur de modification
                case 116: nUD_Nb_Pompe11.Value = int.Parse(ligne); break;
                case 117:
                    r_Btn_Parr11.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie11.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_11, r_Btn_Parr11.Checked);
                    break;
                case 118:
                    nUD_Nb_Stage11.Value = int.Parse(ligne);
                    break;
                case 119: nUD_Ratio11.Value = int.Parse(ligne); break;
                case 120: nUD_Vit_P11.Value = int.Parse(ligne); break;
                /*===================Pompe 12====================*/
                //Marque
                case 121: cBox_Marque12.SelectedIndex = int.Parse(ligne); break;
                case 122:
                    if ((string)cBox_Marque12.SelectedItem != ligne && cBox_Marque12.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Serie
                case 123: cBox_Serie12.SelectedIndex = int.Parse(ligne); break;
                case 124:
                    if ((string)cBox_Serie12.SelectedItem != ligne && cBox_Serie12.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Modele
                case 125: cBox_Modele12.SelectedIndex = int.Parse(ligne); break;
                case 126:
                    if ((string)cBox_Modele12.SelectedItem != ligne && cBox_Modele12.SelectedIndex != k.VIDE)
                    {
                        MessageBox.Show("Erreur d'ouverture");
                    }
                    break;
                //Facteur de modification
                case 127: nUD_Nb_Pompe12.Value = int.Parse(ligne); break;
                case 128:
                    r_Btn_Parr12.Checked = (bool.Parse(ligne) == true) ? true : false;
                    r_Btn_Serie12.Checked = (bool.Parse(ligne) == true) ? false : true;
                    changement_disposition_pompe(k.POMPE_12, r_Btn_Parr12.Checked);
                    break;
                case 129:
                    nUD_Nb_Stage12.Value = int.Parse(ligne);
                    break;
                case 130: nUD_Ratio12.Value = int.Parse(ligne); break;
                case 131: nUD_Vit_P12.Value = int.Parse(ligne); break;
            }
        }

        private void ouverture_sauvegarde_tracage_pompe()
        {
            //ligne 8539

            //Pompe 1
            if (cBox_Marque1.SelectedItem != null && cBox_Modele1.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_1);

            }
            //Pompe 2
            if (cBox_Marque2.SelectedItem != null && cBox_Modele2.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_2);
            }
            //Pompe 3
            if (cBox_Marque3.SelectedItem != null && cBox_Modele3.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_3);
            }
            //Pompe 4
            if (cBox_Marque4.SelectedItem != null && cBox_Modele4.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_4);
            }
            //Pompe 5
            if (cBox_Marque5.SelectedItem != null && cBox_Modele5.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_5);
            }
            //Pompe 6
            if (cBox_Marque6.SelectedItem != null && cBox_Modele6.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_6);
            }
            //Pompe 7
            if (cBox_Marque7.SelectedItem != null && cBox_Modele7.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_7);
            }
            //Pompe 8
            if (cBox_Marque8.SelectedItem != null && cBox_Modele8.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_8);
            }
            //Pompe 9
            if (cBox_Marque9.SelectedItem != null && cBox_Modele9.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_9);
            }
            //Pompe 10
            if (cBox_Marque10.SelectedItem != null && cBox_Modele10.SelectedItem != null)
            {
                // validation_tracage_pompe(k.POMPE_10);
            }
            //Pompe 11
            if (cBox_Marque11.SelectedItem != null && cBox_Modele11.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_11);
            }
            //Pompe 12
            if (cBox_Marque12.SelectedItem != null && cBox_Modele12.SelectedItem != null)
            {
                //validation_tracage_pompe(k.POMPE_12);
            }
        }

        private void rafaichissement_tab_control()
        {
            //Ouvre et rafaichit les pages du tab_control a l'ouverture du 
            //programme, pour ameliorer la vitesse d'ouverture
            for (int i = 3; i >= 0; i--)
            {
                tab_Control.SelectedTab = tab_Control.TabPages[i];
                tab_Control.TabPages[i].Refresh();
            }
        }

        //Remise a zero et effacement de la courbe demandee
        void raz_serie(int numero_Serie)
        {
            graphique_1.Series[numero_Serie].Points.Clear();
        }

        private void raz_page_pompe()
        {
            //La fonction met tous les comboBox de la page pompe a null, donc vide
            //Efface les series du graphique et vide la liste d'items du cBox modeles
            for (int i = k.POMPE_1; i <= k.POMPE_12; i++)
            {
                effacer_serie(i, i + k.OFFSET_3, i + k.OFFSET_EFF, i + k.OFFSET_PUIS);
            }
        }

        //Update des calcul de TDH et du graphique
        private void update_resultats_et_graphique(int numero_serie)
        {
            //Declenche l'alarme pour aviser le debit nul
            clignotant();
            //Remise a zero de la courbe avant de la mettre a jour
            raz_serie(numero_serie);
            //Calcul des TDH apres changement de valeur
            calcul.TDH_section(TDH,
                                inputs.section,
                                    materiel_select,
                                        type_select,
                                            intervalles_debit,
                                                sections_actives,
                                                    inputs.acceuil.unit_pression,
                                                        inputs.acceuil.unit_longueur,
                                                            inputs.acceuil.unit_debit);
            calcul.TDH_total(TDH_tot,
                                TDH,
                                    materiel_select,
                                        type_select,
                                            intervalles_debit,
                                                sections_actives);
            //Update des resultats affiches dans le tableur
            update_tableur_TDH_total();
            //Update des courbes et allure du graphique
            update_graphique();
        }
        private void clignotant()
        {            //Declenchement de l'alarme de debit nul
            if (inputs.action.debit[k.DEBIT_AXE_X] == 0)
            {
                timer_clignotant.Enabled = true;
            }
            //Arret de l'alarme de debti nul
            if (inputs.action.debit[k.DEBIT_AXE_X] != 0 && timer_clignotant.Enabled == true)
            {
                timer_clignotant.Enabled = false;
                lbl_Debit_Nul.Visible = false;
            }
        }
        //Update des courbes et allure du graphique
        private void update_graphique()
        {
            for (int i = 0; i < 11; i++)
            {
                graphique_1.Series[0].Points.AddXY(intervalles_debit[i], TDH_tot[i]);
            }
        }
        //Activation des points sur une courbe specifique, points mis en 
        //evidence des coordonnees disponibles
        private void activer_affichage_points_graphique(int pompe)
        {
            graphique_1.Series[pompe + k.OFFSET_3].IsValueShownAsLabel = true;

            graphique_1.Series[pompe + k.OFFSET_3].MarkerStyle =
                System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
        }
        //Desactivation des points sur une courbe specifique,  retrait des 
        //points mis en evidence des coordonnees disponibles pour laisser la 
        //courbe lisse
        private void desactiver_affichage_points_graphique(int pompe)
        {
            graphique_1.Series[pompe + k.OFFSET_3].IsValueShownAsLabel = false;
            graphique_1.Series[pompe + k.OFFSET_3].MarkerStyle =
                System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
        }

        #endregion //Fonctions et Calculs

        #region Update_tableur
        //Update des tableur et de la serie de tuyauterie
        void update_tableur()
        {
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_A();
            update_tableur_section_B();
            update_tableur_section_C();
            update_tableur_section_D();
            update_tableur_section_E();
        }

        //Mise a jour des valeurs contenues dans la section GPM
        void update_tableur_section_GPM()
        {
            lbl_gpm1.Text = Math.Round(intervalles_debit[0], 0).ToString();
            lbl_gpm2.Text = Math.Round(intervalles_debit[1], 0).ToString();
            lbl_gpm3.Text = Math.Round(intervalles_debit[2], 0).ToString();
            lbl_gpm4.Text = Math.Round(intervalles_debit[3], 0).ToString();
            lbl_gpm5.Text = Math.Round(intervalles_debit[4], 0).ToString();
            lbl_gpm6.Text = Math.Round(intervalles_debit[5], 0).ToString();
            lbl_gpm7.Text = Math.Round(intervalles_debit[6], 0).ToString();
            lbl_gpm8.Text = Math.Round(intervalles_debit[7], 0).ToString();
            lbl_gpm9.Text = Math.Round(intervalles_debit[8], 0).ToString();
            lbl_gpm10.Text = Math.Round(intervalles_debit[9], 0).ToString();
            lbl_gpm11.Text = Math.Round(intervalles_debit[10], 0).ToString();
        }
        //Mise a jour des valeurs contenues dans le tableur, Section A
        void update_tableur_section_A()
        {
            lbl_A_1.Text = Math.Round(TDH[k.SECTION_A, 0], 1).ToString();
            lbl_A_2.Text = Math.Round(TDH[k.SECTION_A, 1], 1).ToString();
            lbl_A_3.Text = Math.Round(TDH[k.SECTION_A, 2], 1).ToString();
            lbl_A_4.Text = Math.Round(TDH[k.SECTION_A, 3], 1).ToString();
            lbl_A_5.Text = Math.Round(TDH[k.SECTION_A, 4], 1).ToString();
            lbl_A_6.Text = Math.Round(TDH[k.SECTION_A, 5], 1).ToString();
            lbl_A_7.Text = Math.Round(TDH[k.SECTION_A, 6], 1).ToString();
            lbl_A_8.Text = Math.Round(TDH[k.SECTION_A, 7], 1).ToString();
            lbl_A_9.Text = Math.Round(TDH[k.SECTION_A, 8], 1).ToString();
            lbl_A_10.Text = Math.Round(TDH[k.SECTION_A, 9], 1).ToString();
            lbl_A_11.Text = Math.Round(TDH[k.SECTION_A, 10], 1).ToString();
        }
        //Mise a jour des valeurs contenues dans le tableur, Section B
        void update_tableur_section_B()
        {
            lbl_B_1.Text = Math.Round(TDH[k.SECTION_B, 0], 1).ToString();
            lbl_B_2.Text = Math.Round(TDH[k.SECTION_B, 1], 1).ToString();
            lbl_B_3.Text = Math.Round(TDH[k.SECTION_B, 2], 1).ToString();
            lbl_B_4.Text = Math.Round(TDH[k.SECTION_B, 3], 1).ToString();
            lbl_B_5.Text = Math.Round(TDH[k.SECTION_B, 4], 1).ToString();
            lbl_B_6.Text = Math.Round(TDH[k.SECTION_B, 5], 1).ToString();
            lbl_B_7.Text = Math.Round(TDH[k.SECTION_B, 6], 1).ToString();
            lbl_B_8.Text = Math.Round(TDH[k.SECTION_B, 7], 1).ToString();
            lbl_B_9.Text = Math.Round(TDH[k.SECTION_B, 8], 1).ToString();
            lbl_B_10.Text = Math.Round(TDH[k.SECTION_B, 9], 1).ToString();
            lbl_B_11.Text = Math.Round(TDH[k.SECTION_B, 10], 1).ToString();
        }
        //Mise a jour des valeurs contenues dans le tableur, Section C
        void update_tableur_section_C()
        {
            lbl_C_1.Text = Math.Round(TDH[k.SECTION_C, 0], 1).ToString();
            lbl_C_2.Text = Math.Round(TDH[k.SECTION_C, 1], 1).ToString();
            lbl_C_3.Text = Math.Round(TDH[k.SECTION_C, 2], 1).ToString();
            lbl_C_4.Text = Math.Round(TDH[k.SECTION_C, 3], 1).ToString();
            lbl_C_5.Text = Math.Round(TDH[k.SECTION_C, 4], 1).ToString();
            lbl_C_6.Text = Math.Round(TDH[k.SECTION_C, 5], 1).ToString();
            lbl_C_7.Text = Math.Round(TDH[k.SECTION_C, 6], 1).ToString();
            lbl_C_8.Text = Math.Round(TDH[k.SECTION_C, 7], 1).ToString();
            lbl_C_9.Text = Math.Round(TDH[k.SECTION_C, 8], 1).ToString();
            lbl_C_10.Text = Math.Round(TDH[k.SECTION_C, 9], 1).ToString();
            lbl_C_11.Text = Math.Round(TDH[k.SECTION_C, 10], 1).ToString();
        }
        //Mise a jour des valeurs contenues dans le tableur, Section D
        void update_tableur_section_D()
        {
            lbl_D_1.Text = Math.Round(TDH[k.SECTION_D, 0]).ToString();
            lbl_D_2.Text = Math.Round(TDH[k.SECTION_D, 1]).ToString();
            lbl_D_3.Text = Math.Round(TDH[k.SECTION_D, 2]).ToString();
            lbl_D_4.Text = Math.Round(TDH[k.SECTION_D, 3]).ToString();
            lbl_D_5.Text = Math.Round(TDH[k.SECTION_D, 4]).ToString();
            lbl_D_6.Text = Math.Round(TDH[k.SECTION_D, 5]).ToString();
            lbl_D_7.Text = Math.Round(TDH[k.SECTION_D, 6]).ToString();
            lbl_D_8.Text = Math.Round(TDH[k.SECTION_D, 7]).ToString();
            lbl_D_9.Text = Math.Round(TDH[k.SECTION_D, 8]).ToString();
            lbl_D_10.Text = Math.Round(TDH[k.SECTION_D, 9]).ToString();
            lbl_D_11.Text = Math.Round(TDH[k.SECTION_D, 10], 1).ToString();
        }
        //Mise a jour des valeurs contenues dans le tableur, Section E
        void update_tableur_section_E()
        {
            lbl_E_1.Text = Math.Round(TDH[k.SECTION_E, 0]).ToString();
            lbl_E_2.Text = Math.Round(TDH[k.SECTION_E, 1]).ToString();
            lbl_E_3.Text = Math.Round(TDH[k.SECTION_E, 2]).ToString();
            lbl_E_4.Text = Math.Round(TDH[k.SECTION_E, 3]).ToString();
            lbl_E_5.Text = Math.Round(TDH[k.SECTION_E, 4]).ToString();
            lbl_E_6.Text = Math.Round(TDH[k.SECTION_E, 5]).ToString();
            lbl_E_7.Text = Math.Round(TDH[k.SECTION_E, 6]).ToString();
            lbl_E_8.Text = Math.Round(TDH[k.SECTION_E, 7]).ToString();
            lbl_E_9.Text = Math.Round(TDH[k.SECTION_E, 8]).ToString();
            lbl_E_10.Text = Math.Round(TDH[k.SECTION_E, 9]).ToString();
            lbl_E_11.Text = Math.Round(TDH[k.SECTION_E, 10], 1).ToString();
        }
        //Mise a jour des valerus contenues dans le tableur, TDH total
        void update_tableur_TDH_total()
        {
            lbl_tot_1.Text = Math.Round(TDH_tot[0]).ToString();
            lbl_tot_2.Text = Math.Round(TDH_tot[1]).ToString();
            lbl_tot_3.Text = Math.Round(TDH_tot[2]).ToString();
            lbl_tot_4.Text = Math.Round(TDH_tot[3]).ToString();
            lbl_tot_5.Text = Math.Round(TDH_tot[4]).ToString();
            lbl_tot_6.Text = Math.Round(TDH_tot[5]).ToString();
            lbl_tot_7.Text = Math.Round(TDH_tot[6]).ToString();
            lbl_tot_8.Text = Math.Round(TDH_tot[7]).ToString();
            lbl_tot_9.Text = Math.Round(TDH_tot[8]).ToString();
            lbl_tot_10.Text = Math.Round(TDH_tot[9]).ToString();
            lbl_tot_11.Text = Math.Round(TDH_tot[10]).ToString();
        }
        #endregion //Update_tableur

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            PaperSize toto = printDocument1.PrinterSettings.PaperSizes[0];
            e.Graphics.DrawImage(bmp, 20, 40, toto.Height - 80, toto.Width - 80);

            e.Graphics.Save();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void timer_clignotant_Tick(object sender, EventArgs e)
        {
            if (lbl_Debit_Nul.Visible == true)
            {
                lbl_Debit_Nul.Visible = false;
            }
            else
            {
                lbl_Debit_Nul.Visible = true;
            }
        }


        private void btn_imprimer_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            ajustement_graphique_fullscreen();

            imprimer();
        }


        private void imprimer()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Save As PDF";
            sfd.Filter = "(*.pdf)|*.pdf";
            sfd.InitialDirectory = @"C:\";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (checkBox_Curseur_Auto.Checked)
                {
                    retirer_curseur();

                    capture_ecran = true;
                }

                sfd_name = sfd.FileName;

                timer_print.Enabled = true;
            }
        }


        private void timer_print_Tick(object sender, EventArgs e)
        {
            timer_print.Enabled = false;


            int X = this.Location.X;

            Graphics g = graphique_1.CreateGraphics();
            bmp = new Bitmap(graphique_1.Size.Width - 60, graphique_1.Size.Height - 20, g);
            Graphics mg = Graphics.FromImage(bmp);

            mg.CopyFromScreen(X + 60,
                                (-(Screen.PrimaryScreen.WorkingArea.Height -
                                Screen.PrimaryScreen.Bounds.Height) / 2) +
                                10,
                                    0,
                                        0,
                                            graphique_1.Size);

            using (var document = new PdfSharp.Pdf.PdfDocument())
            {
                PdfSharp.Pdf.PdfPage page = document.AddPage();

                page.Orientation = PageOrientation.Landscape;

                using (PdfSharp.Drawing.XImage img =
                                PdfSharp.Drawing.XImage.FromGdiPlusImage(bmp))
                {
                    PdfSharp.Drawing.XGraphics gfx =
                                PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                    gfx.DrawImage(img, -10, 0, page.Width, page.Height);
                }

                document.Save(String.Concat(sfd_name, ".pdf"));
            }

            System.Diagnostics.Process.Start(String.Concat(sfd_name, ".pdf"));

            capture_ecran = false;

            if (checkBox_Curseur_Auto.Checked)
            {
                activer_curseur();
            }
        }

        public void printPDFWithAcrobat()
        {
            string Filepath = @"C:\Users\sdkca\Desktop\path-to-your-pdf\pdf-sample.pdf";

            using (PrintDialog Dialog = new PrintDialog())
            {
                Dialog.ShowDialog();

                ProcessStartInfo printProcessInfo = new ProcessStartInfo()
                {
                    Verb = "print",
                    CreateNoWindow = true,
                    FileName = Filepath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process printProcess = new Process();
                printProcess.StartInfo = printProcessInfo;
                printProcess.Start();

                printProcess.WaitForInputIdle();

                Thread.Sleep(3000);

                if (false == printProcess.CloseMainWindow())
                {
                    printProcess.Kill();
                }
            }
        }
        //Les comboBox contenant les type de tuyaux
        /*La fonction obtention_pipe_type dans le module Pipes remplie un tableau
        liste_pipes qui est utilise pour remplir les cBox ligne par ligne selon
        les choix de selection dans la liste*/
        void remplissage_cBox_Pipes(int nb)
        {
            string[] liste_pipes = Pipes.obtention_pipe_type();

            for (int i = 0; i < liste_pipes.Length; i++)
            {
                cBox_Type1.Items.Add(liste_pipes[i]);
                cBox_Type2.Items.Add(liste_pipes[i]);
                cBox_Type3.Items.Add(liste_pipes[i]);
                cBox_Type4.Items.Add(liste_pipes[i]);
                cBox_Type5.Items.Add(liste_pipes[i]);
            }
        }

        void remplissage_cBox_Materiel(int nb)
        {
            string[] liste_materiel = Materiel.obtention_materiel();

            for (int i = 0; i < liste_materiel.Length; i++)
            {
                cBox_materiel1.Items.Add(liste_materiel[i]);
                cBox_materiel2.Items.Add(liste_materiel[i]);
                cBox_materiel3.Items.Add(liste_materiel[i]);
                cBox_materiel4.Items.Add(liste_materiel[i]);
                cBox_materiel5.Items.Add(liste_materiel[i]);
            }
        }

        void reset_cBox_Materiel(int nb, int no_section)
        {
            string[] liste_materiel = Materiel.obtention_materiel();

            switch (no_section)
            {
                case k.SECTION_A:
                    cBox_materiel1.SelectedIndex = k.VIDE;
                    cBox_materiel1.Items.Clear();
                    for (int i = 0; i < liste_materiel.Length; i++)
                    {
                        cBox_materiel1.Items.Add(liste_materiel[i]);
                    }
                    break;
                case k.SECTION_B:
                    cBox_materiel2.SelectedIndex = k.VIDE;
                    cBox_materiel2.Items.Clear();
                    for (int i = 0; i < liste_materiel.Length; i++)
                    {
                        cBox_materiel2.Items.Add(liste_materiel[i]);
                    }

                    break;
                case k.SECTION_C:
                    cBox_materiel3.SelectedIndex = k.VIDE;
                    cBox_materiel3.Items.Clear();
                    for (int i = 0; i < liste_materiel.Length; i++)
                    {
                        cBox_materiel3.Items.Add(liste_materiel[i]);
                    }
                    break;
                case k.SECTION_D:
                    cBox_materiel4.SelectedIndex = k.VIDE;
                    cBox_materiel4.Items.Clear();
                    for (int i = 0; i < liste_materiel.Length; i++)
                    {
                        cBox_materiel4.Items.Add(liste_materiel[i]);
                    }
                    break;
                case k.SECTION_E:
                    cBox_materiel5.SelectedIndex = k.VIDE;
                    cBox_materiel5.Items.Clear();
                    for (int i = 0; i < liste_materiel.Length; i++)
                    {
                        cBox_materiel5.Items.Add(liste_materiel[i]);
                    }
                    break;
            }
        }

        //Obtention des marques selon le module brands
        void remplissage_cBox_Marques(int nb_marques)
        {
            string[] liste_marques = brands.obtention_marques();

            for (int i = 0; i < liste_marques.Length; i++)
            {
                cBox_Marque1.Items.Add(liste_marques[i]);
                cBox_Marque2.Items.Add(liste_marques[i]);
                cBox_Marque3.Items.Add(liste_marques[i]);
                cBox_Marque4.Items.Add(liste_marques[i]);
                cBox_Marque5.Items.Add(liste_marques[i]);
                cBox_Marque6.Items.Add(liste_marques[i]);
                cBox_Marque7.Items.Add(liste_marques[i]);
                cBox_Marque8.Items.Add(liste_marques[i]);
                cBox_Marque9.Items.Add(liste_marques[i]);
                cBox_Marque10.Items.Add(liste_marques[i]);
                cBox_Marque11.Items.Add(liste_marques[i]);
                cBox_Marque12.Items.Add(liste_marques[i]);
            }
        }

        void reset_cBox_Pipe(int nb, int no_section)
        {
            string[] liste_pipe = Pipes.obtention_pipe_type();

            switch (no_section)
            {
                case k.SECTION_A:
                    cBox_Type1.SelectedIndex = k.VIDE;
                    cBox_Type1.Items.Clear();
                    for (int i = 0; i < liste_pipe.Length; i++)
                    {
                        cBox_Type1.Items.Add(liste_pipe[i]);
                    }
                    break;
                case k.SECTION_B:
                    cBox_Type2.SelectedIndex = k.VIDE;
                    cBox_Type2.Items.Clear();
                    for (int i = 0; i < liste_pipe.Length; i++)
                    {
                        cBox_Type2.Items.Add(liste_pipe[i]);
                    }
                    break;
                case k.SECTION_C:
                    cBox_Type3.SelectedIndex = k.VIDE;
                    cBox_Type3.Items.Clear();
                    for (int i = 0; i < liste_pipe.Length; i++)
                    {
                        cBox_Type3.Items.Add(liste_pipe[i]);
                    }
                    break;
                case k.SECTION_D:
                    cBox_Type4.SelectedIndex = k.VIDE;
                    cBox_Type4.Items.Clear();
                    for (int i = 0; i < liste_pipe.Length; i++)
                    {
                        cBox_Type4.Items.Add(liste_pipe[i]);
                    }
                    break;
                case k.SECTION_E:
                    cBox_Type5.SelectedIndex = k.VIDE;
                    cBox_Type5.Items.Clear();
                    for (int i = 0; i < liste_pipe.Length; i++)
                    {
                        cBox_Type5.Items.Add(liste_pipe[i]);
                    }
                    break;
            }
        }

        //Obtention des series disponible pour chaque marque selon le module serie
        void remplissage_cBox_Serie(int no_marque, int num_pompe)
        {
            //desactivation_bouton_validation(num_pompe);

            string[] liste_serie = serie.obtention_serie(no_marque);

            if (inputs.pompes[num_pompe].marque != null)
            {
                for (int j = 0; j < liste_serie.Length; j++)
                {
                    switch (num_pompe)
                    {
                        case k.POMPE_1: cBox_Serie1.Items.Add(liste_serie[j]); break;
                        case k.POMPE_2: cBox_Serie2.Items.Add(liste_serie[j]); break;
                        case k.POMPE_3: cBox_Serie3.Items.Add(liste_serie[j]); break;
                        case k.POMPE_4: cBox_Serie4.Items.Add(liste_serie[j]); break;
                        case k.POMPE_5: cBox_Serie5.Items.Add(liste_serie[j]); break;
                        case k.POMPE_6: cBox_Serie6.Items.Add(liste_serie[j]); break;
                        case k.POMPE_7: cBox_Serie7.Items.Add(liste_serie[j]); break;
                        case k.POMPE_8: cBox_Serie8.Items.Add(liste_serie[j]); break;
                        case k.POMPE_9: cBox_Serie9.Items.Add(liste_serie[j]); break;
                        case k.POMPE_10: cBox_Serie10.Items.Add(liste_serie[j]); break;
                        case k.POMPE_11: cBox_Serie11.Items.Add(liste_serie[j]); break;
                        case k.POMPE_12: cBox_Serie12.Items.Add(liste_serie[j]); break;
                    }
                }
                switch (num_pompe)
                {
                    case k.POMPE_1: cBox_Serie1.SelectedIndex = 0; break;
                    case k.POMPE_2: cBox_Serie2.SelectedIndex = 0; break;
                    case k.POMPE_3: cBox_Serie3.SelectedIndex = 0; break;
                    case k.POMPE_4: cBox_Serie4.SelectedIndex = 0; break;
                    case k.POMPE_5: cBox_Serie5.SelectedIndex = 0; break;
                    case k.POMPE_6: cBox_Serie6.SelectedIndex = 0; break;
                    case k.POMPE_7: cBox_Serie7.SelectedIndex = 0; break;
                    case k.POMPE_8: cBox_Serie8.SelectedIndex = 0; break;
                    case k.POMPE_9: cBox_Serie9.SelectedIndex = 0; break;
                    case k.POMPE_10: cBox_Serie10.SelectedIndex = 0; break;
                    case k.POMPE_11: cBox_Serie11.SelectedIndex = 0; break;
                    case k.POMPE_12: cBox_Serie12.SelectedIndex = 0; break;
                }
            }
        }

        //Obtention des Modeles pour chaque marques et series disponible selon 
        //le module modele
        void remplissage_cBox_Modeles(int nb_marques, int serie, int num_pompe)
        {
            //desactivation_bouton_validation(num_pompe);

            string[] liste_modeles = modele.obtention_modeles(nb_marques, serie);

            if (inputs.pompes[num_pompe].marque != null &&
                    inputs.pompes[num_pompe].serie != null)
            {
                for (int j = 0; j < liste_modeles.Length; j++)
                {
                    switch (num_pompe)
                    {
                        case k.POMPE_1: cBox_Modele1.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_2: cBox_Modele2.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_3: cBox_Modele3.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_4: cBox_Modele4.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_5: cBox_Modele5.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_6: cBox_Modele6.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_7: cBox_Modele7.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_8: cBox_Modele8.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_9: cBox_Modele9.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_10: cBox_Modele10.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_11: cBox_Modele11.Items.Add(liste_modeles[j]); break;
                        case k.POMPE_12: cBox_Modele12.Items.Add(liste_modeles[j]); break;
                    }
                }
                switch (num_pompe)
                {
                    case k.POMPE_1: cBox_Modele1.SelectedIndex = 0; break;
                    case k.POMPE_2: cBox_Modele2.SelectedIndex = 0; break;
                    case k.POMPE_3: cBox_Modele3.SelectedIndex = 0; break;
                    case k.POMPE_4: cBox_Modele4.SelectedIndex = 0; break;
                    case k.POMPE_5: cBox_Modele5.SelectedIndex = 0; break;
                    case k.POMPE_6: cBox_Modele6.SelectedIndex = 0; break;
                    case k.POMPE_7: cBox_Modele7.SelectedIndex = 0; break;
                    case k.POMPE_8: cBox_Modele8.SelectedIndex = 0; break;
                    case k.POMPE_9: cBox_Modele9.SelectedIndex = 0; break;
                    case k.POMPE_10: cBox_Modele10.SelectedIndex = 0; break;
                    case k.POMPE_11: cBox_Modele11.SelectedIndex = 0; break;
                    case k.POMPE_12: cBox_Modele12.SelectedIndex = 0; break;
                }
            }
        }

        void info_pompe(int no_marque,
                            int no_serie,
                                int no_modele,
                                    int num_pompe,
                                        double TDH_tot)
        {
            double point_max;

            //Obtention des parametres de l'equations de la courbe de pompe
            double A = matrice_formule[no_marque, no_serie, no_modele, F.A];
            double B = matrice_formule[no_marque, no_serie, no_modele, F.B];
            double C = matrice_formule[no_marque, no_serie, no_modele, F.C];
            double D = matrice_formule[no_marque, no_serie, no_modele, F.D];
            double E = matrice_formule[no_marque, no_serie, no_modele, F.E];
            double MAX = matrice_formule[no_marque, no_serie, no_modele, F.MAX];
            double vitesse_max = matrice_formule[no_marque, no_serie, no_modele, F.VITESSE];
            double rA = matrice_formule[no_marque, no_serie, no_modele, F.rA];
            double rB = matrice_formule[no_marque, no_serie, no_modele, F.rB];
            double rC = matrice_formule[no_marque, no_serie, no_modele, F.rC];
            double rD = matrice_formule[no_marque, no_serie, no_modele, F.rD];
            double rE = matrice_formule[no_marque, no_serie, no_modele, F.rE];
            double rMIN = matrice_formule[no_marque, no_serie, no_modele, F.rMIN];
            double pA = matrice_formule[no_marque, no_serie, no_modele, F.pA];
            double pB = matrice_formule[no_marque, no_serie, no_modele, F.pB];
            double pC = matrice_formule[no_marque, no_serie, no_modele, F.pC];
            double pD = matrice_formule[no_marque, no_serie, no_modele, F.pD];
            double pE = matrice_formule[no_marque, no_serie, no_modele, F.pE];
            //Determination du numero de courbe selon la pompe
            int no_courbe = num_pompe + k.OFFSET_3;
            ////VariableS Y, une seule equation aurait suffit mais cette methode
            ////facilite la lecture du code et le deboguage
            double valeur_Y;

            double valeur_X;

            //Calcul des intervalles pour avoir 12 points minimalement de trace
            //sur le graphique
            double intervalles_pompe = (double)(MAX / 12);

            double intervalles_efficacite = (double)((MAX - rMIN) / 12);
            //iterateur
            int i = 0;
            //Effacement de la courbe presente pour la remplacer par la nouvelle
            raz_serie(no_courbe);
            //Activation de la serie au graphique
            graphique_1.Series[no_courbe].Enabled = true;

            //Calcul de chaque point d'intervalle calcule plus haut pour tracer
            //le graphique de ces points
            for (double x = 0; x <= MAX + 1; x += intervalles_pompe)
            {
                /*==============Calcul du Y==================================*/
                //Premiere variable etant le calcul par l'equation de forme
                //polynomemiale
                valeur_Y = Y_polynome_pompe(x, A, B, C, D, E, vitesse_max, num_pompe);

                valeur_X = X_polynome_pompe(num_pompe, x, vitesse_max);

                //Ajout du point calcule, X et Y pondere au numero de courbe voulue
                //Passage de l'iterateur i pour suivre dans quelle case du listview
                //Le programme est rendu a ecrire
                tracage_courbe_pompe(valeur_X, valeur_Y, num_pompe, i, no_courbe);
                //Incrementation de l'iterateur des case du listview
                i++;
            }

            if (rD != 0)
            {
                //Effacement de la courbe presente pour la remplacer par la nouvelle
                raz_serie(num_pompe + k.OFFSET_EFF);

                for (double x = rMIN; x <= MAX + 1; x += intervalles_efficacite)
                {
                    valeur_Y = Y_polynome_efficacite(x, rA, rB, rC, rD, rE, vitesse_max, num_pompe);

                    valeur_X = X_polynome_pompe(num_pompe, x, vitesse_max);

                    tracage_courbe_efficacite(valeur_X, valeur_Y, num_pompe);
                }
            }

            if (pD != 0)
            {
                //Effacement de la courbe presente pour la remplacer par la nouvelle

                raz_serie(num_pompe + k.OFFSET_PUIS);

                for (double x = rMIN; x <= MAX + 1; x += intervalles_efficacite)
                {
                    valeur_Y = Y_polynome_puissance(x, pA, pB, pC, pD, pE, vitesse_max, num_pompe);

                    valeur_X = X_polynome_puissance(num_pompe, x, vitesse_max);

                    tracage_courbe_puissance(valeur_X, valeur_Y, num_pompe);
                }
            }

            ////Si un TDH est calcule et plus grand que 0
            //if (TDH_tot > 0)
            //{
            //    point_max = X_polynome_pompe(num_pompe, MAX, vitesse_max);
            //    //Si les courbes se croisent
            //    if (calcul.TDH_en_un_point(
            //            point_max,
            //                inputs.section,
            //                    inputs.acceuil.unit_pression,
            //                        inputs.acceuil.unit_longueur,
            //                            inputs.acceuil.unit_debit)
            //                                >
            //        Y_polynome_pompe(MAX, A, B, C, D, E, vitesse_max, num_pompe))
            //    {
            //        //Recherche du point de jonctions des courbes systeme et de la pompe
            //        recherche_jonction_courbes_polynome_4(
            //            A, B, C, D, E, MAX, vitesse_max, num_pompe);
            //    }
            //    else
            //    {
            //        switch (num_pompe)
            //        {
            //            case k.POMPE_1: lbl_X_jonc1.Text = "Ø";
            //                lbl_Y_jonc1.Text = "Ø";
            //                break;

            //        }
            //    }
            //}
            // }

            activer_legende(num_pompe + k.OFFSET_3);
        }

        private double Y_disposition_pompes(int num_pompe, double valeur_Y)
        {
            //Si les pompes sont en serie
            if (inputs.pompes[num_pompe].disposition == k.DISPO_SERIE)
            {
                //Multiplication de la valeur Y passee par le nombre de pompe
                valeur_Y = Math.Round(valeur_Y * inputs.pompes[num_pompe].nb_pompe, 3);
            }
            return valeur_Y;
        }

        private double Y_nb_stages(int num_pompe, double valeur_Y_dispo)
        {
            //Multiplication de la valeur Y passe par le nombre de stages
            return valeur_Y_dispo * inputs.pompes[num_pompe].nb_stages;
        }

        private double Y_ratio(int num_pompe, double Y_nb_stages)
        {
            //Multiplication de la valeur Y passee par le ratio mis en centieme au carre
            //50%  -  0.5 au carre
            return Y_nb_stages * Math.Pow(inputs.pompes[num_pompe].ratio_diametre, 2);
        }

        private double Y_vitesse(int num_pompe, double Y_ratio, double vitesse_max)
        {
            return Y_ratio * (Math.Pow(inputs.pompes[num_pompe].vitesse / vitesse_max, 2));
        }

        private double Y_unite(double Y_vitesse)
        {
            return Y_vitesse * inputs.acceuil.unit_pression;
        }


        //Division de la valeur de puissance divisee par la nombre de pompe, peu
        //importe la disposition
        private double Y_disposition_puissance(int num_pompe, double valeur_Y)
        {
            return valeur_Y / inputs.pompes[num_pompe].nb_pompe;
        }
        //Multiplication de la puissance par le nombre de stages
        private double Y_nb_stages_puissance(int num_pompe, double valeur_Y_dispo)
        {
            return valeur_Y_dispo * inputs.pompes[num_pompe].nb_stages;
        }
        //Le ratio du diametre de l'impulseur et mis a la puissance 5 avant d'etre
        //multiplie par la puissance theorique
        private double Y_ratio_puissance(int num_pompe, double valeur_Y_stages)
        {
            return valeur_Y_stages * Math.Pow(inputs.pompes[num_pompe].ratio_diametre, 5);
        }
        //Le ratio de la vitesse par rapport a la vitesse theorique est mis au
        //cube avant d'etre multiplie par la puissance
        private double Y_vitesse_puissance(int num_pompe, double valeur_Y_ratio, double vitesse_max)
        {
            return valeur_Y_ratio * Math.Pow((inputs.pompes[num_pompe].vitesse / vitesse_max), 3);
        }

        private double Y_unite_puissance(double Y_vitesse)
        {
            return Y_vitesse * inputs.acceuil.unit_puissance;
        }

        private double X_ratio_puissance(int num_pompe, double valeur_X)
        {
            double valeur_X_ratio;

            valeur_X_ratio = valeur_X * Math.Pow(inputs.pompes[num_pompe].ratio_diametre, 3);

            return valeur_X_ratio;
        }

        private double X_vitesse_puissance(int num_pompe, double valeur_X_ratio, double vitesse_max)
        {
            return valeur_X_ratio * (inputs.pompes[num_pompe].vitesse / vitesse_max);
        }

        private double X_disposition_pompe(int num_pompe, double valeur_X)
        {
            if (inputs.pompes[num_pompe].disposition == k.DISPO_PARR)
            {
                valeur_X = valeur_X * inputs.pompes[num_pompe].nb_pompe;
            }
            return valeur_X;
        }

        private double X_ratio(int num_pompe, double valeur_X_dispo)
        {
            return valeur_X_dispo * Math.Pow(inputs.pompes[num_pompe].ratio_diametre, 3);
        }

        private double X_vitesse(int num_pompe, double X_ratio, double vitesse_max)
        {
            return X_ratio * (inputs.pompes[num_pompe].vitesse / vitesse_max);
        }

        private double X_unite(int num_pompe, double valeur_X_vitesse)
        {
            return valeur_X_vitesse / inputs.acceuil.unit_debit;
        }

        private void recherche_jonction_courbes_polynome_4(
                                    double A,
                                        double B,
                                            double C,
                                                double D,
                                                    double E,
                                                        double MAX,
                                                            double vitesse_max,
                                                                int num_pompe)
        {
            double delta = 100;
            double xd = MAX;

            double xg = 0;
            double x_demi = (xd - xg) / 2;

            while (delta > 2 || delta < -2)
            {
                if (inputs.pompes[num_pompe].disposition == k.DISPO_PARR)
                {
                    delta = Y_polynome_pompe(x_demi, A, B, C, D, E, vitesse_max, num_pompe) - calcul.TDH_en_un_point(x_demi * inputs.pompes[num_pompe].nb_pompe, inputs.section, inputs.acceuil.unit_pression, inputs.acceuil.unit_longueur, inputs.acceuil.unit_debit);

                    //Positif, donc jonction a droite
                    if (delta > 2)
                    {
                        xg = x_demi;
                    }
                    else if (delta < -2)
                    {
                        xd = x_demi;
                    }
                    x_demi = ((xd - xg) / 2) + xg;
                }
                else if (inputs.pompes[num_pompe].disposition == k.DISPO_SERIE)
                {
                    delta = Y_polynome_pompe(x_demi, A, B, C, D, E, vitesse_max, num_pompe) - calcul.TDH_en_un_point(x_demi, inputs.section, inputs.acceuil.unit_pression, inputs.acceuil.unit_longueur, inputs.acceuil.unit_debit);

                    //Positif, donc jonction a droite
                    if (delta > 2)
                    {
                        xg = x_demi;
                    }
                    else if (delta < -2)
                    {
                        xd = x_demi;
                    }
                    x_demi = ((xd - xg) / 2) + xg;
                }
            }

            switch (num_pompe)
            {
                case k.POMPE_1:
                    lbl_X_jonc1.Text =
            //X_polynome_pompe(num_pompe, x_demi, vitesse_max).ToString();
            x_demi.ToString();
                    lbl_Y_jonc1.Text = Y_polynome_pompe(x_demi, A, B, C, D, E, vitesse_max, num_pompe).ToString();
                    break;

            }
        }

        private void tracage_courbe_pompe(
                                        double valeur_X,
                                            double valeur_Y,
                                                    int num_pompe,
                                                        int i,
                                                            int no_courbe)
        {
            graphique_1.Series[no_courbe].Points.AddXY(valeur_X, valeur_Y);
            affichage_valeurs(num_pompe, i, valeur_X, valeur_Y);
        }

        private void tracage_courbe_efficacite(
                                double valeur_X,
                                    double valeur_Y,
                                            int num_pompe)
        {
            graphique_1.Series[num_pompe + k.OFFSET_EFF].Points.AddXY(valeur_X, valeur_Y);
        }

        private void tracage_courbe_puissance(
                                double valeur_X,
                                    double valeur_Y,
                                            int num_pompe)
        {
            graphique_1.Series[num_pompe + k.OFFSET_PUIS].Points.AddXY(valeur_X, valeur_Y);
        }

        private double X_converti_unite(double x, int num_pompe, double vitesse_max)
        {
            double valeur_X = 0;

            if (inputs.pompes[num_pompe].disposition == k.DISPO_SERIE)
            {
                valeur_X = Math.Round((x / inputs.acceuil.unit_debit) * (inputs.pompes[num_pompe].vitesse / vitesse_max), 3);
            }
            else if (inputs.pompes[num_pompe].disposition == k.DISPO_PARR)
            {
                valeur_X = Math.Round(((x * inputs.pompes[num_pompe].nb_pompe) / inputs.acceuil.unit_debit) * (inputs.pompes[num_pompe].vitesse / vitesse_max), 3);
            }

            return valeur_X;
        }

        private double Y_polynome_pompe(
                                    double x,
                                        double A,
                                            double B,
                                                double C,
                                                    double D,
                                                        double E,
                                                            double vitesse_max,
                                                                int num_pompe)
        {
            double valeur_Y = 0;
            //VariableS Y, une seule equation aurait suffit mais cette methode
            //facilite la lecture du code et le deboguage
            double valeur_Y_dispo;
            double valeur_Y_stages;
            double valeur_Y_ratio;
            double valeur_Y_unite;
            double valeur_Y_vitesse;
            double valeur_Y_pondere;


            //Calcul de la valeur Y selon les parametres de l'equation
            valeur_Y = (Math.Pow(x, 4) * A) +
                (Math.Pow(x, 3) * B) +
                    (Math.Pow(x, 2) * C) +
                        (x * D) +
                            E;

            //Modification du Y selon la disposition et le nombre de pompe
            //multiplication par le nombre de pompe si placees en seriee
            valeur_Y_dispo = Y_disposition_pompes(num_pompe, valeur_Y);
            //Modification du Y selon le nombre de stage, multiplication par
            //le nombre de stages
            valeur_Y_stages = Y_nb_stages(num_pompe, valeur_Y_dispo);
            //Modification du Y selon le ratio du diametre de l'impeller
            //Multiplication par le (pourcentage mis en centieme au carre)
            valeur_Y_ratio = Y_ratio(num_pompe, valeur_Y_stages);
            //Modification du Y selon la vitesse modifie du la pompe
            //Y * (Vitesse Modifiee / Vitesse Max) au carre
            valeur_Y_vitesse = Y_vitesse(num_pompe, valeur_Y_ratio, vitesse_max);
            //Modification du Y selon les unites choisies
            //Selon l'unite de pression selectionnee
            valeur_Y_unite = Y_unite(valeur_Y_vitesse);
            //Valeur ajoutee pour visualiser la fin de la ponderation du Y
            //Copie inutile autre que pour la lecture du code
            valeur_Y_pondere = valeur_Y_unite;

            //Retour de la valeur Y
            return valeur_Y_pondere;
        }


        private double Y_polynome_puissance(
                            double x,
                                double A,
                                    double B,
                                        double C,
                                            double D,
                                                double E,
                                                    double vitesse_max,
                                                        int num_pompe)
        {
            double valeur_Y = 0;
            //VariableS Y, une seule equation aurait suffit mais cette methode
            //facilite la lecture du code et le deboguage
            double valeur_Y_dispo;
            double valeur_Y_stages;
            double valeur_Y_ratio;
            double valeur_Y_unite;
            double valeur_Y_vitesse;
            double valeur_Y_pondere;


            //Calcul de la valeur Y selon les parametres de l'equation
            valeur_Y = (Math.Pow(x, 4) * A) +
                (Math.Pow(x, 3) * B) +
                    (Math.Pow(x, 2) * C) +
                        (x * D) +
                            E;

            //Modification du Y selon la disposition et le nombre de pompe
            //multiplication par le nombre de pompe si placees en seriee
            valeur_Y_dispo = Y_disposition_puissance(num_pompe, valeur_Y);
            //Modification du Y selon le nombre de stage, multiplication par
            //le nombre de stages
            valeur_Y_stages = Y_nb_stages_puissance(num_pompe, valeur_Y_dispo);
            //Modification du Y selon le ratio du diametre de l'impeller
            //Multiplication par le (pourcentage mis en centieme au carre)
            valeur_Y_ratio = Y_ratio_puissance(num_pompe, valeur_Y_stages);
            //Modification du Y selon la vitesse modifie du la pompe
            //Y * (Vitesse Modifiee / Vitesse Max) au carre
            valeur_Y_vitesse = Y_vitesse_puissance(num_pompe, valeur_Y_ratio, vitesse_max);
            //Modification du Y selon les unites choisies
            //Selon l'unite de pression selectionnee

            valeur_Y_unite = Y_unite_puissance(valeur_Y_vitesse);

            return valeur_Y_unite;
        }

        private double Y_polynome_efficacite(
                    double x,
                        double A,
                            double B,
                                double C,
                                    double D,
                                        double E,
                                            double vitesse_max,
                                                int num_pompe)
        {
            double valeur_Y = 0;
            //VariableS Y, une seule equation aurait suffit mais cette methode
            //facilite la lecture du code et le deboguage

            double valeur_Y_ratio;

            double valeur_Y_vitesse;

            //Calcul de la valeur Y selon les parametres de l'equation
            valeur_Y = (Math.Pow(x, 4) * A) +
                (Math.Pow(x, 3) * B) +
                    (Math.Pow(x, 2) * C) +
                        (x * D) +
                            E;


            //Modification du Y selon la disposition et le nombre de pompe
            //multiplication par le nombre de pompe si placees en seriee
            //valeur_Y_dispo = Y_disposition_puissance(num_pompe, valeur_Y);
            //Modification du Y selon le nombre de stage, multiplication par
            //le nombre de stages
            //valeur_Y_stages = Y_nb_stages_puissance(num_pompe, valeur_Y_dispo);
            //Modification du Y selon le ratio du diametre de l'impeller
            //Multiplication par le (pourcentage mis en centieme au carre)
            valeur_Y_ratio = Y_ratio_puissance(num_pompe, valeur_Y);
            //Modification du Y selon la vitesse modifie du la pompe
            //Y * (Vitesse Modifiee / Vitesse Max) au carre
            valeur_Y_vitesse = Y_vitesse_puissance(num_pompe, valeur_Y_ratio, vitesse_max);

            return valeur_Y_vitesse;
        }

        private double X_polynome_pompe(int num_pompe, double x, double vitesse_max)
        {
            double valeur_X_dispo;
            double valeur_X_ratio;
            double valeur_X_vitesse;
            double valeur_X_unite;
            double valeur_X_pondere;
            //Modification du X selon la disposition et le nombre de pompes
            //Multiplication par le nombre de pompes si placees en paralleles
            valeur_X_dispo = X_disposition_pompe(num_pompe, x);
            //Modification du X selon le ratio du diametre de l'impeller
            //Multiplication par le (pourcentage mis en centieme au cube)
            valeur_X_ratio = X_ratio(num_pompe, valeur_X_dispo);
            //Modification du X selon la vitesse modifiee de la pompe
            //X * (Vitesse Modifiee / Vitesse Max)
            valeur_X_vitesse = X_vitesse(num_pompe, valeur_X_ratio, vitesse_max);
            //Modification du X selon les unites choisies
            //Selon l'unite de debit selectionnee
            valeur_X_unite = X_unite(num_pompe, valeur_X_vitesse);
            //Arrondissement pour eviter les nombreuses decimales a l'affichage
            valeur_X_pondere = valeur_X_unite;

            return valeur_X_pondere;
        }

        private double X_polynome_puissance(int num_pompe, double x, double vitesse_max)
        {
            double valeur_X_ratio;
            double valeur_X_vitesse;
            double valeur_X_unite;

            valeur_X_ratio = X_ratio_puissance(num_pompe, x);
            valeur_X_vitesse = X_vitesse_puissance(num_pompe, valeur_X_ratio, vitesse_max);
            valeur_X_unite = X_unite(num_pompe, valeur_X_vitesse);

            return valeur_X_unite;
        }


        /*=======================SAUVEGARDE==================================*/

        //Bouton Enregistrer SOus
        private void btn_EnregistrerSous_Click(object sender, EventArgs e)
        {
            ecriture_sauvegarde_dossier();
        }
        //Bouton Ouverture de fichier
        private void btn_ouvrir_fichier_Click(object sender, EventArgs e)
        {
            ouvrir_fichier_sauvegarde();
        }

        //Ecriture des entrees dans un fichier texte
        private void ecriture_sauvegarde_dossier()
        {
            /*La fonction utilise l'algorithme de classement des info ""dossier_sauvegarde""
             Les donnees sont classees dans un struct t_sauvegarde selon un ordre permettant
             reouverture du dossier par browsing*/

            //Le dossier comportant les informations recueuillies
            Stream sauvegarde;

            string[] nom_split = new string[10];
            string nom = null;

            //Option et parametres de la boite de dialogue de sauvegarde
            //  saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Filter = "curve files (*.curv)|";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (pathstring != null)
            {
                saveFileDialog1.InitialDirectory = pathstring;
            }

            //Une fois le fichier nomme et son emplacement choisi
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pathstring = saveFileDialog1.FileName;

                System.IO.Directory.CreateDirectory(pathstring);

                init.tab_string(nom_split, 10, "");

                nom_split = pathstring.Split('\\');

                nom = String.Concat(nom_split.Last(), ".curv");

                //Ouverture (creation du fichier)
                if ((sauvegarde = File.Create(System.IO.Path.Combine(pathstring, nom))) != null)
                {
                    //Utilisation d'un stream writer pour aller ecrire dans le dossier cree
                    using (StreamWriter sw = new StreamWriter(sauvegarde))
                    {
                        //Ecriture des infos client
                        for (int i = k.NOM_CLIENT; i <= k.DATE; i++)
                        {
                            sw.WriteLine(inputs.acceuil.info_client[i]);
                        }
                        //Ecriture des unites et langage
                        sw.WriteLine(inputs.acceuil.langue);
                        sw.WriteLine(inputs.acceuil.unit_debit_string);
                        sw.WriteLine(inputs.acceuil.unit_longueur_string);
                        sw.WriteLine(inputs.acceuil.unit_pression_string);
                        sw.WriteLine(inputs.acceuil.unit_puissance_string);

                        //Ecriture ds debits entres a la page de point d'action
                        sw.WriteLine("///Ecriture des debits des points d'action");
                        for (int i = k.DEBIT_AXE_X; i <= k.DEBIT_P3; i++)
                        {
                            sw.WriteLine(inputs.action.debit[i].ToString());
                        }
                        //Ecriture des pression entrees a la page point d'action
                        sw.WriteLine("///Ecriture des pression de la page d'action");
                        for (int i = k.PRESSION_P1; i <= k.PRESSION_P3; i++)
                        {
                            sw.WriteLine(inputs.action.pression[i].ToString());
                        }
                        //Ecriture du mode de point d'action
                        sw.WriteLine("///Ecriture du mode de point d'action");
                        for (int i = k.DROITE_P1; i <= k.DROITE_P3; i++)
                        {
                            sw.WriteLine(inputs.action.droite[i].ToString());
                        }
                        //Ecriture du mode legende auto ou manuel
                        sw.WriteLine("///Ecriture du mode legende auto ou man");
                        for (int i = k.LEGENDE_P1; i <= k.LEGENDE_P3; i++)
                        {
                            sw.WriteLine(inputs.action.legende_auto[i].ToString());
                        }
                        //Ecriture de la legende entree manuellement
                        sw.WriteLine("///Ecriture de la legende manuelle");
                        for (int i = k.LEGENDE_P1; i <= k.LEGENDE_P3; i++)
                        {
                            sw.WriteLine(inputs.action.legende[i]);
                        }
                        sw.WriteLine("///Ecriture du mode de curseur");
                        {
                            if (checkBox_Curseur_Auto.Checked)
                            {
                                sw.WriteLine("auto");
                                sw.WriteLine(k.VIDE);
                                sw.WriteLine(k.VIDE);
                            }
                            else if (checkBox_Curseur_Semi_Auto.Checked)
                            {
                                sw.WriteLine("semi_auto");
                                sw.WriteLine(graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                    CursorX.Position.ToString());
                                sw.WriteLine(graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                    CursorY.Position.ToString());
                            }
                            else if (checkBox_Curseur_Man.Checked)
                            {
                                sw.WriteLine("man");
                                sw.WriteLine(graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                    CursorX.Position.ToString());
                                sw.WriteLine(graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                    CursorY.Position.ToString());
                            }
                        }
                        //Ecriture des infos de segments de tuyauterie
                        sw.WriteLine("///Ecriture des infos de segment tuyauterie");
                        for (int i = k.SECTION_A; i <= k.SECTION_E; i++)
                        {
                            //Section active ou non
                            sw.WriteLine(inputs.section[i].actif.ToString());
                            //Le materiel utilise
                            sw.WriteLine(inputs.section[i].selection_materiel);
                            //Le type de tuyauterie
                            sw.WriteLine(inputs.section[i].selection_pipe_type);
                            //Le nombre de lignes paralleles
                            sw.WriteLine(inputs.section[i].num_parallel_lines.ToString());
                            //La longueur de la sectin
                            sw.WriteLine(inputs.section[i].longueur_section.ToString());
                            //La hateur de la tete statique
                            sw.WriteLine(inputs.section[i].static_head.ToString());

                            //Ecriture des fittings
                            for (int j = 0; j < 10; j++)
                            {
                                //Quantite
                                sw.WriteLine(inputs.section[i].fitting[j].quantite.ToString());
                                //Type de fitting
                                sw.WriteLine(inputs.section[i].fitting[j].fitting);
                            }
                            //Ecriture du facteur de securite
                            sw.WriteLine(inputs.section[i].safety_factor.ToString());
                        }
                        //Ecriture des infos de segments de tuyauterie
                        sw.WriteLine("///Ecriture des infos de pompes");
                        for (int i = k.POMPE_1; i <= k.POMPE_12; i++)
                        {
                            //Marque
                            sw.WriteLine(inputs.pompes[i].index_marque);
                            sw.WriteLine(inputs.pompes[i].marque);
                            //Serie
                            sw.WriteLine(inputs.pompes[i].index_serie);
                            sw.WriteLine(inputs.pompes[i].serie);
                            //Modele
                            sw.WriteLine(inputs.pompes[i].index_modele);
                            sw.WriteLine(inputs.pompes[i].modele);
                            //Nb pompe
                            sw.WriteLine(inputs.pompes[i].nb_pompe.ToString());
                            sw.WriteLine(inputs.pompes[i].disposition.ToString());
                            //Nb de stage
                            sw.WriteLine(inputs.pompes[i].nb_stages.ToString());
                            //Ratio impeller
                            sw.WriteLine((inputs.pompes[i].ratio_diametre * 100).ToString());
                            //Vitesse
                            sw.WriteLine(inputs.pompes[i].vitesse.ToString());
                        }
                        sw.WriteLine("///EOF");
                    }
                    //Fermeture du fichier une fois les modifications et informations ecrites
                    sauvegarde.Close();

                    lbl_client_date.Text =
                        String.Concat(inputs.acceuil.info_client[k.DATE],
                            "  -  ",
                                inputs.acceuil.info_client[k.REVISION]);
                }
            }
        }


        /*=========================GRAPHIQUE=================================*/
        //Evenement changeant l'affichage du graphique, NormalSize ou FullScreen
        private void graphique_1_DoubleClick(object sender, EventArgs e)
        {
            if (fullscreen == true)
            {
                ajustement_graphique_normalsize();
            }
            else
            {
                ajustement_graphique_fullscreen();
            }
        }
        //Ajustment du graphique en mode demi ecran
        private void ajustement_graphique_normalsize()
        {
            //Effacement du panneau client et ajustement du chart areas automatique
            fullscreen = false;
            pnl_client_graph.Visible = false;
            lbl_Section.Visible = false;
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].InnerPlotPosition.Auto = true;
            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].InnerPlotPosition.Auto = true;
            graphique_1.ChartAreas[k.GRAPH_TROIS].InnerPlotPosition.Auto = true;

            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].Position.Y = 67;
            graphique_1.ChartAreas[k.GRAPH_TROIS].Position.Y = 67;


            graphique_1.Height = dimensions[k.HAUTEUR] - (k.BORDURE_PNL * 4);

            graphique_1.Width = dimensions[k.LARGEUR] - pnl_system.Width -
        (k.BORDURE_PNL * 7);
            //Ajustment du positionnement du graphique
            graphique_1.Location = new Point(pnl_system.Width +
        (k.BORDURE_PNL * 4), k.BORDURE_PNL);
            //Ajustment de la taille et de la position du graphique
            pnl_EnTete_Graph.Width = graphique_1.Width;
            pnl_EnTete_Graph.Height = graphique_1.Height / k.PROPORTION_TREIZE;

            pnl_EnTete_Graph.Location =
                new Point(graphique_1.Location.X, graphique_1.Location.Y);
            //Ajustement taille et position du titre du graphique
            int x_titre_graph = (graphique_1.Width / 2);
            int y_titre_graph = (pnl_EnTete_Graph.Height / k.PROPORTION_DEUX)
                                    - (lbl_titre_graph.Height / k.PROPORTION_DEUX);
            lbl_titre_graph.Location = new Point(x_titre_graph, y_titre_graph);
            //Ajustement position du logo
            int x_pic_Logo_graph = (pnl_EnTete_Graph.Width / k.PROPORTION_QUATRE)
                        - (pic_Logo_graph_FR.Width / 2);
            pic_Logo_graph_FR.Location =
                new Point(x_pic_Logo_graph,
                                (pnl_EnTete_Graph.Height / 2)
                                    - (pic_Logo_graph_FR.Height / 2));
            pic_Logo_graph_EN.Location = pic_Logo_graph_FR.Location;
            btn_imprimer.Visible = false;
            btn_Curseur2.Visible = false;
        }
        //Ajustement du graphique en mode full screen
        private void ajustement_graphique_fullscreen()
        {
            //Affichage du panneau client et ajustement du chart areas manuellement
            fullscreen = true;
            pnl_client_graph.Visible = true;
            //Ajustement et affichage lbl_section_graph
            affichage_lbl_section();
            //Ajustment du Chart Area du graph
            graphique_1.ChartAreas[0].InnerPlotPosition.Auto = false;
            graphique_1.ChartAreas[0].InnerPlotPosition.Height = 90;
            graphique_1.ChartAreas[0].InnerPlotPosition.Y = 6;

            graphique_1.ChartAreas[1].InnerPlotPosition.Auto = false;
            graphique_1.ChartAreas[1].InnerPlotPosition.Height = 40;
            graphique_1.ChartAreas[1].InnerPlotPosition.Y = 6;

            graphique_1.ChartAreas[1].Position.Y = 72;

            graphique_1.ChartAreas[2].InnerPlotPosition.Auto = false;
            graphique_1.ChartAreas[2].InnerPlotPosition.Height = 40;
            graphique_1.ChartAreas[2].InnerPlotPosition.Width = 80;
            graphique_1.ChartAreas[2].InnerPlotPosition.Y = 6;

            graphique_1.ChartAreas[2].Position.Y = 72;

            graphique_1.Width = pnl_SystemInput.Width;
            //graphique_1.Height = pnl_SystemInput.Height;

            graphique_1.Height = dimensions[k.HAUTEUR];
            graphique_1.Width = dimensions[k.LARGEUR] - (k.BORDURE_PNL * 2);

            graphique_1.Location = new Point(0, 0);
            //Ajustement de la taille et de la position du graphique
            pnl_EnTete_Graph.Width = (graphique_1.Width / 4) * 3;
            pnl_EnTete_Graph.Height = graphique_1.Height / k.PROPORTION_DIX;
            pnl_EnTete_Graph.Location =
                new Point(graphique_1.Location.X, graphique_1.Location.Y);
            //Ajustement taille et position du titre du graphique
            int x_titre_graph = (graphique_1.Width / 2)
                                    - (lbl_titre_graph.Width / 2);

            int y_titre_graph = (pnl_EnTete_Graph.Height / 2)
                                    - (lbl_titre_graph.Height / 2);
            lbl_titre_graph.Location = new Point(x_titre_graph, y_titre_graph);
            //Bouton imprimer
            int x_btn_print = 10;
            int y_btn_print = dimensions[k.HAUTEUR] - btn_imprimer.Height - 10;
            btn_imprimer.Location = new Point(x_btn_print, y_btn_print);

            btn_Curseur2.Location = new Point(x_btn_print, y_btn_print - 400);

            btn_imprimer.Visible = true;

            btn_Curseur2.Visible = true;
            //AJustement position du logo graph
            int x_pic_Logo_graph = (pnl_EnTete_Graph.Width / k.PROPORTION_QUATRE)
                                    - (pic_Logo_graph_FR.Width / 2);
            pic_Logo_graph_FR.Location =
                new Point(x_pic_Logo_graph,
                            (pnl_EnTete_Graph.Height / 2)
                                - (pic_Logo_graph_FR.Height / 2));
            pic_Logo_graph_EN.Location = pic_Logo_graph_FR.Location;
        }
        //Panneau Client Graphique selectionne par le bouton gauche de la souris
        private void pnl_client_graph_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }
        //Permet le deplacement du panneau
        private void pnl_client_graph_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pnl_client_graph.Left =
                                        e.X
                                            + pnl_client_graph.Left
                                                - MouseDownLocation.X;
                pnl_client_graph.Top =
                                        e.Y
                                            + pnl_client_graph.Top
                                                - MouseDownLocation.Y;
            }
        }
        //Panneau selectionne
        private void lbl_Section_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }
        //Permet le mouvement du panneau
        private void lbl_Section_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                lbl_Section.Left =
                                    e.X
                                        + lbl_Section.Left
                                            - MouseDownLocation.X;
                lbl_Section.Top =
                                    e.Y
                                        + lbl_Section.Top
                                            - MouseDownLocation.Y;
            }
        }
        //Reajustement automatique si le panneau depasse les bornes
        private void lbl_Section_MouseUp(object sender, MouseEventArgs e)
        {
            if (depassement_droite(lbl_Section.Left, lbl_Section.Width))
            {
                lbl_Section.Left = position_max_droite(lbl_Section.Width);
            }
            if (depassement_gauche(lbl_Section.Left))
            {
                lbl_Section.Left = position_max_gauche();
            }
            if (depassement_haut(lbl_Section.Top))
            {
                lbl_Section.Top = position_max_haut();
            }
            if (depassement_bas(lbl_Section.Bottom))
            {
                lbl_Section.Top = position_max_bas(lbl_Section.Height);
            }
        }
        //Methode permettant de deplacer le pnl client avec la souris
        private void pnl_client_graph_MouseUp(object sender, MouseEventArgs e)
        {
            if (depassement_droite(pnl_client_graph.Left, pnl_client_graph.Width))
            {
                pnl_client_graph.Left = position_max_droite(pnl_client_graph.Width);
            }
            if (depassement_gauche(pnl_client_graph.Left))
            {
                pnl_client_graph.Left = position_max_gauche();
            }
            if (depassement_haut(pnl_client_graph.Top))
            {
                pnl_client_graph.Top = position_max_haut();
            }
            if (depassement_bas(pnl_client_graph.Bottom))
            {
                pnl_client_graph.Top = position_max_bas(pnl_client_graph.Height);
            }
        }

        private bool depassement_droite(int Left, int largeur_contenant)
        {
            return ((dimensions[k.LARGEUR]
                        - Left
                            - (k.BORDURE_PNL * 5))
                                <
                   largeur_contenant);
        }
        private bool depassement_gauche(int Left)
        {
            return ((k.BORDURE_PNL * 5) > Left);
        }
        private bool depassement_haut(int Top)
        {
            return ((k.BORDURE_PNL * 5) > Top);
        }
        private bool depassement_bas(int Bottom)
        {
            return (dimensions[k.HAUTEUR] - (k.BORDURE_PNL * 5) < Bottom);
        }

        private int position_max_droite(int largeur_contenant)
        {
            return (dimensions[k.LARGEUR]
                        - largeur_contenant
                            - (k.BORDURE_PNL * 5));
        }
        private int position_max_gauche()
        {
            return (k.BORDURE_PNL * 5);
        }
        private int position_max_haut()
        {
            return (k.BORDURE_PNL * 5);
        }
        private int position_max_bas(int hauteur_contenant)
        {
            return (dimensions[k.HAUTEUR]
                        - (k.BORDURE_PNL * 5)
                            - hauteur_contenant);
        }
        //Activation des legende des points d'action
        private string[] activer_legende_actions(t_inputs inputs, int serie)
        {
            string[] action = new string[2];
            action[k.LEGENDE_DEBIT] = inputs.action.debit[serie].ToString();
            action[k.LEGENDE_PRESSION] = inputs.action.pression[serie].ToString();
            return action;
        }
        //Activation des legendes du graphique
        private void activer_legende(int serie)
        {
            string dispo = (rBtn_English.Checked) ? "Parallel" : "Parallele";
            string[] action = new string[2];

            //Serie de point d'action
            if (serie >= k.SERIE && serie < k.SERIE_POMPE_1)
            {
                //Si un debit et une pression sont entres
                if (inputs.action.debit[serie] != 0
                        && inputs.action.pression[serie] != 0)
                {
                    //Legende en mode manuel
                    if (inputs.action.legende_auto[serie] == false)
                    {
                        graphique_1.Series[serie].Name =
                            String.Concat(serie,
                                " - ",
                                    inputs.action.legende[serie]);
                    }
                    //Legende en mode automatique
                    else
                    {
                        string unites_debit = "";
                        string unites_pression = "";
                        //Ecriture des unites de debit selon la selection
                        if (rBtn_USGPM.Checked)
                        {
                            unites_debit = " USGPM - ";
                        }
                        else if (rBtn_m3h.Checked)
                        {
                            unites_debit = " m3/hr";
                        }

                        //Ecriture des unites de pression selon la selection
                        if (rBtn_ftH20.Checked)
                        {
                            unites_pression = " ftH20";
                        }
                        else if (rBtn_mH20.Checked)
                        {
                            unites_pression = " mH20";
                        }
                        else if (rBtn_PSI.Checked)
                        {
                            unites_pression = " PSI";
                        }
                        else if (rBtn_Pascal.Checked)
                        {
                            unites_pression = "kPa";
                        }

                        //Va chercher le debit et la pression de la serie
                        action = activer_legende_actions(inputs, serie);
                        graphique_1.Series[serie].Name =
                            String.Concat(
                                serie.ToString(),
                                    " - ",
                                        action[k.LEGENDE_DEBIT],
                                            unites_debit,
                                                action[k.LEGENDE_PRESSION],
                                                    unites_pression);
                    }
                    graphique_1.Series[serie].Enabled = true;
                }
                else if (inputs.action.debit[serie] == 0
                            || inputs.action.pression[serie] == 0)
                {
                    effacer_serie(0, serie, serie, serie);
                }
            }
            //Si la serie est une pompe
            else if (serie > k.SERIE_POINT_ACTION_UN)
            {
                int num_pompe = serie - k.OFFSET_3;

                //Ecriture de la legende
                try
                {
                    //S'il y a plus d'une pompe
                    if (inputs.pompes[num_pompe].nb_pompe > 1)
                    {
                        if (inputs.pompes[num_pompe].disposition == k.DISPO_SERIE)
                        {
                            dispo = "Serie";
                        }
                        graphique_1.Series[serie].Name =
                            String.Concat(
                                inputs.pompes[num_pompe].nb_pompe,
                                    " X ",
                                        inputs.pompes[num_pompe].marque,
                                            ' ',
                                                inputs.pompes[num_pompe].modele,
                                                    " - ",
                                                        dispo,
                                                            "\n",
                                                                inputs.pompes[num_pompe].vitesse.ToString(),
                                                                    " RPM\n",
                                                                        inputs.pompes[num_pompe].nb_stages,
                                                                            " X stages",
                                                                                "\n Ratio impeller : ",
                                                                                    inputs.pompes[num_pompe].ratio_diametre * 100,
                                                                                        "%");
                    }
                    //S'il n'y a qu'une pompe
                    else
                    {
                        graphique_1.Series[serie].Name =
                            String.Concat(
                                inputs.pompes[num_pompe].nb_pompe,
                                    " X ",
                                        inputs.pompes[num_pompe].marque,
                                            ' ',
                                                inputs.pompes[num_pompe].modele,
                                                    "\n",
                                                        inputs.pompes[num_pompe].vitesse.ToString(),
                                                            " RPM\n",
                                                                inputs.pompes[num_pompe].nb_stages,
                                                                    " X stages",
                                                                        "\n Ratio impeller : ",
                                                                            inputs.pompes[num_pompe].ratio_diametre * 100,
                                                                                "%");
                    }
                }
                //Si un meme systeme (la legende existe deja), un message d'erreur est affiche
                catch
                {
                    //S'il y a plus d'une pompe
                    if (inputs.pompes[num_pompe].nb_pompe > 1)
                    {
                        if (inputs.pompes[num_pompe].disposition == k.DISPO_SERIE)
                        {
                            dispo = "Serie";
                        }
                        graphique_1.Series[serie].Name =
                            String.Concat(
                                "P",
                                    num_pompe,
                                    "\n",
                                        inputs.pompes[num_pompe].nb_pompe,
                                            " X ",
                                                inputs.pompes[num_pompe].marque,
                                                    ' ',
                                                        inputs.pompes[num_pompe].modele,
                                                            " - ",
                                                                dispo,
                                                                    "\n",
                                                                        inputs.pompes[num_pompe].vitesse.ToString(),
                                                                            " RPM\n",
                                                                                inputs.pompes[num_pompe].nb_stages,
                                                                                    " X stages",
                                                                                        "\n Ratio impeller : ",
                                                                                            inputs.pompes[num_pompe].ratio_diametre * 100,
                                                                                                "%");
                    }
                    //S'il n'y a qu'une pompe
                    else
                    {
                        graphique_1.Series[serie].Name =
                            String.Concat(
                                "P",
                                    num_pompe,
                                    "\n",
                                        inputs.pompes[num_pompe].nb_pompe,
                                            " X ",
                                                inputs.pompes[num_pompe].marque,
                                                    ' ',
                                                        inputs.pompes[num_pompe].modele,
                                                            "\n",
                                                                inputs.pompes[num_pompe].vitesse.ToString(),
                                                                    " RPM\n",
                                                                        inputs.pompes[num_pompe].nb_stages,
                                                                            " X stages",
                                                                                "\n Ratio impeller : ",
                                                                                    inputs.pompes[num_pompe].ratio_diametre * 100,
                                                                                        "%");
                    }
                    graphique_1.Series[serie].Enabled = true;
                }
            }
            else if (serie == k.SERIE_TUYAUTERIE)
            {
                graphique_1.Series[serie].Enabled = true;
            }
        }
        //Desactivation et effacement de la legende
        private void desactiver_legende(int serie)
        {
            graphique_1.Series[serie].Enabled = false;
        }
        //Tracage d'un point d'action
        private void tracer_point_action(int serie_action)
        {
            raz_serie(serie_action);

            if (inputs.action.debit[serie_action] != 0
                    && inputs.action.pression[serie_action] != 0)
            {
                if (graphique_1.Series[serie_action].Enabled == false)
                {
                    activer_legende(serie_action);
                }
                graphique_1.Series[serie_action].Points.AddXY(
                                    inputs.action.debit[serie_action],
                                        inputs.action.pression[serie_action]);
                if (inputs.action.droite[serie_action] == true)
                {
                    graphique_1.Series[serie_action].Points.AddXY(
                                    inputs.action.debit[serie_action], 0);
                    graphique_1.Series[serie_action].BorderWidth =
                                                            k.EPAISSEUR_DROITE;
                }
                else
                {
                    graphique_1.Series[serie_action].Points.AddXY(
                                    inputs.action.debit[serie_action],
                                        inputs.action.pression[serie_action]);
                    graphique_1.Series[serie_action].BorderWidth =
                                                            k.EPAISSERUR_POINT;
                }
            }
        }
        //Effacement d'une courbe sur le graphique
        private void effacer_serie(int pompe, int serie, int serie_efficacite, int serie_puissance)
        {
            //Efface la courbe
            raz_serie(serie);
            raz_serie(serie_efficacite);
            raz_serie(serie_puissance);
            //Desactivation de la legende
            desactiver_legende(serie);
            //Selon la pompe, le switchcase procede a la remise a zero des selections
            switch (pompe)
            {
                case 0: break;

                case k.POMPE_1:
                    //Effacer selection choix des cBox 
                    cBox_Marque1.SelectedIndex = k.VIDE;
                    cBox_Serie1.SelectedIndex = k.VIDE;
                    cBox_Modele1.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie1.Items.Clear();
                    cBox_Modele1.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe1.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage1.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio1.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P1.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P1.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P1.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_2:
                    //Effacer selection choix des cBox 
                    cBox_Modele2.SelectedIndex = k.VIDE;
                    cBox_Serie2.SelectedIndex = k.VIDE;
                    cBox_Marque2.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie2.Items.Clear();
                    cBox_Modele2.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe2.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage2.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio2.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P2.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P2.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P2.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_3:
                    //Effacer selection choix des cBox 
                    cBox_Modele3.SelectedIndex = k.VIDE;
                    cBox_Serie3.SelectedIndex = k.VIDE;
                    cBox_Marque3.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie3.Items.Clear();
                    cBox_Modele3.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe3.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage3.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio3.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P3.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P3.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P3.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_4:
                    //Effacer selection choix des cBox 
                    cBox_Modele4.SelectedIndex = k.VIDE;
                    cBox_Serie4.SelectedIndex = k.VIDE;
                    cBox_Marque4.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie4.Items.Clear();
                    cBox_Modele4.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe4.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage4.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio4.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P4.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P4.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P4.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_5:
                    //Effacer selection choix des cBox 
                    cBox_Modele5.SelectedIndex = k.VIDE;
                    cBox_Serie5.SelectedIndex = k.VIDE;
                    cBox_Marque5.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie5.Items.Clear();
                    cBox_Modele5.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe5.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage5.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio5.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P5.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P5.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P5.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_6:
                    //Effacer selection choix des cBox 
                    cBox_Modele6.SelectedIndex = k.VIDE;
                    cBox_Serie6.SelectedIndex = k.VIDE;
                    cBox_Marque6.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie6.Items.Clear();
                    cBox_Modele6.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe6.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage6.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio6.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P6.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P6.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P6.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_7:
                    //Effacer selection choix des cBox 
                    cBox_Modele7.SelectedIndex = k.VIDE;
                    cBox_Serie7.SelectedIndex = k.VIDE;
                    cBox_Marque7.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie7.Items.Clear();
                    cBox_Modele7.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe7.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage7.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio7.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P7.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P7.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P7.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_8:
                    //Effacer selection choix des cBox 
                    cBox_Modele8.SelectedIndex = k.VIDE;
                    cBox_Serie8.SelectedIndex = k.VIDE;
                    cBox_Marque8.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie8.Items.Clear();
                    cBox_Modele8.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe8.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage8.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio8.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P8.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P8.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P8.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_9:
                    //Effacer selection choix des cBox 
                    cBox_Modele9.SelectedIndex = k.VIDE;
                    cBox_Serie9.SelectedIndex = k.VIDE;
                    cBox_Marque9.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie9.Items.Clear();
                    cBox_Modele9.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe9.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage9.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio9.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P9.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P9.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P9.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_10:
                    //Effacer selection choix des cBox 
                    cBox_Modele10.SelectedIndex = k.VIDE;
                    cBox_Serie10.SelectedIndex = k.VIDE;
                    cBox_Marque10.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie10.Items.Clear();
                    cBox_Modele10.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe10.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage10.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio10.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P10.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P10.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P10.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_11:
                    //Effacer selection choix des cBox 
                    cBox_Modele11.SelectedIndex = k.VIDE;
                    cBox_Serie11.SelectedIndex = k.VIDE;
                    cBox_Marque11.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie11.Items.Clear();
                    cBox_Modele11.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe11.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage11.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio11.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P11.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P11.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P11.Value = k.VITESSE_PAR_DEFAUT;
                    break;
                case k.POMPE_12:
                    //Effacer selection choix des cBox 
                    cBox_Modele12.SelectedIndex = k.VIDE;
                    cBox_Serie12.SelectedIndex = k.VIDE;
                    cBox_Marque12.SelectedIndex = k.VIDE;
                    //Effacement des items dans les cBox Serie et Modele, 
                    //necessitant un choix de marque pour disponibilite des choix
                    cBox_Serie12.Items.Clear();
                    cBox_Modele12.Items.Clear();
                    //Remise des valeurs par defaut pour les modifiant de la pompe
                    nUD_Nb_Pompe12.Value = k.NB_POMPE_PAR_DEFAUT;
                    nUD_Nb_Stage12.Value = k.NB_STAGES_PAR_DEFAUT;
                    nUD_Ratio12.Value = k.RATIO_PAR_DEFAUT;
                    Scroll_Vit_P12.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P12.Maximum = k.VITESSE_PAR_DEFAUT;
                    nUD_Vit_P12.Value = k.VITESSE_PAR_DEFAUT;
                    break;
            }
        }

        //Ajustement et affichage du lbl section graph
        private void affichage_lbl_section()
        {
            //Depart avec un panneau assez grand pour contenir un seul label
            lbl_Section.Height = 1;
            lbl_Section.Visible = false;
            //Permet au label de modifier automatiquement sa longueur selon son contenu
            lbl_Section_1.AutoSize = true;
            lbl_Section_2.AutoSize = true;
            lbl_Section_3.AutoSize = true;
            lbl_Section_4.AutoSize = true;
            lbl_Section_5.AutoSize = true;
            //Nombre de section complete et active commence a zero
            int nb_section = 0;
            int plus_long = 0;
            //min_section a false indique qu'il n'y a aucune section complete
            bool min_section = false;

            //Parcourt des sections pour concatener les informations pour le descriptif dans le graph
            for (int i = k.SECTION_A; i <= k.SECTION_E; i++)
            {
                //Si la section est complete et active
                if (inputs.section[i].actif
                        && inputs.section[i].selection_materiel != null
                            && inputs.section[i].selection_pipe_type != null)
                {
                    //Indique qu'au moins une section est complete et active
                    min_section = true;
                    //Augemente la taille du panneau pour acceuillir un autre 
                    //label
                    lbl_Section.Height += 21;
                    //Interation du nombre de section, pour savoir la position 
                    //du descriptif dans le panneau
                    nb_section++;
                    //Selon le nombre de section, le label approprie est rempli
                    //Compraison des longueur pour le resize de longueur selon 
                    //le label le plus long
                    switch (nb_section)
                    {
                        case 1:
                            lbl_Section_1.Text = description_section(i);
                            plus_long = lbl_Section_1.Width;
                            break;
                        case 2:
                            lbl_Section_2.Text = description_section(i);
                            plus_long =
                                (lbl_Section_2.Width > plus_long)
                                    ? lbl_Section_2.Width : plus_long;
                            break;
                        case 3:
                            lbl_Section_3.Text = description_section(i);
                            plus_long =
                                (lbl_Section_3.Width > plus_long)
                                    ? lbl_Section_3.Width : plus_long;
                            break;
                        case 4:
                            lbl_Section_4.Text = description_section(i);
                            plus_long =
                                (lbl_Section_4.Width > plus_long)
                                    ? lbl_Section_4.Width : plus_long;
                            break;
                        case 5:
                            lbl_Section_5.Text = description_section(i);
                            plus_long =
                                (lbl_Section_5.Width > plus_long)
                                    ? lbl_Section_5.Width : plus_long;
                            break;
                    }
                }
            }
            if (min_section == true)
            {
                //Size manuelle
                lbl_Section_1.AutoSize = false;
                lbl_Section_2.AutoSize = false;
                lbl_Section_3.AutoSize = false;
                lbl_Section_4.AutoSize = false;
                lbl_Section_5.AutoSize = false;
                //Le panneau change de taille selon la largeur du label le 
                //plus large
                lbl_Section.Width = plus_long + 4;
                //Tous les labels prennent la largeur du plus long
                lbl_Section_1.Width = plus_long;
                lbl_Section_2.Width = plus_long;
                lbl_Section_3.Width = plus_long;
                lbl_Section_4.Width = plus_long;
                lbl_Section_5.Width = plus_long;
                //Apparition du panneau
                lbl_Section.Visible = true;
                //AJustment de location selon la position du pnl client

                if (lbl_Section.Location.X > (dimensions[k.LARGEUR]
                        - lbl_Section.Width
                            - (k.BORDURE_PNL * 3)))
                {
                    lbl_Section.Left = (dimensions[k.LARGEUR]
                        - lbl_Section.Width
                            - (k.BORDURE_PNL * 3));
                }
            }
        }
        //Description des sections tuyauterie dans le graphique
        private string description_section(int section)
        {
            //Ecriture du descriptif de tete statique selon la langue
            string tete =
                    (rBtn_English.Checked)
                        ? "  -  Static Head : " : "  -  Tete Statique : ";
            //Ecriture du descriptif de facteur de securite selon la langue
            string facteur =
                    (rBtn_English.Checked)
                        ? " Safety Factor " : " Facteur de securite ";
            //Ecriture du descriptif d'unites selon la selection
            string unite = (rBtn_Metres.Checked) ? " m " : " ft ";

            //Format de description : Ex:
            //6 X 100(m ou ft) Boreline - Boreline C = 200 - Tete Statique : 100(m ou ft) Facteur de securite 10%
            return String.Concat(inputs.section[section].num_parallel_lines,
                                    " X ",
                                 inputs.section[section].longueur_section,
                                    unite,
                                 inputs.section[section].pipe_type,
                                    " - ",
                                 inputs.section[section].materiel,
                                    " C = ",
                                 inputs.section[section].constante_hazen_williams,
                                    tete,
                                 inputs.section[section].static_head,
                                    unite,
                                 facteur,
                                    inputs.section[section].safety_factor,
                                 " % ");
        }
        //Retracage des courbes de pompe, utile apres un changement d'unites
        private void retracage_pompe()
        {
            for (int i = 1; i <= k.POMPE_12; i++)
            {

                switch (i)
                {
                    case k.POMPE_1:
                        if (cBox_Modele1.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque1.SelectedIndex,
                                cBox_Serie1.SelectedIndex,
                                    cBox_Modele1.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);

                        }
                        break;
                    case k.POMPE_2:
                        if (cBox_Modele2.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque2.SelectedIndex,
                                cBox_Serie2.SelectedIndex,
                                    cBox_Modele2.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_3:
                        if (cBox_Modele3.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque3.SelectedIndex,
                                cBox_Serie3.SelectedIndex,
                                    cBox_Modele3.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_4:
                        if (cBox_Modele4.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque4.SelectedIndex,
                                cBox_Serie4.SelectedIndex,
                                    cBox_Modele4.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_5:
                        if (cBox_Modele5.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque5.SelectedIndex,
                                cBox_Serie5.SelectedIndex,
                                    cBox_Modele5.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_6:
                        if (cBox_Modele6.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque6.SelectedIndex,
                                cBox_Serie6.SelectedIndex,
                                    cBox_Modele6.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_7:
                        if (cBox_Modele7.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque7.SelectedIndex,
                                cBox_Serie7.SelectedIndex,
                                    cBox_Modele7.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_8:
                        if (cBox_Modele8.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque8.SelectedIndex,
                                cBox_Serie8.SelectedIndex,
                                    cBox_Modele8.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_9:
                        if (cBox_Modele9.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque9.SelectedIndex,
                                cBox_Serie9.SelectedIndex,
                                    cBox_Modele9.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_10:
                        if (cBox_Modele10.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque10.SelectedIndex,
                                cBox_Serie10.SelectedIndex,
                                    cBox_Modele10.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_11:
                        if (cBox_Modele11.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque11.SelectedIndex,
                                cBox_Serie11.SelectedIndex,
                                    cBox_Modele11.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                    case k.POMPE_12:
                        if (cBox_Modele12.SelectedIndex != k.VIDE)
                        {
                            info_pompe(cBox_Marque12.SelectedIndex,
                                cBox_Serie12.SelectedIndex,
                                    cBox_Modele12.SelectedIndex,
                                        i,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
                        }
                        break;
                }
            }
        }

        /*==================PAGE ACCEUIL=====================================*/
        #region Encapsulation Page Acceuil
        //Attribution des informations client dans le graphique
        private void tBox_client_Nom_TextChanged(object sender, EventArgs e)
        {
            inputs.acceuil.info_client[k.NOM_CLIENT] = tBox_client_Nom.Text;
            update_texte_client_nom();
        }
        private void update_texte_client_nom()
        {
            //string client = (rBtn_English.Checked) ? "Customer      : " : "Client            : ";
            //lbl_client_Nom.Text = String.Concat(client, tBox_client_Nom.Text);
            lbl_client_Nom.Text = String.Concat(": ", tBox_client_Nom.Text);
        }

        private void tBox_client_Projet_TextChanged(object sender, EventArgs e)
        {
            inputs.acceuil.info_client[k.PROJET] = tBox_client_Projet.Text;
            update_texte_client_projet();
        }
        private void update_texte_client_projet()
        {
            //string projet = (rBtn_English.Checked) ? "Project          : " : "Projet            : ";
            //lbl_client_Projet.Text = String.Concat(projet, tBox_client_Projet.Text);
            lbl_client_Projet.Text = String.Concat(": ", tBox_client_Projet.Text);
        }

        private void tBox_client_Description_TextChanged(object sender, EventArgs e)
        {
            inputs.acceuil.info_client[k.DESCRIPTION] = tBox_client_Description.Text;
            //lbl_client_Description.Text = String.Concat("Description   : ", tBox_client_Description.Text);
            lbl_client_Description.Text = String.Concat(": ", tBox_client_Description.Text);
        }
        private void tBox_client_DoneBy_TextChanged(object sender, EventArgs e)
        {
            inputs.acceuil.info_client[k.FAITPAR] = tBox_client_DoneBy.Text;
            update_texte_client_faitpar();
        }
        private void update_texte_client_faitpar()
        {
            //string faitpar = (rBtn_English.Checked) ? "Done By       : " : "Fait par         : ";
            //lbl_client_DoneBy.Text = String.Concat(faitpar, tBox_client_DoneBy.Text);
            lbl_client_DoneBy.Text = String.Concat(": ", tBox_client_DoneBy.Text);
        }

        private void tBox_client_Revision_TextChanged(object sender, EventArgs e)
        {
            inputs.acceuil.info_client[k.REVISION] = tBox_client_Revision.Text;

            lbl_client_date.Text = String.Concat(": ", inputs.acceuil.info_client[k.DATE], "  -  ", inputs.acceuil.info_client[k.REVISION]);
        }
        private void tBox_client_Date_TextChanged(object sender, EventArgs e)
        {
            inputs.acceuil.info_client[k.DATE] = tBox_client_Date.Text;

            lbl_client_date.Text = String.Concat(": ", inputs.acceuil.info_client[k.DATE], "  -  ", inputs.acceuil.info_client[k.REVISION]);
        }
        #endregion

        #region Changement d'unites
        //RadioBouton pour unite de pression en ftH20
        private void rBtn_ftH20_Click(object sender, EventArgs e)
        {
            if (rBtn_ftH20.Checked == false)
            {
                //Interlock des cochage des boutons et ajustement de Inputs
                unite_en_fth20();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des textes indiquant les unites selon le changement
                changement_texte_unites();
            }
        }
        //RadioBouton pour unite de pression en mH20
        private void rBtn_mH20_Click(object sender, EventArgs e)
        {
            if (rBtn_mH20.Checked == false)
            {
                //Interlock des cochage des boutons et ajustement des Inputs
                unite_en_mh20();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des textes indiquant les unites selon le changement
                changement_texte_unites();
            }
        }
        //RadioBouton pour unite de pression en PSI
        private void rBtn_PSI_Click(object sender, EventArgs e)
        {
            if (rBtn_PSI.Checked == false)
            {
                //Interlock des cochages des boutons et ajustement des Inputs
                unite_en_psi();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des textes indiquant les unites selon le changement
                changement_texte_unites();
            }
        }
        //RadioBouton pour unite de pression en kPa
        private void rBtn_Pascal_Click(object sender, EventArgs e)
        {
            if (rBtn_Pascal.Checked == false)
            {
                //Interlock des cochages des boutons et ajustement des Inputs
                unite_en_pascal();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des textes indiquant les unites selon le changement 
                changement_texte_unites();
            }
        }
        //RadioBouton pour unite de debit en USGPM
        private void rBtn_USGPM_Click(object sender, EventArgs e)
        {
            if (rBtn_USGPM.Checked == false)
            {
                //Interlock des cochages des boutons et ajustement des Inputs
                unite_en_USGPM();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des textes indiquant les unites selon le changement
                changement_texte_unites();
            }
        }
        //RadioBouton pour unite de debit en m3/hr
        private void rBtn_m3h_Click(object sender, EventArgs e)
        {
            if (rBtn_m3h.Checked == false)
            {
                //Interlock des cochages de boutons et ajustement des Inputs
                unite_en_m3hr();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des unites indiquant les unites selon le changement
                changement_texte_unites();
            }
        }
        //RadioBouton pour unite de longueur en pieds
        private void rBtn_Pieds_Click(object sender, EventArgs e)
        {
            if (rBtn_Pieds.Checked == false)
            {
                //Interlock des cochages de boutons et ajustement des Inputs
                unite_en_pieds();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des unites indiquant les unites selon le changement
                changement_texte_unites();
            }
        }
        //RadioBouton pour unite de longueur en metres
        private void rBtn_Metres_Click(object sender, EventArgs e)
        {
            if (rBtn_Metres.Checked == false)
            {
                //Interlock des cochages de boutons et ajustement des Inputs
                unite_en_metres();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des unites indiquant les unites selon le changement
                changement_texte_unites();
            }
        }
        //RadioBouton pour unite de puissance en Hp
        private void rBtn_Hp_Click(object sender, EventArgs e)
        {
            if (rBtn_Hp.Checked == false)
            {
                //Interlock des cochages de boutons et ajustement des Inputs
                unite_en_hp();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des unites indiquant les unites selon le changement
                changement_texte_unites();
            }
        }
        //RadioBouton pour unite de puissance en Watts
        private void rBtn_Watts_Click(object sender, EventArgs e)
        {
            if (rBtn_Watts.Checked == false)
            {
                //Interlock des cochages de boutons et ajustement des Inputs
                unite_en_watts();
                //Mise a jour des courbes et calculs selon le nouvel unite
                changement_unites();
                //Ajustement des unites indiquant les unites selon le changement
                changement_texte_unites();
            }
        }


        private void unite_en_fth20()
        {
            //Interlock des cochage de boutons
            rBtn_ftH20.Checked = true;
            rBtn_mH20.Checked = false;
            rBtn_PSI.Checked = false;
            rBtn_Pascal.Checked = false;
            //Ajustement des Inputs
            inputs.acceuil.unit_pression = k.FTH20;
            inputs.acceuil.unit_pression_string = "fth20";
        }
        private void unite_en_mh20()
        {
            //Interlock des cochages de boutons
            rBtn_mH20.Checked = true;
            rBtn_ftH20.Checked = false;
            rBtn_PSI.Checked = false;
            rBtn_Pascal.Checked = false;
            //Ajustement des Inputs
            inputs.acceuil.unit_pression = k.FTH20_A_MH20;
            inputs.acceuil.unit_pression_string = "mh20";
        }
        private void unite_en_psi()
        {
            //Interlock des cochages de boutons
            rBtn_PSI.Checked = true;
            rBtn_ftH20.Checked = false;
            rBtn_mH20.Checked = false;
            rBtn_Pascal.Checked = false;
            //Ajustemenet des Inputs
            inputs.acceuil.unit_pression = k.FTH20_A_PSI;
            inputs.acceuil.unit_pression_string = "psi";
        }
        private void unite_en_pascal()
        {
            //Interlock des cochages de boutons
            rBtn_Pascal.Checked = true;
            rBtn_ftH20.Checked = false;
            rBtn_mH20.Checked = false;
            rBtn_PSI.Checked = false;
            //Ajustement des Inputs
            inputs.acceuil.unit_pression = k.FTH20_A_KPA;
            inputs.acceuil.unit_pression_string = "kpa";
        }
        private void unite_en_USGPM()
        {
            //Interlock des cochages de boutons
            rBtn_USGPM.Checked = true;
            rBtn_m3h.Checked = false;
            //Ajustement des Inputs
            inputs.acceuil.unit_debit = k.USPGM;
            inputs.acceuil.unit_longueur_string = "usgpm";
        }
        private void unite_en_m3hr()
        {
            //Interlock des cochages de boutons
            rBtn_m3h.Checked = true;
            rBtn_USGPM.Checked = false;
            //Ajustement des Inputs
            inputs.acceuil.unit_debit = k.M3H_A_USGPM;
            inputs.acceuil.unit_debit_string = "m3/hr";
        }
        private void unite_en_pieds()
        {
            //Interlock des cochages de boutons
            rBtn_Pieds.Checked = true;
            rBtn_Metres.Checked = false;
            //Ajustement des Inputs
            inputs.acceuil.unit_longueur = k.PIEDS;
            inputs.acceuil.unit_longueur_string = "pieds";
        }
        private void unite_en_metres()
        {
            //Interlock des cochages de boutons
            rBtn_Metres.Checked = true;
            rBtn_Pieds.Checked = false;
            //Ajustement des Inputs
            inputs.acceuil.unit_longueur = k.METRES_A_PIEDS;
            inputs.acceuil.unit_longueur_string = "metres";
        }
        private void unite_en_hp()
        {
            //Interlock des cochages de boutons 
            rBtn_Hp.Checked = true;
            rBtn_Watts.Checked = false;
            //Ajustement des Inputs
            inputs.acceuil.unit_puissance = k.HP;
            inputs.acceuil.unit_puissance_string = "hp";
        }
        private void unite_en_watts()
        {
            //Interlock des cochages de boutons
            rBtn_Watts.Checked = true;
            rBtn_Hp.Checked = false;
            //Ajustement des Inputs
            inputs.acceuil.unit_puissance = k.WATTS;
            inputs.acceuil.unit_puissance_string = "watts";
        }

        //Retracage des courbes apres un changement d'unites
        private void changement_unites()
        {
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);

            retracage_pompe();

            for (int i = 1; i <= k.SECTION_E; i++)
            {
                if (inputs.section[i].actif)
                {
                    switch (i)
                    {
                        case k.SECTION_A: update_tableur_section_A(); break;
                        case k.SECTION_B: update_tableur_section_B(); break;
                        case k.SECTION_C: update_tableur_section_C(); break;
                        case k.SECTION_D: update_tableur_section_D(); break;
                        case k.SECTION_E: update_tableur_section_E(); break;
                    }
                }
            }
        }
        //Changement des textes indiquant les unites
        private void changement_texte_unites()
        {
            string buffer_X = "";
            string buffer_puiss = "";
            string buffer_eff = "";

            string unite_pression = "";
            string unite_debit = "";
            string unite_longueur = "";
            string unite_puissance = "";

            if (rBtn_Francais.Checked == true)
            {
                buffer_X = "Debit";
                buffer_puiss = "Puissance";
                buffer_eff = "Efficacite (%)";
            }
            else if (rBtn_English.Checked == true)
            {
                buffer_X = "FLow";
                buffer_puiss = "Power";
                buffer_eff = "Effiency (%)";
            }

            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].Axes[1].Title =
                    buffer_eff;


            if (rBtn_USGPM.Checked == true)
            {
                unite_debit = "USGPM";
            }
            else if (rBtn_m3h.Checked)
            {
                unite_debit = "m3/hr";
            }
            //Concatenation du titre anglais ou francais suivi de l'unite
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].Axes[0].Title =
                                String.Concat(buffer_X, " (", unite_debit, ")");
            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].Axes[0].Title =
                                String.Concat(buffer_X, " (", unite_debit, ")");
            graphique_1.ChartAreas[k.GRAPH_TROIS].Axes[0].Title =
                                String.Concat(buffer_X, " (", unite_debit, ")");

            if (rBtn_Hp.Checked == true)
            {
                unite_puissance = "Hp";
            }
            else if (rBtn_Watts.Checked == true)
            {
                unite_puissance = "Watts";
            }

            graphique_1.ChartAreas[k.GRAPH_TROIS].Axes[1].Title =
                String.Concat(buffer_puiss, " (", unite_puissance, ")");

            if (rBtn_ftH20.Checked == true)
            {
                unite_pression = "fhH20";
            }
            else if (rBtn_mH20.Checked == true)
            {
                unite_pression = "mH20";
            }
            else if (rBtn_PSI.Checked == true)
            {
                unite_pression = "PSI";
            }
            else if (rBtn_Pascal.Checked == true)
            {
                unite_pression = "kPa";
            }
            graphique_1.ChartAreas[0].Axes[1].Title =
                                    String.Concat("TDH (", unite_pression, ")");

            if (rBtn_Metres.Checked == true)
            {
                unite_longueur = "m";
            }
            else if (rBtn_Pieds.Checked == true)
            {
                unite_longueur = "ft";
            }

            lbl_UnitLong1.Text = unite_longueur;
            lbl_UnitLong2.Text = unite_longueur;
            lbl_UnitLong3.Text = unite_longueur;
            lbl_UnitLong4.Text = unite_longueur;
            lbl_UnitLong5.Text = unite_longueur;
            lbl_UnitStatic1.Text = unite_longueur;
            lbl_UnitStatic2.Text = unite_longueur;
            lbl_UnitStatic3.Text = unite_longueur;
            lbl_UnitStatic4.Text = unite_longueur;
            lbl_UnitStatic5.Text = unite_longueur;

            pnl_t_1.Text = String.Concat(buffer_X, "(", unite_debit, ")");
            pnl_t_2.Text = String.Concat("TDH A (", unite_pression, ")");
            pnl_t_3.Text = String.Concat("TDH B (", unite_pression, ")");
            pnl_t_4.Text = String.Concat("TDH C (", unite_pression, ")");
            pnl_t_5.Text = String.Concat("TDH D (", unite_pression, ")");
            pnl_t_6.Text = String.Concat("TDH E (", unite_pression, ")");
            pnl_t_tot.Text = String.Concat("TDH TOTAL (", unite_pression, ")");

            activer_legende(k.SERIE_POINT_ACTION_UN);
            activer_legende(k.SERIE_POINT_ACTION_DEUX);
            activer_legende(k.SERIE_POINT_ACTION_TROIS);
        }
        #endregion

        #region Changement de langue
        //Bouton de selection pour mettre le logiciel en francais
        private void rBtn_Francais_Click(object sender, EventArgs e)
        {
            if (rBtn_Francais.Checked == false)
            {
                traduction_francais();
            }
        }
        //Bouton de selection pour mettre le logiciel en anglais
        private void rBtn_English_Click(object sender, EventArgs e)
        {
            if (rBtn_English.Checked == false)
            {
                traduction_anglais();
            }
        }
        private void traduction_francais()
        {
            rBtn_English.Checked = false;
            rBtn_Francais.Checked = true;
            inputs.acceuil.langue = "francais";

            pic_Logo_Acceuil_FR.BringToFront();
            pic_Logo_graph_FR.BringToFront();

            langage(false);

            ajustement_pnl_materiel_type();
            ajustement_boutons();
            ajustement_lbl();
        }
        private void traduction_anglais()
        {
            rBtn_Francais.Checked = false;
            rBtn_English.Checked = true;
            inputs.acceuil.langue = "english";

            pic_Logo_AcceuiL_EN.BringToFront();
            pic_Logo_graph_EN.BringToFront();

            langage(true);

            ajustement_pnl_materiel_type();
            ajustement_boutons();
            ajustement_lbl();
        }


        //Contenu des indications et texte anglais/francais
        private void langage(bool anglais)
        {
            //Page accueil
            string textCLientinfo = "Informations sur le projet"; ;
            string textCLientnom = "Nom du client";
            string textClientprojet = "Projet";
            string textCLientdone = "Fait par : ";
            string textClientSauvegarde = "Enregistrer  \n Sous     ";
            string textClientOuvrir = "Ouvrir un dossier";
            //Page Action
            string textActionDebitTot = "Debit (Maximum de l'axe des X)";
            string textActionDebit = "Debit";
            string textActionAjouter = "Ajouter des points d'action";
            string textActionDebitX = "Debit (X)";
            string textActionDroite = "Droite \n Verticale";
            string textActionLegendeAuto = "Legende \n Auto";
            string textActionLegende = "Point d'action 1";
            string textBoutonGraphiqueSecondaire = "Activer les graphiques \n Efficacite et Puissance";
            //Page Materiel
            string textSystemActif = "Actif";
            string textSystemNonActif = "Non Actif";
            string textSystemMateriel = "Materiel";
            string textSystemPipeType = "Type de tuyau";
            string textSystemLignes = "Lignes Paralleles";
            string textSystemLong = "Longueur de section";
            string textSystemStatic = "Tete statique";
            string textSystemFacteur = "Facteur de securite (%)";
            //Page pompe
            string textPompeMarque = "Marque";
            string textPompeModele = "Modele";
            string textPompeNbPompe = "Nombre de pompe";
            string textPompePompe = "Pompe";
            string textPompeValidation = "Tracage";
            string textPompeVoirValeurs = "Voir tableau de \n valeurs";
            //Graphique
            string textGraphiqueTitre =
                "HAUTEUR MANOMETRIQUE TOTAL \n EN FONCTION DU DEBIT";
            string textLegendSystem = "Systeme";

            //  string textGraphiqueTitreAxeX = "Debit";
            //ControlTab
            string textTabAcceuil = "Acceuil et Informations Client";
            string textTabAction =
                "Point d'Action, Options et Affichage Graphique";
            string textTabTuyauterie = "Tuyauterie Systeme";
            string textTabPompe = "Pompe et Disposition";
            string textTagNom = "Client";
            string textTagProjet = "Projet";
            string textFaitPar = "Fait Par";
            string textDate = "Date et Revision";



            if (anglais)
            {
                textCLientinfo = "Informations about the projet"; ;
                textCLientnom = "Customer's name";
                textClientprojet = "Project";
                textCLientdone = "Done by : ";
                textClientSauvegarde = "Save As    ";
                textClientOuvrir = "Open a folder  ";
                textActionDebitTot = "FLow (Maximum X axis)";
                textActionDebit = "Flow";
                textActionAjouter = "Add duty points";
                textActionDebitX = "FLow (X)";
                textActionDroite = "Vertical \n Line";
                textActionLegendeAuto = "Legend \n Auto";
                textActionLegende = "Duty Point 1";
                textBoutonGraphiqueSecondaire = "Activate Efficiency and Power Chart";
                textSystemActif = "Active";
                textSystemNonActif = "Not Active";
                textSystemMateriel = "Material";
                textSystemPipeType = "Pipe Type";
                textSystemLignes = "Parallel Lines";
                textSystemLong = "Section Lenght";
                textSystemStatic = "Static Head";
                textSystemFacteur = "Safety Factor (%)";
                textPompeMarque = "Brand";
                textPompeModele = "Model";
                textPompeNbPompe = "Number of pump";
                textPompePompe = "Pump";
                textPompeValidation = "Drawing";
                textPompeVoirValeurs = "Show table \n of values";
                textGraphiqueTitre =
                    "TOTAL DISCHARGE HEAD IN \n IN FUNCTION OF FLOW RATE";

                textLegendSystem = "System";
                //textGraphiqueTitreAxeX = "Flow";
                textTabAcceuil = "Home and Customer Information";
                textTabAction = "Duty Point, Options and Chart Display";
                textTabTuyauterie = "System Piping";
                textTabPompe = "Pump and disposal";
                textTagNom = "Customer";
                textTagProjet = "Project";
                textFaitPar = "Done By";
                textDate = "Date and Revision";
            }

            //Page Acceuil
            lbl_client_info.Text = textCLientinfo;
            lbl_nom.Text = textCLientnom;
            lbl_projet.Text = textClientprojet;
            lbl_done.Text = textCLientdone;
            btn_EnregistrerSous.Text = textClientSauvegarde;
            btn_ouvrir_fichier.Text = textClientOuvrir;
            //Page Action
            lbl_debit_tot.Text = textActionDebitTot;
            lbl_ajouter_action.Text = textActionAjouter;
            lbl_debit_X.Text = textActionDebitX;
            lbl_debit_X_tot.Text = textActionDebit;
            lbl_droite.Text = textActionDroite;
            lbl_legende_auto.Text = textActionLegendeAuto;
            if (checkBox_Legend_Auto1.Checked)
            {
                textBox_Action1.Text = textActionLegende;
            }
            if (checkBox_Legend_Auto2.Checked)
            {
                textBox_Action2.Text = textActionLegende;
            }
            if (checkBox_Legend_Auto3.Checked)
            {
                textBox_Action3.Text = textActionLegende;
            }
            btn_Graph_Principal.Text = textBoutonGraphiqueSecondaire;
            //Page Tuyauterie
            /*Si la section est active(checked), le message est Actif, sinon Non Actif*/
            checkBox_Active1.Text = (checkBox_Active1.Checked)
                                        ? textSystemActif : textSystemNonActif;
            checkBox_Active2.Text = (checkBox_Active2.Checked)
                                        ? textSystemActif : textSystemNonActif;
            checkBox_Active3.Text = (checkBox_Active3.Checked)
                                        ? textSystemActif : textSystemNonActif;
            checkBox_Active4.Text = (checkBox_Active4.Checked)
                                        ? textSystemActif : textSystemNonActif;
            checkBox_Active5.Text = (checkBox_Active5.Checked)
                                        ? textSystemActif : textSystemNonActif;
            pnl_t_1.Text = textActionDebit;
            lbl_Materiel1.Text = textSystemMateriel;
            lbl_Materiel2.Text = textSystemMateriel;
            lbl_Materiel3.Text = textSystemMateriel;
            lbl_Materiel4.Text = textSystemMateriel;
            lbl_Materiel5.Text = textSystemMateriel;
            lbl_PipeType1.Text = textSystemPipeType;
            lbl_PipeType2.Text = textSystemPipeType;
            lbl_PipeType3.Text = textSystemPipeType;
            lbl_PipeType4.Text = textSystemPipeType;
            lbl_PipeType5.Text = textSystemPipeType;
            lbl_lignes_par1.Text = textSystemLignes;
            lbl_lignes_par2.Text = textSystemLignes;
            lbl_lignes_par3.Text = textSystemLignes;
            lbl_lignes_par4.Text = textSystemLignes;
            lbl_lignes_par5.Text = textSystemLignes;
            lbl_long_sect1.Text = textSystemLong;
            lbl_long_sect2.Text = textSystemLong;
            lbl_long_sect3.Text = textSystemLong;
            lbl_long_sect4.Text = textSystemLong;
            lbl_long_sect5.Text = textSystemLong;
            lbl_static_head1.Text = textSystemStatic;
            lbl_static_head2.Text = textSystemStatic;
            lbl_static_head3.Text = textSystemStatic;
            lbl_static_head4.Text = textSystemStatic;
            lbl_static_head5.Text = textSystemStatic;
            //Page pompe
            lbl_facteur1.Text = textSystemFacteur;
            lbl_facteur2.Text = textSystemFacteur;
            lbl_facteur3.Text = textSystemFacteur;
            lbl_facteur4.Text = textSystemFacteur;
            lbl_facteur5.Text = textSystemFacteur;
            lbl_marque1.Text = textPompeMarque;
            lbl_marque2.Text = textPompeMarque;
            lbl_marque3.Text = textPompeMarque;
            lbl_marque4.Text = textPompeMarque;
            lbl_marque5.Text = textPompeMarque;
            lbl_marque6.Text = textPompeMarque;
            lbl_marque7.Text = textPompeMarque;
            lbl_marque8.Text = textPompeMarque;
            lbl_marque9.Text = textPompeMarque;
            lbl_marque10.Text = textPompeMarque;
            lbl_marque11.Text = textPompeMarque;
            lbl_marque12.Text = textPompeMarque;
            lbl_modele1.Text = textPompeModele;
            lbl_modele2.Text = textPompeModele;
            lbl_modele3.Text = textPompeModele;
            lbl_modele4.Text = textPompeModele;
            lbl_modele5.Text = textPompeModele;
            lbl_modele6.Text = textPompeModele;
            lbl_modele7.Text = textPompeModele;
            lbl_modele8.Text = textPompeModele;
            lbl_modele9.Text = textPompeModele;
            lbl_modele10.Text = textPompeModele;
            lbl_modele11.Text = textPompeModele;
            lbl_modele12.Text = textPompeModele;
            lbl_nb_pompe1.Text = textPompeNbPompe;
            lbl_nb_pompe2.Text = textPompeNbPompe;
            lbl_nb_pompe3.Text = textPompeNbPompe;
            lbl_nb_pompe4.Text = textPompeNbPompe;
            lbl_nb_pompe5.Text = textPompeNbPompe;
            lbl_nb_pompe6.Text = textPompeNbPompe;
            lbl_nb_pompe7.Text = textPompeNbPompe;
            lbl_nb_pompe8.Text = textPompeNbPompe;
            lbl_nb_pompe9.Text = textPompeNbPompe;
            lbl_nb_pompe10.Text = textPompeNbPompe;
            lbl_nb_pompe11.Text = textPompeNbPompe;
            lbl_nb_pompe12.Text = textPompeNbPompe;
            lbl_pompe1.Text = String.Concat(textPompePompe, " 1");
            lbl_pompe2.Text = String.Concat(textPompePompe, " 2");
            lbl_pompe3.Text = String.Concat(textPompePompe, " 3");
            lbl_pompe4.Text = String.Concat(textPompePompe, " 4");
            lbl_pompe5.Text = String.Concat(textPompePompe, " 5");
            lbl_pompe6.Text = String.Concat(textPompePompe, " 6");
            lbl_pompe7.Text = String.Concat(textPompePompe, " 7");
            lbl_pompe8.Text = String.Concat(textPompePompe, " 8");
            lbl_pompe9.Text = String.Concat(textPompePompe, " 9");
            lbl_pompe10.Text = String.Concat(textPompePompe, " 10");
            lbl_pompe11.Text = String.Concat(textPompePompe, " 11");
            lbl_pompe12.Text = String.Concat(textPompePompe, " 12");
            btn_Validation_1.Text = textPompeValidation;
            btn_Validation_2.Text = textPompeValidation;
            btn_Validation_3.Text = textPompeValidation;
            btn_Validation_4.Text = textPompeValidation;
            btn_Validation_5.Text = textPompeValidation;
            btn_Validation_6.Text = textPompeValidation;
            btn_Validation_7.Text = textPompeValidation;
            btn_Validation_8.Text = textPompeValidation;
            btn_Validation_9.Text = textPompeValidation;
            btn_Validation_10.Text = textPompeValidation;
            btn_Validation_11.Text = textPompeValidation;
            btn_Validation_12.Text = textPompeValidation;
            btn_voir_valeurs1.Text = textPompeVoirValeurs;
            btn_voir_valeurs2.Text = textPompeVoirValeurs;
            btn_voir_valeurs3.Text = textPompeVoirValeurs;
            btn_voir_valeurs4.Text = textPompeVoirValeurs;
            btn_voir_valeurs5.Text = textPompeVoirValeurs;
            btn_voir_valeurs6.Text = textPompeVoirValeurs;
            btn_voir_valeurs7.Text = textPompeVoirValeurs;
            btn_voir_valeurs8.Text = textPompeVoirValeurs;
            btn_voir_valeurs9.Text = textPompeVoirValeurs;
            btn_voir_valeurs10.Text = textPompeVoirValeurs;
            btn_voir_valeurs11.Text = textPompeVoirValeurs;
            btn_voir_valeurs12.Text = textPompeVoirValeurs;
            //Graphique
            lbl_titre_graph.Text = textGraphiqueTitre;

            lbl_tag_Nom.Text = textTagNom;
            lbl_tag_Projet.Text = textTagProjet;
            lbl_tag_FaitPar.Text = textFaitPar;
            lbl_tag_Date.Text = textDate;
            graphique_1.Series[k.SERIE_TUYAUTERIE].Name = textLegendSystem;
            changement_texte_unites();
            //Control Tab
            tab_Control.TabPages[k.PAGE_ACCUEIL].Text = textTabAcceuil;
            tab_Control.TabPages[k.PAGE_ACTION].Text = textTabAction;
            tab_Control.TabPages[k.PAGE_TUYAUTERIE].Text = textTabTuyauterie;
            tab_Control.TabPages[k.PAGE_POMPE].Text = textTabPompe;
        }
        #endregion


        /*==================PAGE POINT ACTIONS===============================*/
        #region Encapsulation Page Point D'Action
        private void nUD_Debit_ValueChanged(object sender, EventArgs e)
        {
            inputs.action.debit[k.DEBIT_AXE_X] = (int)nUD_Debit.Value;

            graphique_1.ChartAreas[0].Axes[0].Interval =
                   Math.Round(
                       inputs.action.debit[k.DEBIT_AXE_X] /
                            (int)nUD_Interval_X.Value, 0);

            graphique_1.ChartAreas[1].Axes[0].Interval = graphique_1.ChartAreas[0].Axes[0].Interval;

            graphique_1.ChartAreas[2].Axes[0].Interval = graphique_1.ChartAreas[0].Axes[0].Interval;


            intervalles_debit =
                calcul.intervalle_debit(
                    calcul.intervalle_en_x(
                        inputs.action.debit[k.DEBIT_AXE_X],
                            k.PROPORTION_DIX),
                                k.NB_POINTS_TABLEUR_INTERVALLE);

            //Update des intervalles dans la premiere colonne du tableur
            update_tableur_section_GPM();

            //Si au moins une section est remplie, update du tableur
            if (inputs.section[k.SECTION_A].actif
                    || inputs.section[k.SECTION_B].actif
                        || inputs.section[k.SECTION_C].actif
                            || inputs.section[k.SECTION_D].actif
                                || inputs.section[k.SECTION_E].actif)
            {
                update_tableur();
            }
            //Modification de la valeur maximale du graphique sur l'axe des X
            graphique_1.ChartAreas[0].AxisX.Maximum =
                                            inputs.action.debit[k.DEBIT_AXE_X];
            graphique_1.ChartAreas[1].AxisX.Maximum =
                                            inputs.action.debit[k.DEBIT_AXE_X];
            graphique_1.ChartAreas[2].AxisX.Maximum =
                                            inputs.action.debit[k.DEBIT_AXE_X];
        }
        //Numeric up/down des debits
        private void nUD_X_action1_ValueChanged(object sender, EventArgs e)
        {
            inputs.action.debit[k.DEBIT_P1] = (int)nUD_X_action1.Value;
            tracer_point_action(k.SERIE_POINT_ACTION_UN);
            activer_legende(k.SERIE_POINT_ACTION_UN);
        }
        private void nUD_X_action2_ValueChanged(object sender, EventArgs e)
        {
            inputs.action.debit[k.DEBIT_P2] = (int)nUD_X_action2.Value;
            tracer_point_action(k.SERIE_POINT_ACTION_DEUX);
            activer_legende(k.SERIE_POINT_ACTION_DEUX);
        }
        private void nUD_X_action3_ValueChanged(object sender, EventArgs e)
        {
            inputs.action.debit[k.DEBIT_P3] = (int)nUD_X_action3.Value;
            tracer_point_action(k.SERIE_POINT_ACTION_TROIS);
            activer_legende(k.SERIE_POINT_ACTION_TROIS);
        }
        //Numeric up/down des pressions
        private void nUD_Y_action1_ValueChanged(object sender, EventArgs e)
        {
            inputs.action.pression[k.PRESSION_P1] = (int)nUD_Y_action1.Value;
            tracer_point_action(k.SERIE_POINT_ACTION_UN);
            activer_legende(k.SERIE_POINT_ACTION_UN);
        }
        private void nUD_Y_action2_ValueChanged(object sender, EventArgs e)
        {
            inputs.action.pression[k.PRESSION_P2] = (int)nUD_Y_action2.Value;
            tracer_point_action(k.SERIE_POINT_ACTION_DEUX);
            activer_legende(k.SERIE_POINT_ACTION_DEUX);
        }
        private void nUD_Y_action3_ValueChanged(object sender, EventArgs e)
        {
            inputs.action.pression[k.PRESSION_P3] = (int)nUD_Y_action3.Value;
            tracer_point_action(k.SERIE_POINT_ACTION_TROIS);
            activer_legende(k.SERIE_POINT_ACTION_TROIS);
        }
        //CheckBox Droite
        private void checkBox_Droite1_Click(object sender, EventArgs e)
        {
            if (checkBox_Droite1.Checked == false)
            {
                droite(k.DROITE_P1);
            }
        }
        private void checkBox_Droite2_Click(object sender, EventArgs e)
        {
            if (checkBox_Droite2.Checked == false)
            {
                droite(k.DROITE_P2);
            }
        }
        private void checkBox_Droite3_Click(object sender, EventArgs e)
        {
            if (checkBox_Droite3.Checked == false)
            {
                droite(k.DROITE_P3);
            }
        }

        private void droite(int serie_action)
        {
            if (serie_action == k.DROITE_P1)
            {
                checkBox_Point1.Checked = false;
                checkBox_Droite1.Checked = true;
            }
            else if (serie_action == k.DROITE_P2)
            {
                checkBox_Point2.Checked = false;
                checkBox_Droite2.Checked = true;
            }
            else if (serie_action == k.DROITE_P3)
            {
                checkBox_Point3.Checked = false;
                checkBox_Droite3.Checked = true;
            }
            inputs.action.droite[serie_action] = k.ACTION_DROITE;
            tracer_point_action(serie_action);
        }

        private void point(int serie_action)
        {
            if (serie_action == k.DROITE_P1)
            {
                checkBox_Droite1.Checked = false;
                checkBox_Point1.Checked = true;
            }
            else if (serie_action == k.DROITE_P2)
            {
                checkBox_Droite2.Checked = false;
                checkBox_Point2.Checked = true;
            }
            else if (serie_action == k.DROITE_P3)
            {
                checkBox_Droite3.Checked = false;
                checkBox_Point3.Checked = true;
            }

            inputs.action.droite[serie_action] = k.ACTION_POINT;
            tracer_point_action(serie_action);
        }

        //CheckBox Point
        private void checkBox_Point1_Click(object sender, EventArgs e)
        {
            if (checkBox_Point1.Checked == false)
            {
                point(k.DROITE_P1);
            }
        }
        private void checkBox_Point2_Click(object sender, EventArgs e)
        {
            if (checkBox_Point2.Checked == false)
            {
                point(k.DROITE_P2);
            }
        }
        private void checkBox_Point3_Click(object sender, EventArgs e)
        {
            if (checkBox_Point3.Checked == false)
            {
                point(k.DROITE_P3);
            }
        }
        //Text Box contenant la legende manuelle
        private void textBox_Action1_TextChanged(object sender, EventArgs e)
        {
            inputs.action.legende[k.LEGENDE_P1] = textBox_Action1.Text;
            if (inputs.action.legende_auto[k.LEGENDE_P1] == true)
            {
                checkBox_Legend_Auto1.Checked = false;
            }
            else
            {
                activer_legende(k.SERIE_POINT_ACTION_UN);
            }
        }
        private void textBox_Action2_TextChanged(object sender, EventArgs e)
        {
            inputs.action.legende[k.LEGENDE_P2] = textBox_Action2.Text;
            if (inputs.action.legende_auto[k.LEGENDE_P2] == true)
            {
                checkBox_Legend_Auto2.Checked = false;
            }
            else
            {
                activer_legende(k.SERIE_POINT_ACTION_DEUX);
            }
        }
        private void textBox_Action3_TextChanged(object sender, EventArgs e)
        {
            inputs.action.legende[k.LEGENDE_P3] = textBox_Action3.Text;
            if (inputs.action.legende_auto[k.LEGENDE_P3] == true)
            {
                checkBox_Legend_Auto3.Checked = false;
            }
            else
            {
                activer_legende(k.SERIE_POINT_ACTION_TROIS);
            }
        }
        //Check Box pour selectionner le mode d'ecriture de la legende des points d'action
        private void checkBox_Legend_Auto1_CheckedChanged(object sender, EventArgs e)
        {
            inputs.action.legende_auto[k.LEGENDE_P1] =
                                                checkBox_Legend_Auto1.Checked;
            activer_legende(k.SERIE_POINT_ACTION_UN);
        }
        private void checkBox_Legend_Auto2_CheckedChanged(object sender, EventArgs e)
        {
            inputs.action.legende_auto[k.LEGENDE_P2] =
                                                checkBox_Legend_Auto2.Checked;
            activer_legende(k.SERIE_POINT_ACTION_DEUX);
        }
        private void checkBox_Legend_Auto3_CheckedChanged(object sender, EventArgs e)
        {
            inputs.action.legende_auto[k.LEGENDE_P3] =
                                                checkBox_Legend_Auto3.Checked;
            activer_legende(k.SERIE_POINT_ACTION_TROIS);
        }
        //Activation de la courbe de point d'action
        private void btn_On_Action1_Click(object sender, EventArgs e)
        {
            tracer_point_action(k.SERIE_POINT_ACTION_UN);
        }
        private void btn_On_Action2_Click(object sender, EventArgs e)
        {
            tracer_point_action(k.SERIE_POINT_ACTION_DEUX);
        }
        private void btn_On_Action3_Click(object sender, EventArgs e)
        {
            tracer_point_action(k.SERIE_POINT_ACTION_TROIS);
        }
        //Desactivation de la courbe de point d'action
        private void btn_Off_Action1_Click(object sender, EventArgs e)
        {
            raz_serie(k.SERIE_POINT_ACTION_UN);
            desactiver_legende(k.SERIE_POINT_ACTION_UN);
        }
        private void btn_Off_Action2_Click(object sender, EventArgs e)
        {
            raz_serie(k.SERIE_POINT_ACTION_DEUX);
            desactiver_legende(k.SERIE_POINT_ACTION_DEUX);
        }
        private void btn_Off_Action3_Click(object sender, EventArgs e)
        {
            raz_serie(k.SERIE_POINT_ACTION_TROIS);
            desactiver_legende(k.SERIE_POINT_ACTION_TROIS);
        }
        #endregion


        /*==================PAGE TUYAUTEREIE=================================*/
        #region Encapsulation Page Tuyauterie
        //Activation d'une section.  Elle est donc prise en compte pour le TDH
        private void activation_section(int section)
        {
            string actif = "Actif";

            //Message dependant de la langue selectionnee a l'acceuil
            if (rBtn_English.Checked)
            {
                actif = "Active";
            }

            if (graphique_1.Series[k.SERIE_TUYAUTERIE].Enabled == false)
            {
                activer_legende(k.SERIE_TUYAUTERIE);
            }

            sections_actives[section] = true;
            inputs.section[section].actif = true;

            switch (section)
            {
                case k.SECTION_A:
                    checkBox_Active1.Text = actif;
                    checkBox_Active1.ForeColor = Color.PaleGreen;
                    checkBox_Active1.Checked = true;
                    if (materiel_select[k.SECTION_A] && type_select[k.SECTION_A])
                    {
                        update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                        update_tableur_section_A();
                    }
                    break;
                case k.SECTION_B:
                    checkBox_Active2.Text = actif;
                    checkBox_Active2.ForeColor = Color.PaleGreen;
                    checkBox_Active2.Checked = true;
                    if (materiel_select[k.SECTION_B] && type_select[k.SECTION_B])
                    {
                        update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                        update_tableur_section_B();
                    }
                    break;
                case k.SECTION_C:
                    checkBox_Active3.Text = actif;
                    checkBox_Active3.ForeColor = Color.PaleGreen;
                    checkBox_Active3.Checked = true;
                    if (materiel_select[k.SECTION_C] && type_select[k.SECTION_C])
                    {
                        update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                        update_tableur_section_C();
                    }
                    break;
                case k.SECTION_D:
                    checkBox_Active4.Text = actif;
                    checkBox_Active4.ForeColor = Color.PaleGreen;
                    checkBox_Active4.Checked = true;
                    if (materiel_select[k.SECTION_D] && type_select[k.SECTION_D])
                    {
                        update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                        update_tableur_section_D();
                    }
                    break;
                case k.SECTION_E:
                    checkBox_Active5.Text = actif;
                    checkBox_Active5.ForeColor = Color.PaleGreen;
                    checkBox_Active5.Checked = true;
                    if (materiel_select[k.SECTION_E] && type_select[k.SECTION_E])
                    {
                        update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                        update_tableur_section_E();
                    }
                    break;
            }
        }
        //Desactivation d'un section.  Elle n'est plus prise en compte pour le TDH
        private void desactivation_section(int section)
        {
            string NonActif = "Non Actif";

            //Message dependant de la langue entree a l'acceuil
            if (rBtn_English.Checked)
            {
                NonActif = "Not Active";
            }

            sections_actives[section] = false;
            inputs.section[section].actif = false;

            //Si tous les checkBox sont unchecked, retrait de la courbe et de 
            //la legende Systeme
            if (inputs.section[k.SECTION_A].actif == false &&
                inputs.section[k.SECTION_B].actif == false &&
                inputs.section[k.SECTION_C].actif == false &&
                inputs.section[k.SECTION_D].actif == false &&
                inputs.section[k.SECTION_E].actif == false)
            {
                desactiver_legende(k.SERIE_TUYAUTERIE);
            }

            switch (section)
            {
                case k.SECTION_A:
                    checkBox_Active1.Text = NonActif;
                    checkBox_Active1.ForeColor = Color.LightCoral;
                    checkBox_Active1.Checked = false;
                    update_tableur_section_A();
                    break;
                case k.SECTION_B:
                    checkBox_Active2.Text = NonActif;
                    checkBox_Active2.ForeColor = Color.LightCoral;
                    update_tableur_section_B();
                    break;
                case k.SECTION_C:
                    checkBox_Active3.Text = NonActif;
                    checkBox_Active3.ForeColor = Color.LightCoral;
                    update_tableur_section_C();
                    break;
                case k.SECTION_D:
                    checkBox_Active4.Text = NonActif;
                    checkBox_Active4.ForeColor = Color.LightCoral;
                    update_tableur_section_D();
                    break;
                case k.SECTION_E:
                    checkBox_Active5.Text = NonActif;
                    checkBox_Active5.ForeColor = Color.LightCoral;
                    update_tableur_section_E();
                    break;
            }

            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
        }
        //Affichage des coordonnees des points de courbe de pompe
        private void affichage_valeurs(int num_pompe, int i, double X, double Y)
        {
            //Inscription des coordonnees du point selon le numero de pompe
            switch (num_pompe)
            {
                case k.POMPE_1:
                    listView1.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView1.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_2:
                    listView2.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView2.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_3:
                    listView1.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView3.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_4:
                    listView4.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView4.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_5:
                    listView5.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView5.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_6:
                    listView6.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView6.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_7:
                    listView7.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView7.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_8:
                    listView8.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView8.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_9:
                    listView9.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView9.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_10:
                    listView10.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView10.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_11:
                    listView11.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView11.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
                case k.POMPE_12:
                    listView12.Items[k.LIGNE_X].SubItems[i].Text = X.ToString();
                    listView12.Items[k.LIGNE_Y].SubItems[i].Text = Y.ToString();
                    break;
            }
        }


        private void description_listview(int num_pompe)
        {
            if (inputs.pompes[num_pompe].marque != null
                    && inputs.pompes[num_pompe].serie != null
                        && inputs.pompes[num_pompe].modele != null)
            {

                //Mise en evidence du listview une fois rempli et affichage de 
                //la marque et du modele de la pompe
                switch (num_pompe)
                {
                    case k.POMPE_1:
                        pnl_tab_valeurs1.BringToFront();
                        lbl_valeurs_marque1.Text =
                                            cBox_Marque1.SelectedItem.ToString();
                        lbl_valeurs_modele1.Text =
                                            cBox_Modele1.SelectedItem.ToString();
                        break;
                    case k.POMPE_2:
                        pnl_tab_valeurs2.BringToFront();
                        lbl_valeurs_marque2.Text =
                                            cBox_Marque2.SelectedItem.ToString();
                        lbl_valeurs_modele2.Text =
                                            cBox_Modele2.SelectedItem.ToString();
                        break;
                    case k.POMPE_3:
                        pnl_tab_valeurs3.BringToFront();
                        lbl_valeurs_marque3.Text =
                                            cBox_Marque3.SelectedItem.ToString();
                        lbl_valeurs_modele3.Text =
                                            cBox_Modele3.SelectedItem.ToString();
                        break;
                    case k.POMPE_4:
                        pnl_tab_valeurs4.BringToFront();
                        lbl_valeurs_marque4.Text =
                                            cBox_Marque4.SelectedItem.ToString();
                        lbl_valeurs_modele4.Text =
                                            cBox_Modele4.SelectedItem.ToString();
                        break;
                    case k.POMPE_5:
                        pnl_tab_valeurs5.BringToFront();
                        lbl_valeurs_marque5.Text =
                                            cBox_Marque5.SelectedItem.ToString();
                        lbl_valeurs_modele5.Text =
                                            cBox_Modele5.SelectedItem.ToString();
                        break;
                    case k.POMPE_6:
                        pnl_tab_valeurs6.BringToFront();
                        lbl_valeurs_marque6.Text =
                                            cBox_Marque6.SelectedItem.ToString();
                        lbl_valeurs_modele6.Text =
                                            cBox_Modele6.SelectedItem.ToString();
                        break;
                    case k.POMPE_7:
                        pnl_tab_valeurs7.BringToFront();
                        lbl_valeurs_marque7.Text =
                                            cBox_Marque7.SelectedItem.ToString();
                        lbl_valeurs_modele7.Text =
                                            cBox_Modele7.SelectedItem.ToString();
                        break;
                    case k.POMPE_8:
                        pnl_tab_valeurs8.BringToFront();
                        lbl_valeurs_marque8.Text =
                                            cBox_Marque8.SelectedItem.ToString();
                        lbl_valeurs_modele8.Text =
                                            cBox_Modele8.SelectedItem.ToString();
                        break;
                    case k.POMPE_9:
                        pnl_tab_valeurs9.BringToFront();
                        lbl_valeurs_marque9.Text =
                                            cBox_Marque9.SelectedItem.ToString();
                        lbl_valeurs_modele9.Text =
                                            cBox_Modele9.SelectedItem.ToString();
                        break;
                    case k.POMPE_10:
                        pnl_tab_valeurs10.BringToFront();
                        lbl_valeurs_marque10.Text =
                                            cBox_Marque10.SelectedItem.ToString();
                        lbl_valeurs_modele10.Text =
                                            cBox_Modele10.SelectedItem.ToString();
                        break;
                    case k.POMPE_11:
                        pnl_tab_valeurs11.BringToFront();
                        lbl_valeurs_marque11.Text =
                                            cBox_Marque11.SelectedItem.ToString();
                        lbl_valeurs_modele11.Text =
                                            cBox_Modele11.SelectedItem.ToString();
                        break;
                    case k.POMPE_12:
                        pnl_tab_valeurs12.BringToFront();
                        lbl_valeurs_marque12.Text =
                                            cBox_Marque12.SelectedItem.ToString();
                        lbl_valeurs_modele12.Text =
                                            cBox_Modele12.SelectedItem.ToString();
                        break;
                }
                //Activation des points sur la courbe graphique
                activer_affichage_points_graphique(num_pompe);
            }
            else
            {
                MessageBox.Show("Aucune valeur a afficher");
            }

        }
        //Clique du panneau Titre, du checkBox Actif ou du texte dans le 
        //panneau, activant la section
        private void pnl_Titre1_Click(object sender, EventArgs e)
        {
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel1.SelectedIndex != k.VIDE
                    && cBox_Type1.SelectedIndex != k.VIDE
                        && nUD_Long1.Value != 0)
            {
                if (checkBox_Active1.Checked == false)
                {
                    checkBox_Active1.Checked = true;
                    activation_section(k.SECTION_A);
                }
                else
                {
                    checkBox_Active1.Checked = false;
                    desactivation_section(k.SECTION_A);
                }
                inputs.section[k.SECTION_A].actif = checkBox_Active1.Checked;

                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_A();
            }
        }
        private void pnl_Titre2_Click(object sender, EventArgs e)
        {
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel2.SelectedIndex != k.VIDE
                    && cBox_Type2.SelectedIndex != k.VIDE
                        && nUD_Long2.Value != 0)
            {
                if (checkBox_Active2.Checked == false)
                {
                    checkBox_Active2.Checked = true;
                    activation_section(k.SECTION_B);
                }
                else
                {
                    checkBox_Active2.Checked = false;
                    desactivation_section(k.SECTION_B);
                }
                inputs.section[k.SECTION_B].actif = checkBox_Active2.Checked;

                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_B();
            }
        }
        private void pnl_Titre3_Click(object sender, EventArgs e)
        {
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel3.SelectedIndex != k.VIDE
                    && cBox_Type3.SelectedIndex != k.VIDE
                        && nUD_Long3.Value != 0)
            {
                if (checkBox_Active3.Checked == false)
                {
                    checkBox_Active3.Checked = true;
                    activation_section(k.SECTION_C);
                }
                else
                {
                    checkBox_Active3.Checked = false;
                    desactivation_section(k.SECTION_C);
                }
                inputs.section[k.SECTION_C].actif = checkBox_Active3.Checked;

                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_C();
            }
        }
        private void pnl_Titre4_Click(object sender, EventArgs e)
        {
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel4.SelectedIndex != k.VIDE
                    && cBox_Type4.SelectedIndex != k.VIDE
                        && nUD_Long4.Value != 0)
            {
                if (checkBox_Active4.Checked == false)
                {
                    checkBox_Active4.Checked = true;
                    activation_section(k.SECTION_D);
                }
                else
                {
                    checkBox_Active4.Checked = false;
                    desactivation_section(k.SECTION_D);
                }
                inputs.section[k.SECTION_D].actif = checkBox_Active4.Checked;

                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_D();
            }
        }
        private void pnl_Titre5_Click(object sender, EventArgs e)
        {
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel5.SelectedIndex != k.VIDE
                    && cBox_Type5.SelectedIndex != k.VIDE
                        && nUD_Long5.Value != 0)
            {
                if (checkBox_Active5.Checked == false)
                {
                    checkBox_Active5.Checked = true;
                    activation_section(k.SECTION_E);
                }
                else
                {
                    checkBox_Active5.Checked = false;
                    desactivation_section(k.SECTION_E);
                }
                inputs.section[k.SECTION_E].actif = checkBox_Active5.Checked;

                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_E();
            }
        }
        //Combo Box Materiel
        private void cBox_materiel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_materiel1.SelectedIndex != k.VIDE)
            {
                //Obtention du materiel et sa constante HW d'apres un split du cBox.
                manip_string.update_tab_section_materiel(
                                                (string)cBox_materiel1.SelectedItem,
                                                    k.SECTION_A,
                                                        inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel1.SelectedIndex != k.VIDE
                    && cBox_Type1.SelectedIndex != k.VIDE
                        && nUD_Long1.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_A);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_A);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_A();
        }
        //Combo Box Materiel
        private void cBox_materiel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_materiel2.SelectedIndex != k.VIDE)
            {
                //Obtention du materiel et sa constante HW d'apres un split du cBox.
                manip_string.update_tab_section_materiel(
                                            (string)cBox_materiel2.SelectedItem,
                                                k.SECTION_B,
                                                    inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel2.SelectedIndex != k.VIDE
                    && cBox_Type2.SelectedIndex != k.VIDE
                        && nUD_Long2.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_B);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_B);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_B();
        }
        //Combo Box Materiel
        private void cBox_materiel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_materiel3.SelectedIndex != k.VIDE)
            {
                //Obtention du materiel et sa constante HW d'apres un split du cBox.
                manip_string.update_tab_section_materiel(
                                            (string)cBox_materiel3.SelectedItem,
                                                k.SECTION_C,
                                                    inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel3.SelectedIndex != k.VIDE
                    && cBox_Type3.SelectedIndex != k.VIDE
                        && nUD_Long3.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_C);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_C);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_C();
        }
        //Combo Box Materiel
        private void cBox_materiel4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_materiel4.SelectedIndex != k.VIDE)
            {
                //Obtention du materiel et sa constante HW d'apres un split du cBox.
                manip_string.update_tab_section_materiel(
                                            (string)cBox_materiel4.SelectedItem,
                                                k.SECTION_D,
                                                    inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel4.SelectedIndex != k.VIDE
                    && cBox_Type4.SelectedIndex != k.VIDE
                        && nUD_Long4.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_D);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_D);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_D();
        }
        //Combo Box Materiel
        private void cBox_materiel5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_materiel1.SelectedIndex != k.VIDE)
            {
                //Obtention du materiel et sa constante HW d'apres un split du cBox.
                manip_string.update_tab_section_materiel(
                                                (string)cBox_materiel5.SelectedItem,
                                                    k.SECTION_E,
                                                        inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel5.SelectedIndex != k.VIDE
                    && cBox_Type5.SelectedIndex != k.VIDE
                        && nUD_Long5.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_E);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_E);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_E();
        }
        //Combo Box Type de tuyau
        private void cBox_Type1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_Type1.SelectedIndex != k.VIDE)
            {
                //Obtention du type de tuyau et son diametre d'apres un split du cBox.
                manip_string.update_tab_section_type(
                                                (string)cBox_Type1.SelectedItem,
                                                    k.SECTION_A,
                                                        inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel1.SelectedIndex != k.VIDE
                    && cBox_Type1.SelectedIndex != k.VIDE
                        && nUD_Long1.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_A);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_A);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_A();
        }
        //Combo Box Type de tuyau
        private void cBox_Type2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_Type2.SelectedIndex != k.VIDE)
            {
                //Obtention du type de tuyau et son diametre d'apres un split du cBox.
                manip_string.update_tab_section_type(
                                                (string)cBox_Type2.SelectedItem,
                                                    k.SECTION_B,
                                                        inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel2.SelectedIndex != k.VIDE
                    && cBox_Type2.SelectedIndex != k.VIDE
                        && nUD_Long2.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_B);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_B);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_B();
        }
        //Combo Box Type de tuyau
        private void cBox_Type3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_Type3.SelectedIndex != k.VIDE)
            {
                //Obtention du type de tuyau et son diametre d'apres un split du cBox.
                manip_string.update_tab_section_type(
                                                (string)cBox_Type3.SelectedItem,
                                                    k.SECTION_C,
                                                        inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel3.SelectedIndex != k.VIDE
                    && cBox_Type3.SelectedIndex != k.VIDE
                        && nUD_Long3.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_C);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_C);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_C();
        }
        //Combo Box Type de tuyau
        private void cBox_Type4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_Type4.SelectedIndex != k.VIDE)
            {
                //Obtention du type de tuyau et son diametre d'apres un split du cBox.
                manip_string.update_tab_section_type(
                                                (string)cBox_Type4.SelectedItem,
                                                    k.SECTION_D,
                                                        inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel4.SelectedIndex != k.VIDE
                    && cBox_Type4.SelectedIndex != k.VIDE
                        && nUD_Long4.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_D);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_D);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_D();
        }
        //Combo Box Type de tuyau
        private void cBox_Type5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Si le resultat du changement n'est pas le vidage de la case (RAZ)
            if (cBox_Type5.SelectedIndex != k.VIDE)
            {
                //Obtention du type de tuyau et son diametre d'apres un split du cBox.
                manip_string.update_tab_section_type(
                                                (string)cBox_Type5.SelectedItem,
                                                    k.SECTION_E,
                                                        inputs);
            }
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel5.SelectedIndex != k.VIDE
                    && cBox_Type5.SelectedIndex != k.VIDE
                        && nUD_Long5.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_E);
            }
            //Si un des champs est manquant, la section est desactivee
            else
            {
                desactivation_section(k.SECTION_E);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_E();
        }

        private void nUD_LignesParr1_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du nombre de lignes paralleles dans le tableau Inputs
            inputs.section[k.SECTION_A].num_parallel_lines =
                                                    (int)nUD_LignesParr1.Value;
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel1.SelectedIndex != k.VIDE
                    && cBox_Type1.SelectedIndex != k.VIDE
                        && nUD_Long1.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_A);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_A();
            }
        }
        private void nUD_LignesParr2_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du nombre de lignes paralleles dans le tableau Inputs
            inputs.section[k.SECTION_B].num_parallel_lines =
                                                    (int)nUD_LignesParr2.Value;
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel2.SelectedIndex != k.VIDE
                    && cBox_Type2.SelectedIndex != k.VIDE
                        && nUD_Long2.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_B);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_B();
            }
        }
        private void nUD_LignesParr3_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du nombre de lignes paralleles dans le tableau Inputs
            inputs.section[k.SECTION_C].num_parallel_lines =
                                                    (int)nUD_LignesParr3.Value;
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel3.SelectedIndex != k.VIDE
                    && cBox_Type3.SelectedIndex != k.VIDE
                        && nUD_Long3.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_C);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_C();
            }
        }
        private void nUD_LignesParr4_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du nombre de lignes paralleles dans le tableau Inputs
            inputs.section[k.SECTION_D].num_parallel_lines =
                                                    (int)nUD_LignesParr4.Value;
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel4.SelectedIndex != k.VIDE
                    && cBox_Type4.SelectedIndex != k.VIDE
                        && nUD_Long4.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_D);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_D();
            }
        }
        private void nUD_LignesParr5_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du nombre de lignes paralleles dans le tableau Inputs
            inputs.section[k.SECTION_E].num_parallel_lines =
                                                    (int)nUD_LignesParr5.Value;
            //Si un materiel ET un type de tuyau ET un longueur de section sont
            //selectionne
            if (cBox_materiel5.SelectedIndex != k.VIDE
                    && cBox_Type5.SelectedIndex != k.VIDE
                        && nUD_Long5.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_E);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_E();
            }
        }
        /*===Insertion de la longueur de section dans la case d t_section====*/
        private void nUD_Long1_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_A].longueur_section =
                                                       (double)nUD_Long1.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel1.SelectedIndex != k.VIDE
                    && cBox_Type1.SelectedIndex != k.VIDE
                        && nUD_Long1.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_A);
            }
            //Sinon desactivation de la section
            else
            {
                desactivation_section(k.SECTION_A);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_A();
        }
        private void nUD_Long2_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_B].longueur_section =
                                                        (double)nUD_Long2.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel2.SelectedIndex != k.VIDE
                    && cBox_Type2.SelectedIndex != k.VIDE
                        && nUD_Long2.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_B);
            }
            //Sinon desactivation de la section
            else
            {
                desactivation_section(k.SECTION_B);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_B();
        }
        private void nUD_Long3_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_C].longueur_section =
                                                        (double)nUD_Long3.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel3.SelectedIndex != k.VIDE
                    && cBox_Type3.SelectedIndex != k.VIDE
                        && nUD_Long3.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_C);
            }
            //Sinon desactivation de la section
            else
            {
                desactivation_section(k.SECTION_C);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_C();
        }
        private void nUD_Long4_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_D].longueur_section =
                                                        (double)nUD_Long4.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel4.SelectedIndex != k.VIDE
                    && cBox_Type4.SelectedIndex != k.VIDE
                        && nUD_Long4.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_D);
            }
            //Sinon desactivation de la section
            else
            {
                desactivation_section(k.SECTION_D);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_D();
        }
        private void nUD_Long5_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_E].longueur_section =
                                                        (double)nUD_Long5.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel5.SelectedIndex != k.VIDE
                    && cBox_Type5.SelectedIndex != k.VIDE
                        && nUD_Long5.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_E);
            }
            //Sinon desactivation de la section
            else
            {
                desactivation_section(k.SECTION_E);
            }
            //Update des resultats, pour changer les resultats ou les remettre a 0
            update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
            update_tableur_section_E();
        }
        /*===Insertion de la hauteur statique dans la case du t_section===*/
        private void nUD_Static1_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la hauteur statique dans le tableau Inputs
            inputs.section[k.SECTION_A].static_head = (int)nUD_Static1.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel1.SelectedIndex != k.VIDE
                   && cBox_Type1.SelectedIndex != k.VIDE
                       && nUD_Long1.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_A);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_A();
            }
        }
        private void nUD_Static2_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la hauteur statique dans le tableau Inputs
            inputs.section[k.SECTION_B].static_head = (int)nUD_Static2.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel2.SelectedIndex != k.VIDE
                    && cBox_Type2.SelectedIndex != k.VIDE
                        && nUD_Long2.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_B);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_B();
            }
        }
        private void nUD_Static3_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la hauteur statique dans le tableau Inputs
            inputs.section[k.SECTION_C].static_head = (int)nUD_Static3.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel3.SelectedIndex != k.VIDE
                    && cBox_Type3.SelectedIndex != k.VIDE
                        && nUD_Long3.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_C);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_C();
            }
        }
        private void nUD_Static4_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la hauteur statique dans le tableau Inputs
            inputs.section[k.SECTION_D].static_head = (int)nUD_Static4.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel4.SelectedIndex != k.VIDE
                    && cBox_Type4.SelectedIndex != k.VIDE
                        && nUD_Long4.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_D);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_D();
            }
        }
        private void nUD_Static5_ValueChanged(object sender, EventArgs e)
        {
            //Insertion de la hauteur statique dans le tableau Inputs
            inputs.section[k.SECTION_E].static_head = (int)nUD_Static5.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel5.SelectedIndex != k.VIDE
                    && cBox_Type5.SelectedIndex != k.VIDE
                        && nUD_Long5.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_E);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_E();
            }
        }
        /*===Insertion du facteur de securite dans la case du t_section===*/
        private void nUD_Safety_Factor1_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du facteur de securite dans le tableau Inputs
            inputs.section[k.SECTION_A].safety_factor =
                                                (double)nUD_Safety_Factor1.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel1.SelectedIndex != k.VIDE
                    && cBox_Type1.SelectedIndex != k.VIDE
                        && nUD_Long1.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_A);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_A();
            }
        }
        private void nUD_Safety_Factor2_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du facteur de securite dans le tableau Inputs
            inputs.section[k.SECTION_B].safety_factor =
                                                (double)nUD_Safety_Factor2.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel2.SelectedIndex != k.VIDE
                    && cBox_Type2.SelectedIndex != k.VIDE
                        && nUD_Long2.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_B);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_B();
            }
        }
        private void nUD_Safety_Factor3_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du facteur de securite dans le tableau Inputs
            inputs.section[k.SECTION_C].safety_factor =
                                                (double)nUD_Safety_Factor3.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel3.SelectedIndex != k.VIDE
                    && cBox_Type3.SelectedIndex != k.VIDE
                        && nUD_Long3.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_C);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_C();
            }
        }
        private void nUD_Safety_Factor4_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du facteur de securite dans le tableau Inputs
            inputs.section[k.SECTION_D].safety_factor =
                                                (double)nUD_Safety_Factor4.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel4.SelectedIndex != k.VIDE
                    && cBox_Type4.SelectedIndex != k.VIDE
                        && nUD_Long4.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_D);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_D();
            }
        }
        private void nUD_Safety_Factor5_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du facteur de securite dans le tableau Inputs
            inputs.section[k.SECTION_E].safety_factor =
                                                (double)nUD_Safety_Factor5.Value;
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel5.SelectedIndex != k.VIDE
                    && cBox_Type5.SelectedIndex != k.VIDE
                        && nUD_Long5.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_E);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_section_E();
            }
        }
        #endregion
        //FITTINGS
        #region Encapsulation des inputs de fittings
        //Quantite de fitting section A
        private void nUD_A_1_ValueChanged(object sender, EventArgs e)
        {
            //Insertion du fitting 1 A dans le tableau
            inputs.section[k.SECTION_A].fitting[k.FITTING_1].quantite =
                                                        (double)nUD_A_1.Value;
            //Ecriture de l'equivalence dans le lbl associe
            lbl_A_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_1).ToString();
            //Calcul, addition des fitting pour obtenir le total
            calcul_equivalent_tot(k.SECTION_A);
            //Update du tableur suite a la modification de fitting
            update_tableur_section_A();
        }
        private void nUD_A_2_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_2].quantite =
                                                        (double)nUD_A_2.Value;
            lbl_A_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void nUD_A_3_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_3].quantite =
                                                        (double)nUD_A_3.Value;
            lbl_A_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void nUD_A_4_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_4].quantite =
                                                        (double)nUD_A_4.Value;
            lbl_A_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void nUD_A_5_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_5].quantite =
                                                        (double)nUD_A_5.Value;
            lbl_A_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void nUD_A_6_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_6].quantite =
                                                        (double)nUD_A_6.Value;
            lbl_A_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void nUD_A_7_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_7].quantite =
                                                        (double)nUD_A_7.Value;
            lbl_A_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void nUD_A_8_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_8].quantite =
                                                        (double)nUD_A_8.Value;
            lbl_A_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void nUD_A_9_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_9].quantite =
                                                        (double)nUD_A_9.Value;
            lbl_A_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void nUD_A_10_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_10].quantite =
                                                        (double)nUD_A_10.Value;
            lbl_A_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        //Quantite de fitting section B
        private void nUD_B_1_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_1].quantite =
                                                        (double)nUD_B_1.Value;
            lbl_B_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_1).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void nUD_B_2_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_2].quantite =
                                                        (double)nUD_B_2.Value;
            lbl_B_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void nUD_B_3_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_3].quantite =
                                                        (double)nUD_B_3.Value;
            lbl_B_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void nUD_B_4_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_4].quantite =
                                                        (double)nUD_B_4.Value;
            lbl_B_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void nUD_B_5_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_5].quantite =
                                                        (double)nUD_B_5.Value;
            lbl_B_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void nUD_B_6_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_6].quantite =
                                                        (double)nUD_B_6.Value;
            lbl_B_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void nUD_B_7_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_7].quantite =
                                                        (double)nUD_B_7.Value;
            lbl_B_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void nUD_B_8_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_8].quantite =
                                                        (double)nUD_B_8.Value;
            lbl_B_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void nUD_B_9_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_9].quantite =
                                                        (double)nUD_B_9.Value;
            lbl_B_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void nUD_B_10_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_10].quantite =
                                                        (double)nUD_B_10.Value;
            lbl_B_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        //Quantite de fitting section C
        private void nUD_C_1_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_1].quantite =
                                                        (double)nUD_C_1.Value;
            lbl_C_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_1).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void nUD_C_2_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_2].quantite =
                                                        (double)nUD_C_2.Value;
            lbl_C_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void nUD_C_3_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_3].quantite =
                                                        (double)nUD_C_3.Value;
            lbl_C_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void nUD_C_4_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_4].quantite =
                                                        (double)nUD_C_4.Value;
            lbl_C_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void nUD_C_5_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_5].quantite =
                                                        (double)nUD_C_5.Value;
            lbl_C_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void nUD_C_6_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_6].quantite =
                                                        (double)nUD_C_6.Value;
            lbl_C_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void nUD_C_7_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_7].quantite =
                                                        (double)nUD_C_7.Value;
            lbl_C_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void nUD_C_8_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_8].quantite =
                                                        (double)nUD_C_8.Value;
            lbl_C_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void nUD_C_9_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_9].quantite =
                                                        (double)nUD_C_9.Value;
            lbl_C_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void nUD_C_10_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_10].quantite =
                                                        (double)nUD_C_10.Value;
            lbl_C_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        //Quantite de fitting section D
        private void nUD_D_1_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_1].quantite =
                                                        (double)nUD_D_1.Value;
            lbl_D_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_1).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void nUD_D_2_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_2].quantite =
                                                        (double)nUD_D_2.Value;
            lbl_D_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void nUD_D_3_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_3].quantite =
                                                        (double)nUD_D_3.Value;
            lbl_D_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void nUD_D_4_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_4].quantite =
                                                        (double)nUD_D_4.Value;
            lbl_D_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void nUD_D_5_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_5].quantite =
                                                        (double)nUD_D_5.Value;
            lbl_D_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void nUD_D_6_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_6].quantite =
                                                        (double)nUD_D_6.Value;
            lbl_D_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void nUD_D_7_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_7].quantite =
                                                        (double)nUD_D_7.Value;
            lbl_D_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void nUD_D_8_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_8].quantite =
                                                        (double)nUD_D_8.Value;
            lbl_D_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void nUD_D_9_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_9].quantite =
                                                        (double)nUD_D_9.Value;
            lbl_D_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void nUD_D_10_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_10].quantite =
                                                        (double)nUD_D_10.Value;
            lbl_D_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        //Quantite de fitting section E
        private void nUD_E_1_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_1].quantite =
                                                        (double)nUD_E_1.Value;
            lbl_E_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_1).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void nUD_E_2_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_2].quantite =
                                                        (double)nUD_E_2.Value;
            lbl_E_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void nUD_E_3_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_3].quantite =
                                                        (double)nUD_E_3.Value;
            lbl_E_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void nUD_E_4_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_4].quantite =
                                                        (double)nUD_E_4.Value;
            lbl_E_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void nUD_E_5_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_5].quantite =
                                                        (double)nUD_E_5.Value;
            lbl_E_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void nUD_E_6_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_6].quantite =
                                                        (double)nUD_E_6.Value;
            lbl_E_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void nUD_E_7_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_7].quantite =
                                                        (double)nUD_E_7.Value;
            lbl_E_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void nUD_E_8_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_8].quantite =
                                                        (double)nUD_E_8.Value;
            lbl_E_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void nUD_E_9_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_9].quantite =
                                                        (double)nUD_E_9.Value;
            lbl_E_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void nUD_E_10_ValueChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_10].quantite =
                                                        (double)nUD_E_10.Value;
            lbl_E_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        //Type de fitting section A
        private void cBox_Fitting_A_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Insertion du fitting dans le tableau Inputs
            inputs.section[k.SECTION_A].fitting[k.FITTING_1].fitting =
                                        (string)cBox_Fitting_A_1.SelectedItem;
            //Ecriture de l'equivalence des fittings
            lbl_A_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_1).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void cBox_Fitting_A_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_2].fitting =
                                        (string)cBox_Fitting_A_2.SelectedItem;
            lbl_A_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void cBox_Fitting_A_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_3].fitting =
                                        (string)cBox_Fitting_A_3.SelectedItem;
            lbl_A_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void cBox_Fitting_A_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_4].fitting =
                                        (string)cBox_Fitting_A_4.SelectedItem;
            lbl_A_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void cBox_Fitting_A_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_5].fitting =
                                        (string)cBox_Fitting_A_5.SelectedItem;
            lbl_A_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void cBox_Fitting_A_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_6].fitting =
                                        (string)cBox_Fitting_A_6.SelectedItem;
            lbl_A_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void cBox_Fitting_A_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_7].fitting =
                                        (string)cBox_Fitting_A_7.SelectedItem;
            lbl_A_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void cBox_Fitting_A_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_8].fitting =
                                        (string)cBox_Fitting_A_8.SelectedItem;
            lbl_A_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void cBox_Fitting_A_9_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_9].fitting =
                                        (string)cBox_Fitting_A_9.SelectedItem;
            lbl_A_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        private void cBox_Fitting_A_10_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_A].fitting[k.FITTING_10].fitting =
                                        (string)cBox_Fitting_A_10.SelectedItem;
            lbl_A_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_A, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_A);
            update_tableur_section_A();
        }
        //Type de fitting section B
        private void cBox_Fitting_B_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_1].fitting =
                                        (string)cBox_Fitting_B_1.SelectedItem;
            lbl_B_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_1).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void cBox_Fitting_B_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_2].fitting =
                                        (string)cBox_Fitting_B_2.SelectedItem;
            lbl_B_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void cBox_Fitting_B_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_3].fitting =
                                        (string)cBox_Fitting_B_3.SelectedItem;
            lbl_B_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void cBox_Fitting_B_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_4].fitting =
                                        (string)cBox_Fitting_B_4.SelectedItem;
            lbl_B_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void cBox_Fitting_B_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_5].fitting =
                                        (string)cBox_Fitting_B_5.SelectedItem;
            lbl_B_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void cBox_Fitting_B_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_6].fitting =
                                        (string)cBox_Fitting_B_6.SelectedItem;
            lbl_B_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void cBox_Fitting_B_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_7].fitting =
                                        (string)cBox_Fitting_B_7.SelectedItem;
            lbl_B_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void cBox_Fitting_B_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_8].fitting =
                                        (string)cBox_Fitting_B_8.SelectedItem;
            lbl_B_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void cBox_Fitting_B_9_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_9].fitting =
                                        (string)cBox_Fitting_B_9.SelectedItem;
            lbl_B_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        private void cBox_Fitting_B_10_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_B].fitting[k.FITTING_10].fitting =
                                        (string)cBox_Fitting_B_10.SelectedItem;
            lbl_B_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_B, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_B);
            update_tableur_section_B();
        }
        //Type de fitting section C
        private void cBox_Fitting_C_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_1].fitting =
                                        (string)cBox_Fitting_C_1.SelectedItem;
            lbl_C_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_1).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void cBox_Fitting_C_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_2].fitting =
                                        (string)cBox_Fitting_C_2.SelectedItem;
            lbl_C_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void cBox_Fitting_C_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_3].fitting =
                                        (string)cBox_Fitting_C_3.SelectedItem;
            lbl_C_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void cBox_Fitting_C_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_4].fitting =
                                        (string)cBox_Fitting_C_4.SelectedItem;
            lbl_C_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void cBox_Fitting_C_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_5].fitting =
                                        (string)cBox_Fitting_C_5.SelectedItem;
            lbl_C_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void cBox_Fitting_C_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_6].fitting =
                                        (string)cBox_Fitting_C_6.SelectedItem;
            lbl_C_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void cBox_Fitting_C_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_7].fitting =
                                        (string)cBox_Fitting_C_7.SelectedItem;
            lbl_C_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void cBox_Fitting_C_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_8].fitting =
                                        (string)cBox_Fitting_C_8.SelectedItem;
            lbl_C_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void cBox_Fitting_C_9_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_9].fitting =
                                        (string)cBox_Fitting_C_9.SelectedItem;
            lbl_C_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        private void cBox_Fitting_C_10_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_C].fitting[k.FITTING_10].fitting =
                                        (string)cBox_Fitting_C_10.SelectedItem;
            lbl_C_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_C, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_C);
            update_tableur_section_C();
        }
        //Type de fitting section D
        private void cBox_Fitting_D_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_1].fitting =
                                        (string)cBox_Fitting_D_1.SelectedItem;
            lbl_D_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_1).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void cBox_Fitting_D_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_2].fitting =
                                        (string)cBox_Fitting_D_2.SelectedItem;
            lbl_D_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void cBox_Fitting_D_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_3].fitting =
                                        (string)cBox_Fitting_D_3.SelectedItem;
            lbl_D_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void cBox_Fitting_D_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_4].fitting =
                                        (string)cBox_Fitting_D_4.SelectedItem;
            lbl_D_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void cBox_Fitting_D_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_5].fitting =
                                        (string)cBox_Fitting_D_5.SelectedItem;
            lbl_D_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void cBox_Fitting_D_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_6].fitting =
                                        (string)cBox_Fitting_D_6.SelectedItem;
            lbl_D_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void cBox_Fitting_D_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_7].fitting =
                                        (string)cBox_Fitting_D_7.SelectedItem;
            lbl_D_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void cBox_Fitting_D_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_8].fitting =
                                        (string)cBox_Fitting_D_8.SelectedItem;
            lbl_D_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void cBox_Fitting_D_9_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_9].fitting =
                                        (string)cBox_Fitting_D_9.SelectedItem;
            lbl_D_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        private void cBox_Fitting_D_10_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_D].fitting[k.FITTING_10].fitting =
                                        (string)cBox_Fitting_D_10.SelectedItem;
            lbl_D_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_D, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_D);
            update_tableur_section_D();
        }
        //Type de fitting section E
        private void cBox_Fitting_E_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_1].fitting =
                                        (string)cBox_Fitting_E_1.SelectedItem;
            lbl_E_Equiv1.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_1).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void cBox_Fitting_E_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_2].fitting =
                                        (string)cBox_Fitting_E_2.SelectedItem;
            lbl_E_Equiv2.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_2).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void cBox_Fitting_E_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_3].fitting =
                                        (string)cBox_Fitting_E_3.SelectedItem;
            lbl_E_Equiv3.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_3).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void cBox_Fitting_E_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_4].fitting =
                                        (string)cBox_Fitting_E_4.SelectedItem;
            lbl_E_Equiv4.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_4).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void cBox_Fitting_E_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_5].fitting =
                                        (string)cBox_Fitting_E_5.SelectedItem;
            lbl_E_Equiv5.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_5).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void cBox_Fitting_E_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_6].fitting =
                                        (string)cBox_Fitting_E_6.SelectedItem;
            lbl_E_Equiv6.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_6).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void cBox_Fitting_E_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_7].fitting =
                                        (string)cBox_Fitting_E_7.SelectedItem;
            lbl_E_Equiv7.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_7).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void cBox_Fitting_E_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_8].fitting =
                                        (string)cBox_Fitting_E_8.SelectedItem;
            lbl_E_Equiv8.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_8).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void cBox_Fitting_E_9_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_9].fitting =
                                        (string)cBox_Fitting_E_9.SelectedItem;
            lbl_E_Equiv9.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_9).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        private void cBox_Fitting_E_10_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputs.section[k.SECTION_E].fitting[k.FITTING_10].fitting =
                                        (string)cBox_Fitting_E_10.SelectedItem;
            lbl_E_Equiv10.Text =
                    ecriture_equivalence(k.SECTION_E, k.FITTING_10).ToString();
            calcul_equivalent_tot(k.SECTION_E);
            update_tableur_section_E();
        }
        //Label affichant la valeur totale de longueur equivalente des fittings pour la section
        private void lbl_Fitting_A_TextChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_A].fitting_tot =
                                            double.Parse(lbl_Fitting_A.Text);
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel1.SelectedIndex != k.VIDE
                    && cBox_Type1.SelectedIndex != k.VIDE
                        && nUD_Long1.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_A);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_TDH_total();
            }
        }
        private void lbl_Fitting_B_TextChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_B].fitting_tot =
                                            double.Parse(lbl_Fitting_B.Text);
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel2.SelectedIndex != k.VIDE
                    && cBox_Type2.SelectedIndex != k.VIDE
                        && nUD_Long2.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_B);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_TDH_total();
            }
        }
        private void lbl_Fitting_C_TextChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_C].fitting_tot =
                                            double.Parse(lbl_Fitting_C.Text);
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel3.SelectedIndex != k.VIDE
                    && cBox_Type3.SelectedIndex != k.VIDE
                        && nUD_Long3.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_C);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_TDH_total();
            }
        }
        private void lbl_Fitting_D_TextChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_D].fitting_tot =
                                            double.Parse(lbl_Fitting_D.Text);
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel4.SelectedIndex != k.VIDE
                    && cBox_Type4.SelectedIndex != k.VIDE
                        && nUD_Long4.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_D);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_TDH_total();
            }
        }
        private void lbl_Fitting_E_TextChanged(object sender, EventArgs e)
        {
            //Insertion de la longueur de section dasn le tableau Inputs
            inputs.section[k.SECTION_E].fitting_tot =
                                double.Parse(lbl_Fitting_E.Text);
            //Si le materiel ET le type ET la longueur ont une valeur
            if (cBox_materiel5.SelectedIndex != k.VIDE
                    && cBox_Type5.SelectedIndex != k.VIDE
                        && nUD_Long5.Value != 0)
            {
                //Activation de la section associee
                activation_section(k.SECTION_E);
                //Update des resultats, pour changer les resultats ou les remettre a 0
                update_resultats_et_graphique(k.SERIE_TUYAUTERIE);
                update_tableur_TDH_total();
            }
        }
        //Retourne la valeur equivalente des fitting
        private string ecriture_equivalence(int section, int no_fitting)
        {
            if (inputs.section[section].fitting[no_fitting].fitting != null)
            {
                inputs.section[section].fitting[no_fitting].equivalent =
                    calcul.longueur_equiv(inputs, section, no_fitting);
            }
            return inputs.section[section].fitting[no_fitting].equivalent.ToString();
        }
        //Les boutons servant a sortir de la page fitting
        private void btn_Ok_Equiv_A_Click(object sender, EventArgs e)
        {
            //Deblocage des entrees a la sorti du menu fitting
            pnl_SystemInput.Enabled = true;
            //Remise en arriere plan du menu fitting
            pnl_Equiv_A.SendToBack();
        }
        //Les boutons servant a sortir de la page fitting
        private void btn_Ok_Equiv_B_Click(object sender, EventArgs e)
        {
            //Deblocage des entrees a la sorti du menu fitting
            pnl_SystemInput.Enabled = true;
            //Remise en arriere plan du menu fitting
            pnl_Equiv_B.SendToBack();
        }
        //Les boutons servant a sortir de la page fitting
        private void btn_Ok_Equiv_C_Click(object sender, EventArgs e)
        {
            //Deblocage des entrees a la sorti du menu fitting
            pnl_SystemInput.Enabled = true;
            //Remise en arriere plan du menu fitting
            pnl_Equiv_C.SendToBack();
        }
        //Les boutons servant a sortir de la page fitting
        private void btn_Ok_Equiv_D_Click(object sender, EventArgs e)
        {
            //Deblocage des entrees a la sorti du menu fitting
            pnl_SystemInput.Enabled = true;
            //Remise en arriere plan du menu fitting
            pnl_Equiv_D.SendToBack();
        }
        //Les boutons servant a sortir de la page fitting
        private void btn_Ok_Equiv_E_Click(object sender, EventArgs e)
        {
            //Deblocage des entrees a la sorti du menu fitting
            pnl_SystemInput.Enabled = true;
            //Remise en arriere plan du menu fitting
            pnl_Equiv_E.SendToBack();
        }
        //Boutons servant a rentrer dans le menu fitting de chaque section
        private void btn_Fitting_A_Click(object sender, EventArgs e)
        {
            //Blocage du restant des entrees tant que le menu fitting est actif
            pnl_SystemInput.Enabled = false;
            //Mise en evidence du menu fitting
            pnl_Equiv_A.BringToFront();
        }
        //Boutons servant a rentrer dans le menu fitting de chaque section
        private void btn_Fitting_B_Click(object sender, EventArgs e)
        {
            //Blocage du restant des entrees tant que le menu fitting est actif
            pnl_SystemInput.Enabled = false;
            //Mise en evidence du menu fitting
            pnl_Equiv_B.BringToFront();
        }
        //Boutons servant a rentrer dans le menu fitting de chaque section
        private void btn_Fitting_C_Click(object sender, EventArgs e)
        {
            //Blocage du restant des entrees tant que le menu fitting est actif
            pnl_SystemInput.Enabled = false;
            //Mise en evidence du menu fitting
            pnl_Equiv_C.BringToFront();
        }
        //Boutons servant a rentrer dans le menu fitting de chaque section
        private void btn_Fitting_D_Click(object sender, EventArgs e)
        {
            //Blocage du restant des entrees tant que le menu fitting est actif
            pnl_SystemInput.Enabled = false;
            //Mise en evidence du menu fitting
            pnl_Equiv_D.BringToFront();
        }
        //Boutons servant a rentrer dans le menu fitting de chaque section
        private void btn_Fitting_E_Click(object sender, EventArgs e)
        {
            //Blocage du restant des entrees tant que le menu fitting est actif
            pnl_SystemInput.Enabled = false;
            //Mise en evidence du menu fitting
            pnl_Equiv_E.BringToFront();
        }
        //Region contenant tout ce qui touche les longueur equivalente de fitting
        void calcul_equivalent_tot(int segment)
        {
            switch (segment)
            {
                //Addition de toutes les equivalences
                case k.SECTION_A:
                    lbl_A_Equiv_tot.Text =
          (double.Parse(lbl_A_Equiv1.Text) +
              double.Parse(lbl_A_Equiv2.Text) +
              double.Parse(lbl_A_Equiv3.Text) +
              double.Parse(lbl_A_Equiv4.Text) +
              double.Parse(lbl_A_Equiv5.Text) +
              double.Parse(lbl_A_Equiv6.Text) +
              double.Parse(lbl_A_Equiv7.Text) +
              double.Parse(lbl_A_Equiv8.Text) +
              double.Parse(lbl_A_Equiv9.Text) +
              double.Parse(lbl_A_Equiv10.Text)).ToString();
                    //Ecriture de l'equivalence totale dans le label approprie
                    lbl_Fitting_A.Text = lbl_A_Equiv_tot.Text;
                    break;

                //Addition de toutes les equivalences
                case k.SECTION_B:
                    lbl_B_Equiv_tot.Text =
          (double.Parse(lbl_B_Equiv1.Text) +
              double.Parse(lbl_B_Equiv2.Text) +
              double.Parse(lbl_B_Equiv3.Text) +
              double.Parse(lbl_B_Equiv4.Text) +
              double.Parse(lbl_B_Equiv5.Text) +
              double.Parse(lbl_B_Equiv6.Text) +
              double.Parse(lbl_B_Equiv7.Text) +
              double.Parse(lbl_B_Equiv8.Text) +
              double.Parse(lbl_B_Equiv9.Text) +
              double.Parse(lbl_B_Equiv10.Text)).ToString();
                    //Ecriture de l'equivalence totale dans le label approprie
                    lbl_Fitting_B.Text = lbl_B_Equiv_tot.Text;
                    break;

                //Addition de toutes les equivalences
                case k.SECTION_C:
                    lbl_C_Equiv_tot.Text =
          (double.Parse(lbl_C_Equiv1.Text) +
              double.Parse(lbl_C_Equiv2.Text) +
              double.Parse(lbl_C_Equiv3.Text) +
              double.Parse(lbl_C_Equiv4.Text) +
              double.Parse(lbl_C_Equiv5.Text) +
              double.Parse(lbl_C_Equiv6.Text) +
              double.Parse(lbl_C_Equiv7.Text) +
              double.Parse(lbl_C_Equiv8.Text) +
              double.Parse(lbl_C_Equiv9.Text) +
              double.Parse(lbl_C_Equiv10.Text)).ToString();
                    //Ecriture de l'equivalence totale dans le label approprie
                    lbl_Fitting_C.Text = lbl_C_Equiv_tot.Text;
                    break;

                //Addition de toutes les equivalences
                case k.SECTION_D:
                    lbl_D_Equiv_tot.Text =
          (double.Parse(lbl_D_Equiv1.Text) +
              double.Parse(lbl_D_Equiv2.Text) +
              double.Parse(lbl_D_Equiv3.Text) +
              double.Parse(lbl_D_Equiv4.Text) +
              double.Parse(lbl_D_Equiv5.Text) +
              double.Parse(lbl_D_Equiv6.Text) +
              double.Parse(lbl_D_Equiv7.Text) +
              double.Parse(lbl_D_Equiv8.Text) +
              double.Parse(lbl_D_Equiv9.Text) +
              double.Parse(lbl_D_Equiv10.Text)).ToString();
                    //Ecriture de l'equivalence totale dans le label approprie
                    lbl_Fitting_D.Text = lbl_D_Equiv_tot.Text;
                    break;

                //Addition de toutes les equivalences
                case k.SECTION_E:
                    lbl_E_Equiv_tot.Text =
          (double.Parse(lbl_E_Equiv1.Text) +
              double.Parse(lbl_E_Equiv2.Text) +
              double.Parse(lbl_E_Equiv3.Text) +
              double.Parse(lbl_E_Equiv4.Text) +
              double.Parse(lbl_E_Equiv5.Text) +
              double.Parse(lbl_E_Equiv6.Text) +
              double.Parse(lbl_E_Equiv7.Text) +
              double.Parse(lbl_E_Equiv8.Text) +
              double.Parse(lbl_E_Equiv9.Text) +
              double.Parse(lbl_E_Equiv10.Text)).ToString();
                    //Ecriture de l'equivalence totale dans le label approprie
                    lbl_Fitting_E.Text = lbl_E_Equiv_tot.Text;
                    break;
            }
        }
        #endregion

        /*=====================PAGE POMPES===================================*/
        #region Encapsulation de la page pompes
        /*Changement de valeur dans les combobox marques, declenchent 
        * l'evenement pour remplir le cBox modele qui lui est associe selon le 
        * choix de l'utilisateur*/
        private void cBox_Marque1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque1.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_1].marque = (string)cBox_Marque1.SelectedItem;
                inputs.pompes[k.POMPE_1].index_marque = cBox_Marque1.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Serie1.TabIndex = 0;
                cBox_Modele1.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele1.Items.Clear();
                cBox_Serie1.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque1.SelectedIndex, k.POMPE_1);
            }
        }
        private void cBox_Marque2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque2.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_2].marque = (string)cBox_Marque2.SelectedItem;
                inputs.pompes[k.POMPE_2].index_marque = cBox_Marque2.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele2.TabIndex = 0;
                cBox_Serie2.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele2.Items.Clear();
                cBox_Serie2.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque2.SelectedIndex, k.POMPE_2);
            }
        }
        private void cBox_Marque3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque3.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_3].marque = (string)cBox_Marque3.SelectedItem;
                inputs.pompes[k.POMPE_3].index_marque = cBox_Marque3.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele3.TabIndex = 0;
                cBox_Serie3.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele3.Items.Clear();
                cBox_Serie3.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque3.SelectedIndex, k.POMPE_3);
            }
        }
        private void cBox_Marque4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque4.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_4].marque = (string)cBox_Marque4.SelectedItem;
                inputs.pompes[k.POMPE_4].index_marque = cBox_Marque4.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele4.TabIndex = 0;
                cBox_Serie4.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele4.Items.Clear();
                cBox_Serie4.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque4.SelectedIndex, k.POMPE_4);
            }
        }
        private void cBox_Marque5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque5.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_5].marque = (string)cBox_Marque5.SelectedItem;
                inputs.pompes[k.POMPE_5].index_marque = cBox_Marque5.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele5.TabIndex = 0;
                cBox_Serie5.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele5.Items.Clear();
                cBox_Serie5.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque5.SelectedIndex, k.POMPE_5);
            }
        }
        private void cBox_Marque6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque6.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_6].marque = (string)cBox_Marque6.SelectedItem;
                inputs.pompes[k.POMPE_6].index_marque = cBox_Marque6.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele6.TabIndex = 0;
                cBox_Serie6.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele6.Items.Clear();
                cBox_Serie6.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque6.SelectedIndex, k.POMPE_6);
            }
        }
        private void cBox_Marque7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque7.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_7].marque = (string)cBox_Marque7.SelectedItem;
                inputs.pompes[k.POMPE_7].index_marque = cBox_Marque7.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele7.TabIndex = 0;
                cBox_Serie7.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele7.Items.Clear();
                cBox_Serie7.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque7.SelectedIndex, k.POMPE_7);
            }
        }
        private void cBox_Marque8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque8.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_8].marque = (string)cBox_Marque8.SelectedItem;
                inputs.pompes[k.POMPE_8].index_marque = cBox_Marque8.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele8.TabIndex = 0;
                cBox_Serie8.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele8.Items.Clear();
                cBox_Serie8.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque8.SelectedIndex, k.POMPE_8);
            }
        }
        private void cBox_Marque9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque9.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_9].marque = (string)cBox_Marque9.SelectedItem;
                inputs.pompes[k.POMPE_9].index_marque = cBox_Marque9.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele9.TabIndex = 0;
                cBox_Serie9.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele9.Items.Clear();
                cBox_Serie9.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque9.SelectedIndex, k.POMPE_9);
            }
        }
        private void cBox_Marque10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque10.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_10].marque = (string)cBox_Marque10.SelectedItem;
                inputs.pompes[k.POMPE_10].index_marque = cBox_Marque10.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele10.TabIndex = 0;
                cBox_Serie10.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele10.Items.Clear();
                cBox_Serie10.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque10.SelectedIndex, k.POMPE_10);
            }
        }
        private void cBox_Marque11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque11.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_11].marque = (string)cBox_Marque11.SelectedItem;
                inputs.pompes[k.POMPE_11].index_marque = cBox_Marque11.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele11.TabIndex = 0;
                cBox_Serie11.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele11.Items.Clear();
                cBox_Serie11.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque11.SelectedIndex, k.POMPE_11);
            }
        }
        private void cBox_Marque12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque12.SelectedIndex != k.VIDE)
            {
                //Encapsulation du contenu du combox
                inputs.pompes[k.POMPE_12].marque = (string)cBox_Marque12.SelectedItem;
                inputs.pompes[k.POMPE_12].index_marque = cBox_Marque12.SelectedIndex;
                //Mise a zero de l'index des cBox Serie et Modele, donc au premier
                //de la liste
                cBox_Modele12.TabIndex = 0;
                cBox_Serie12.TabIndex = 0;
                //Effacement du contenu du combobox modele et serie
                cBox_Modele12.Items.Clear();
                cBox_Serie12.Items.Clear();
                //Remplissage du combobox selon la marque selectionnee
                remplissage_cBox_Serie(cBox_Marque12.SelectedIndex, k.POMPE_12);
            }
        }
        //ComboBox pour la selection de la serie de pompe
        private void cBox_Serie1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque1.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_1].serie = (string)cBox_Serie1.SelectedItem;
                inputs.pompes[k.POMPE_1].index_serie = cBox_Serie1.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele1.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele1.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque1.SelectedIndex,
                                            cBox_Serie1.SelectedIndex,
                                                k.POMPE_1);
            }
        }
        private void cBox_Serie2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque2.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_2].serie = (string)cBox_Serie2.SelectedItem;
                inputs.pompes[k.POMPE_2].index_serie = cBox_Serie2.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele2.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele2.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque2.SelectedIndex,
                                            cBox_Serie2.SelectedIndex,
                                                k.POMPE_2);
            }
        }
        private void cBox_Serie3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque3.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_3].serie = (string)cBox_Serie3.SelectedItem;
                inputs.pompes[k.POMPE_3].index_serie = cBox_Serie3.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele3.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele3.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque3.SelectedIndex,
                                            cBox_Serie3.SelectedIndex,
                                                k.POMPE_3);
            }
        }
        private void cBox_Serie4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque4.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_4].serie = (string)cBox_Serie4.SelectedItem;
                inputs.pompes[k.POMPE_4].index_serie = cBox_Serie4.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele4.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele4.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque4.SelectedIndex,
                                            cBox_Serie4.SelectedIndex,
                                                k.POMPE_4);
            }
        }
        private void cBox_Serie5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque5.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_5].serie = (string)cBox_Serie5.SelectedItem;
                inputs.pompes[k.POMPE_5].index_serie = cBox_Serie5.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele5.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele5.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque5.SelectedIndex,
                                            cBox_Serie5.SelectedIndex,
                                                k.POMPE_5);
            }
        }
        private void cBox_Serie6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque6.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_6].serie = (string)cBox_Serie6.SelectedItem;
                inputs.pompes[k.POMPE_6].index_serie = cBox_Serie6.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele6.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele6.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque6.SelectedIndex,
                                            cBox_Serie6.SelectedIndex,
                                                k.POMPE_6);
            }
        }
        private void cBox_Serie7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque7.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_7].serie = (string)cBox_Serie7.SelectedItem;
                inputs.pompes[k.POMPE_7].index_serie = cBox_Serie7.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele7.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele7.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque7.SelectedIndex,
                                            cBox_Serie7.SelectedIndex,
                                                k.POMPE_7);
            }
        }
        private void cBox_Serie8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque8.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_8].serie = (string)cBox_Serie8.SelectedItem;
                inputs.pompes[k.POMPE_8].index_serie = cBox_Serie8.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele8.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele8.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque8.SelectedIndex,
                                            cBox_Serie8.SelectedIndex,
                                                k.POMPE_8);
            }
        }
        private void cBox_Serie9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque9.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_9].serie = (string)cBox_Serie9.SelectedItem;
                inputs.pompes[k.POMPE_9].index_serie = cBox_Serie9.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele9.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele9.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque9.SelectedIndex,
                                            cBox_Serie9.SelectedIndex,
                                                k.POMPE_9);
            }
        }
        private void cBox_Serie10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque10.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_10].serie = (string)cBox_Serie10.SelectedItem;
                inputs.pompes[k.POMPE_10].index_serie = cBox_Serie10.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele10.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele10.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque10.SelectedIndex,
                                            cBox_Serie10.SelectedIndex,
                                                k.POMPE_10);
            }
        }
        private void cBox_Serie11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque11.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_11].serie = (string)cBox_Serie11.SelectedItem;
                inputs.pompes[k.POMPE_11].index_serie = cBox_Serie11.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele11.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele11.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque11.SelectedIndex,
                                            cBox_Serie11.SelectedIndex,
                                                k.POMPE_11);
            }
        }
        private void cBox_Serie12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_Marque12.SelectedIndex != k.VIDE)
            {
                //Encapsulatin du contenu du cBox Serie
                inputs.pompes[k.POMPE_12].serie = (string)cBox_Serie12.SelectedItem;
                inputs.pompes[k.POMPE_12].index_serie = cBox_Serie12.SelectedIndex;
                //Mise a zero de l'index du cBox Modele, donc au premier de la liste
                cBox_Modele12.TabIndex = 0;
                //Effacement du contenu du combobox modele
                cBox_Modele12.Items.Clear();
                //Remplissage des cBox selon la marque et la serie selectionne
                remplissage_cBox_Modeles(cBox_Marque12.SelectedIndex,
                                            cBox_Serie12.SelectedIndex,
                                                k.POMPE_12);
            }
        }

        private void cBox_Modele1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_1.Focus();
            //Si un modele est selectionne
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_1].modele = (string)cBox_Modele1.SelectedItem;
                inputs.pompes[k.POMPE_1].index_modele = cBox_Modele1.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_1].vitesse = matrice_formule[
                                                            cBox_Marque1.SelectedIndex,
                                                            cBox_Serie1.SelectedIndex,
                                                            cBox_Modele1.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P1.Maximum = (int)inputs.pompes[k.POMPE_1].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P1.Value = (int)inputs.pompes[k.POMPE_1].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P1.Maximum = (int)inputs.pompes[k.POMPE_1].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P1.Value = (int)inputs.pompes[k.POMPE_1].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe1.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage1.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio1.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque1.SelectedIndex,
                                cBox_Serie1.SelectedIndex,
                                    cBox_Modele1.SelectedIndex,
                                        k.POMPE_1,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (null)
            pnl_Nb_Pompe_1.Enabled = (cBox_Modele1.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_1.Enabled = (cBox_Modele1.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_1.Enabled = (cBox_Modele1.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_1.Enabled = (cBox_Modele1.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_2.Focus();
            //Si un modele est selectionne
            if (cBox_Modele2.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_2].modele = (string)cBox_Modele2.SelectedItem;
                inputs.pompes[k.POMPE_2].index_modele = cBox_Modele2.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_2].vitesse = matrice_formule[
                                                            cBox_Marque2.SelectedIndex,
                                                            cBox_Serie2.SelectedIndex,
                                                            cBox_Modele2.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P2.Maximum = (int)inputs.pompes[k.POMPE_2].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P2.Value = (int)inputs.pompes[k.POMPE_2].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P2.Maximum = (int)inputs.pompes[k.POMPE_2].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P2.Value = (int)inputs.pompes[k.POMPE_2].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe2.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage2.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio2.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque2.SelectedIndex,
                                cBox_Serie2.SelectedIndex,
                                    cBox_Modele2.SelectedIndex,
                                        k.POMPE_2,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_2.Enabled = (cBox_Modele2.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_2.Enabled = (cBox_Modele2.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_2.Enabled = (cBox_Modele2.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_2.Enabled = (cBox_Modele2.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_3.Focus();
            //Si un modele est selectionne
            if (cBox_Modele3.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_3].modele = (string)cBox_Modele3.SelectedItem;
                inputs.pompes[k.POMPE_3].index_modele = cBox_Modele3.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_3].vitesse = matrice_formule[
                                                            cBox_Marque3.SelectedIndex,
                                                            cBox_Serie3.SelectedIndex,
                                                            cBox_Modele3.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P3.Maximum = (int)inputs.pompes[k.POMPE_3].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P3.Value = (int)inputs.pompes[k.POMPE_3].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P3.Maximum = (int)inputs.pompes[k.POMPE_3].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P3.Value = (int)inputs.pompes[k.POMPE_3].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe3.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage3.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio3.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque3.SelectedIndex,
                                cBox_Serie3.SelectedIndex,
                                    cBox_Modele3.SelectedIndex,
                                        k.POMPE_3,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_3.Enabled = (cBox_Modele3.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_3.Enabled = (cBox_Modele3.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_3.Enabled = (cBox_Modele3.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_3.Enabled = (cBox_Modele3.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_4.Focus();
            //Si un modele est selectionne
            if (cBox_Modele4.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_4].modele = (string)cBox_Modele4.SelectedItem;
                inputs.pompes[k.POMPE_4].index_modele = cBox_Modele4.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_4].vitesse = matrice_formule[
                                                            cBox_Marque4.SelectedIndex,
                                                            cBox_Serie4.SelectedIndex,
                                                            cBox_Modele4.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P4.Maximum = (int)inputs.pompes[k.POMPE_4].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P4.Value = (int)inputs.pompes[k.POMPE_4].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P4.Maximum = (int)inputs.pompes[k.POMPE_4].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P4.Value = (int)inputs.pompes[k.POMPE_4].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe4.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage4.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio4.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque4.SelectedIndex,
                                cBox_Serie4.SelectedIndex,
                                    cBox_Modele4.SelectedIndex,
                                        k.POMPE_4,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_4.Enabled = (cBox_Modele4.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_4.Enabled = (cBox_Modele4.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_4.Enabled = (cBox_Modele4.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_4.Enabled = (cBox_Modele4.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_5.Focus();
            //Si un modele est selectionne
            if (cBox_Modele5.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_5].modele = (string)cBox_Modele5.SelectedItem;
                inputs.pompes[k.POMPE_5].index_modele = cBox_Modele5.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_5].vitesse = matrice_formule[
                                                            cBox_Marque5.SelectedIndex,
                                                            cBox_Serie5.SelectedIndex,
                                                            cBox_Modele5.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P5.Maximum = (int)inputs.pompes[k.POMPE_5].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P5.Value = (int)inputs.pompes[k.POMPE_5].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P5.Maximum = (int)inputs.pompes[k.POMPE_5].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P5.Value = (int)inputs.pompes[k.POMPE_5].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe5.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage5.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio5.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque5.SelectedIndex,
                                cBox_Serie5.SelectedIndex,
                                    cBox_Modele5.SelectedIndex,
                                        k.POMPE_5,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_5.Enabled = (cBox_Modele5.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_5.Enabled = (cBox_Modele5.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_5.Enabled = (cBox_Modele5.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_5.Enabled = (cBox_Modele5.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_6.Focus();
            //Si un modele est selectionne
            if (cBox_Modele6.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_6].modele = (string)cBox_Modele6.SelectedItem;
                inputs.pompes[k.POMPE_6].index_modele = cBox_Modele6.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_6].vitesse = matrice_formule[
                                                            cBox_Marque6.SelectedIndex,
                                                            cBox_Serie6.SelectedIndex,
                                                            cBox_Modele6.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P6.Maximum = (int)inputs.pompes[k.POMPE_6].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P6.Value = (int)inputs.pompes[k.POMPE_6].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P6.Maximum = (int)inputs.pompes[k.POMPE_6].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P6.Value = (int)inputs.pompes[k.POMPE_6].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe6.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage6.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio6.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque6.SelectedIndex,
                                cBox_Serie6.SelectedIndex,
                                    cBox_Modele6.SelectedIndex,
                                        k.POMPE_6,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_6.Enabled = (cBox_Modele6.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_6.Enabled = (cBox_Modele6.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_6.Enabled = (cBox_Modele6.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_6.Enabled = (cBox_Modele6.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele7_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_7.Focus();
            //Si un modele est selectionne
            if (cBox_Modele7.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_7].modele = (string)cBox_Modele7.SelectedItem;
                inputs.pompes[k.POMPE_7].index_modele = cBox_Modele7.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_7].vitesse = matrice_formule[
                                                            cBox_Marque7.SelectedIndex,
                                                            cBox_Serie7.SelectedIndex,
                                                            cBox_Modele7.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P7.Maximum = (int)inputs.pompes[k.POMPE_7].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P7.Value = (int)inputs.pompes[k.POMPE_7].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P7.Maximum = (int)inputs.pompes[k.POMPE_7].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P7.Value = (int)inputs.pompes[k.POMPE_7].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe7.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage7.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio7.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque7.SelectedIndex,
                                cBox_Serie7.SelectedIndex,
                                    cBox_Modele7.SelectedIndex,
                                        k.POMPE_7,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_7.Enabled = (cBox_Modele7.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_7.Enabled = (cBox_Modele7.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_7.Enabled = (cBox_Modele7.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_7.Enabled = (cBox_Modele7.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele8_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_8.Focus();
            //Si un modele est selectionne
            if (cBox_Modele8.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_8].modele = (string)cBox_Modele8.SelectedItem;
                inputs.pompes[k.POMPE_8].index_modele = cBox_Modele8.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_8].vitesse = matrice_formule[
                                                            cBox_Marque8.SelectedIndex,
                                                            cBox_Serie8.SelectedIndex,
                                                            cBox_Modele8.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P8.Maximum = (int)inputs.pompes[k.POMPE_8].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P8.Value = (int)inputs.pompes[k.POMPE_8].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P8.Maximum = (int)inputs.pompes[k.POMPE_8].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P8.Value = (int)inputs.pompes[k.POMPE_8].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe8.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage8.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio8.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque8.SelectedIndex,
                                cBox_Serie8.SelectedIndex,
                                    cBox_Modele8.SelectedIndex,
                                        k.POMPE_8,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_8.Enabled = (cBox_Modele8.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_8.Enabled = (cBox_Modele8.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_8.Enabled = (cBox_Modele8.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_8.Enabled = (cBox_Modele8.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele9_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_9.Focus();
            //Si un modele est selectionne
            if (cBox_Modele9.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_9].modele = (string)cBox_Modele9.SelectedItem;
                inputs.pompes[k.POMPE_9].index_modele = cBox_Modele9.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_9].vitesse = matrice_formule[
                                                            cBox_Marque9.SelectedIndex,
                                                            cBox_Serie9.SelectedIndex,
                                                            cBox_Modele9.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P9.Maximum = (int)inputs.pompes[k.POMPE_9].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P9.Value = (int)inputs.pompes[k.POMPE_9].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P9.Maximum = (int)inputs.pompes[k.POMPE_9].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P9.Value = (int)inputs.pompes[k.POMPE_9].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe9.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage9.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio9.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque9.SelectedIndex,
                                cBox_Serie9.SelectedIndex,
                                    cBox_Modele9.SelectedIndex,
                                        k.POMPE_9,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_9.Enabled = (cBox_Modele9.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_9.Enabled = (cBox_Modele9.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_9.Enabled = (cBox_Modele9.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_9.Enabled = (cBox_Modele9.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele10_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_10.Focus();
            //Si un modele est selectionne
            if (cBox_Modele10.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_10].modele = (string)cBox_Modele10.SelectedItem;
                inputs.pompes[k.POMPE_10].index_modele = cBox_Modele10.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_10].vitesse = matrice_formule[
                                                            cBox_Marque10.SelectedIndex,
                                                            cBox_Serie10.SelectedIndex,
                                                            cBox_Modele10.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P10.Maximum = (int)inputs.pompes[k.POMPE_10].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P10.Value = (int)inputs.pompes[k.POMPE_10].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P10.Maximum = (int)inputs.pompes[k.POMPE_10].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P10.Value = (int)inputs.pompes[k.POMPE_10].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe10.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage10.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio10.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque10.SelectedIndex,
                                cBox_Serie10.SelectedIndex,
                                    cBox_Modele10.SelectedIndex,
                                        k.POMPE_10,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_10.Enabled = (cBox_Modele10.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_10.Enabled = (cBox_Modele10.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_10.Enabled = (cBox_Modele10.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_10.Enabled = (cBox_Modele10.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele11_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_11.Focus();
            //Si un modele est selectionne
            if (cBox_Modele11.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_11].modele = (string)cBox_Modele11.SelectedItem;
                inputs.pompes[k.POMPE_11].index_modele = cBox_Modele11.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_11].vitesse = matrice_formule[
                                                            cBox_Marque11.SelectedIndex,
                                                            cBox_Serie11.SelectedIndex,
                                                            cBox_Modele11.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P11.Maximum = (int)inputs.pompes[k.POMPE_11].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P11.Value = (int)inputs.pompes[k.POMPE_11].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P11.Maximum = (int)inputs.pompes[k.POMPE_11].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P11.Value = (int)inputs.pompes[k.POMPE_11].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe11.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage11.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio11.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque11.SelectedIndex,
                                cBox_Serie11.SelectedIndex,
                                    cBox_Modele11.SelectedIndex,
                                        k.POMPE_11,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_11.Enabled = (cBox_Modele11.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_11.Enabled = (cBox_Modele11.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_11.Enabled = (cBox_Modele11.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_11.Enabled = (cBox_Modele11.SelectedIndex != k.VIDE) ? true : false;
        }
        private void cBox_Modele12_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_12.Focus();
            //Si un modele est selectionne
            if (cBox_Modele12.SelectedIndex != k.VIDE)
            {
                //Insertion du modele de pompe dans le tableau Inputs
                inputs.pompes[k.POMPE_12].modele = (string)cBox_Modele12.SelectedItem;
                inputs.pompes[k.POMPE_12].index_modele = cBox_Modele12.SelectedIndex;
                //Insertion de la vitesse selon la matrice
                inputs.pompes[k.POMPE_12].vitesse = matrice_formule[
                                                            cBox_Marque12.SelectedIndex,
                                                            cBox_Serie12.SelectedIndex,
                                                            cBox_Modele12.SelectedIndex,
                                                            F.VITESSE];
                //Modification de la valeur maximale du Scroll de vitesse
                Scroll_Vit_P12.Maximum = (int)inputs.pompes[k.POMPE_12].vitesse;
                //Scroll de vitesse egal a la vitesse maximale de la pompe
                Scroll_Vit_P12.Value = (int)inputs.pompes[k.POMPE_12].vitesse;
                //Limite maximale du nUD a la vitesse de la pompe
                nUD_Vit_P12.Maximum = (int)inputs.pompes[k.POMPE_12].vitesse;
                //nUD de vitesse mis a la vitesse maximale
                nUD_Vit_P12.Value = (int)inputs.pompes[k.POMPE_12].vitesse;
                //Remise du nb de pompe a la valeur par defaut de 1
                nUD_Nb_Pompe12.Value = k.NB_POMPE_PAR_DEFAUT;
                //Remise du nb de stage a la valeur par defaut de 1
                nUD_Nb_Stage12.Value = k.NB_STAGES_PAR_DEFAUT;
                //Remise du Ratio a la valeur par defaut de 100
                nUD_Ratio12.Value = k.RATIO_PAR_DEFAUT;

                //Tracage de la pompe
                info_pompe(cBox_Marque12.SelectedIndex,
                                cBox_Serie12.SelectedIndex,
                                    cBox_Modele12.SelectedIndex,
                                        k.POMPE_12,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
            //Modification de l'accessibilite des panneaux modifiant les pompes
            //Actifs si un modele est selectionne
            //Inactifs si l'Index est egal a -1 (vide)
            pnl_Nb_Pompe_12.Enabled = (cBox_Modele12.SelectedIndex != k.VIDE) ? true : false;
            pnl_Nb_Stages_12.Enabled = (cBox_Modele12.SelectedIndex != k.VIDE) ? true : false;
            pnl_Ratio_12.Enabled = (cBox_Modele12.SelectedIndex != k.VIDE) ? true : false;
            pnl_Vitesse_12.Enabled = (cBox_Modele12.SelectedIndex != k.VIDE) ? true : false;
        }
        //Modification du nombre de pompe
        private void nUD_Nb_Pompe1_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_1.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_1].nb_pompe = (int)nUD_Nb_Pompe1.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque1.SelectedIndex,
                                cBox_Serie1.SelectedIndex,
                                    cBox_Modele1.SelectedIndex,
                                        k.POMPE_1,
                                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe2_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_2.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_2].nb_pompe = (int)nUD_Nb_Pompe2.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele2.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque2.SelectedIndex,
                            cBox_Serie2.SelectedIndex,
                                cBox_Modele2.SelectedIndex,
                                    k.POMPE_2,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe3_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_3.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_3].nb_pompe = (int)nUD_Nb_Pompe3.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele3.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque3.SelectedIndex,
                            cBox_Serie3.SelectedIndex,
                                cBox_Modele3.SelectedIndex,
                                    k.POMPE_3,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe4_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_4.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_4].nb_pompe = (int)nUD_Nb_Pompe4.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele4.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque4.SelectedIndex,
                            cBox_Serie4.SelectedIndex,
                                cBox_Modele4.SelectedIndex,
                                    k.POMPE_4,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe5_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_5.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_5].nb_pompe = (int)nUD_Nb_Pompe5.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele5.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque5.SelectedIndex,
                            cBox_Serie5.SelectedIndex,
                                cBox_Modele5.SelectedIndex,
                                    k.POMPE_5,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe6_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_6.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_6].nb_pompe = (int)nUD_Nb_Pompe6.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele6.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque6.SelectedIndex,
                            cBox_Serie6.SelectedIndex,
                                cBox_Modele6.SelectedIndex,
                                    k.POMPE_6,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe7_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_7.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_7].nb_pompe = (int)nUD_Nb_Pompe7.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele7.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque7.SelectedIndex,
                            cBox_Serie7.SelectedIndex,
                                cBox_Modele7.SelectedIndex,
                                    k.POMPE_7,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe8_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_8.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_8].nb_pompe = (int)nUD_Nb_Pompe8.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele8.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque8.SelectedIndex,
                            cBox_Serie8.SelectedIndex,
                                cBox_Modele8.SelectedIndex,
                                    k.POMPE_8,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe9_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_9.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_9].nb_pompe = (int)nUD_Nb_Pompe9.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele9.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque9.SelectedIndex,
                            cBox_Serie9.SelectedIndex,
                                cBox_Modele9.SelectedIndex,
                                    k.POMPE_9,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe10_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_10.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_10].nb_pompe = (int)nUD_Nb_Pompe10.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele10.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque10.SelectedIndex,
                            cBox_Serie10.SelectedIndex,
                                cBox_Modele10.SelectedIndex,
                                    k.POMPE_10,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe11_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_11.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_11].nb_pompe = (int)nUD_Nb_Pompe11.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele11.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque11.SelectedIndex,
                            cBox_Serie11.SelectedIndex,
                                cBox_Modele11.SelectedIndex,
                                    k.POMPE_11,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Pompe12_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_12.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_12].nb_pompe = (int)nUD_Nb_Pompe12.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele12.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque12.SelectedIndex,
                            cBox_Serie12.SelectedIndex,
                                cBox_Modele12.SelectedIndex,
                                    k.POMPE_12,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        //Numeric up/down selectionnant le nombre de stage
        private void nUD_Nb_Stage1_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_1.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_1].nb_stages = (int)nUD_Nb_Stage1.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque1.SelectedIndex,
                            cBox_Serie1.SelectedIndex,
                                cBox_Modele1.SelectedIndex,
                                    k.POMPE_1,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage2_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_2.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_2].nb_stages = (int)nUD_Nb_Stage2.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque2.SelectedIndex,
                            cBox_Serie2.SelectedIndex,
                                cBox_Modele2.SelectedIndex,
                                    k.POMPE_2,
                                        TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage3_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_3.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_3].nb_stages = (int)nUD_Nb_Stage3.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque3.SelectedIndex,
                cBox_Serie3.SelectedIndex,
                    cBox_Modele3.SelectedIndex,
                        k.POMPE_3,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage4_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_4.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_4].nb_stages = (int)nUD_Nb_Stage4.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque4.SelectedIndex,
                cBox_Serie4.SelectedIndex,
                    cBox_Modele4.SelectedIndex,
                        k.POMPE_4,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage5_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_5.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_5].nb_stages = (int)nUD_Nb_Stage5.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque5.SelectedIndex,
                cBox_Serie5.SelectedIndex,
                    cBox_Modele5.SelectedIndex,
                        k.POMPE_5,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage6_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_6.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_6].nb_stages = (int)nUD_Nb_Stage6.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque6.SelectedIndex,
                cBox_Serie6.SelectedIndex,
                    cBox_Modele6.SelectedIndex,
                        k.POMPE_6,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage7_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_7.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_7].nb_stages = (int)nUD_Nb_Stage7.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque7.SelectedIndex,
                cBox_Serie7.SelectedIndex,
                    cBox_Modele7.SelectedIndex,
                        k.POMPE_7,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage8_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_8.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_8].nb_stages = (int)nUD_Nb_Stage8.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque8.SelectedIndex,
                cBox_Serie8.SelectedIndex,
                    cBox_Modele8.SelectedIndex,
                        k.POMPE_8,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage9_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_9.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_9].nb_stages = (int)nUD_Nb_Stage9.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque9.SelectedIndex,
                cBox_Serie9.SelectedIndex,
                    cBox_Modele9.SelectedIndex,
                        k.POMPE_9,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage10_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_10.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_10].nb_stages = (int)nUD_Nb_Stage10.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque10.SelectedIndex,
                cBox_Serie10.SelectedIndex,
                    cBox_Modele10.SelectedIndex,
                        k.POMPE_10,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage11_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_11.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_11].nb_stages = (int)nUD_Nb_Stage11.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque11.SelectedIndex,
                cBox_Serie11.SelectedIndex,
                    cBox_Modele11.SelectedIndex,
                        k.POMPE_11,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Nb_Stage12_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_12.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_12].nb_stages = (int)nUD_Nb_Stage12.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque12.SelectedIndex,
                cBox_Serie12.SelectedIndex,
                    cBox_Modele12.SelectedIndex,
                        k.POMPE_12,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        //Numeric up/down pour modifier le ratio du diametre de l'impeller
        private void nUD_Ratio1_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_1.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_1].ratio_diametre =
                                    (double)nUD_Ratio1.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque1.SelectedIndex,
                 cBox_Serie1.SelectedIndex,
                     cBox_Modele1.SelectedIndex,
                         k.POMPE_1,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }

        }
        private void nUD_Ratio2_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_2.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_2].ratio_diametre =
                                    (double)nUD_Ratio2.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele2.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque2.SelectedIndex,
                        cBox_Serie2.SelectedIndex,
                            cBox_Modele2.SelectedIndex,
                                k.POMPE_2,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio3_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_3.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_3].ratio_diametre =
                                    (double)nUD_Ratio3.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele3.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque3.SelectedIndex,
                        cBox_Serie3.SelectedIndex,
                            cBox_Modele3.SelectedIndex,
                                k.POMPE_3,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio4_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_4.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_4].ratio_diametre =
                                    (double)nUD_Ratio4.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele4.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque4.SelectedIndex,
                        cBox_Serie4.SelectedIndex,
                            cBox_Modele4.SelectedIndex,
                                k.POMPE_4,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio5_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_5.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_5].ratio_diametre =
                                    (double)nUD_Ratio5.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele5.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque5.SelectedIndex,
                        cBox_Serie5.SelectedIndex,
                            cBox_Modele5.SelectedIndex,
                                k.POMPE_5,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio6_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_6.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_6].ratio_diametre =
                                    (double)nUD_Ratio6.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele6.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque6.SelectedIndex,
                        cBox_Serie6.SelectedIndex,
                            cBox_Modele6.SelectedIndex,
                                k.POMPE_6,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio7_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_7.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_7].ratio_diametre =
                                    (double)nUD_Ratio7.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele7.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque7.SelectedIndex,
                        cBox_Serie7.SelectedIndex,
                            cBox_Modele7.SelectedIndex,
                                k.POMPE_7,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio8_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_8.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_8].ratio_diametre =
                                    (double)nUD_Ratio8.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele8.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque8.SelectedIndex,
                        cBox_Serie8.SelectedIndex,
                            cBox_Modele8.SelectedIndex,
                                k.POMPE_8,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio9_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_9.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_9].ratio_diametre =
                                    (double)nUD_Ratio9.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele9.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque9.SelectedIndex,
                        cBox_Serie9.SelectedIndex,
                            cBox_Modele9.SelectedIndex,
                                k.POMPE_9,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio10_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_10.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_10].ratio_diametre =
                                    (double)nUD_Ratio10.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele10.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque10.SelectedIndex,
                          cBox_Serie10.SelectedIndex,
                            cBox_Modele10.SelectedIndex,
                                k.POMPE_10,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio11_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_11.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_11].ratio_diametre =
                                    (double)nUD_Ratio11.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele11.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque11.SelectedIndex,
                        cBox_Serie11.SelectedIndex,
                            cBox_Modele11.SelectedIndex,
                                k.POMPE_11,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        private void nUD_Ratio12_ValueChanged(object sender, EventArgs e)
        {
            //Focus au pnl de la pompe, pour eviter de garder le focus et modifier
            //les valeurs par megarde avec le scroll
            pnl_pompe_12.Focus();
            //Insertion du nombre de pompe dans le tableau Inputs
            inputs.pompes[k.POMPE_12].ratio_diametre =
                                    (double)nUD_Ratio12.Value / k.POURCENTAGE;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele12.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque12.SelectedIndex,
                        cBox_Serie12.SelectedIndex,
                             cBox_Modele12.SelectedIndex,
                                k.POMPE_12,
                                    TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }
        //Radio bouton modifiant la disposition des pompes
        private void r_Btn_Parr1_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr1.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie1.Checked = false;
                r_Btn_Parr1.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_1, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele1.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_1].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque1.SelectedIndex,
                                    cBox_Serie1.SelectedIndex,
                                        cBox_Modele1.SelectedIndex,
                                            k.POMPE_1,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr2_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr2.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie2.Checked = false;
                r_Btn_Parr2.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_2, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele2.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_2].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque2.SelectedIndex,
                                    cBox_Serie2.SelectedIndex,
                                        cBox_Modele2.SelectedIndex,
                                            k.POMPE_2,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr3_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr3.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie3.Checked = false;
                r_Btn_Parr3.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_3, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele3.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_3].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque3.SelectedIndex,
                                    cBox_Serie3.SelectedIndex,
                                        cBox_Modele3.SelectedIndex,
                                            k.POMPE_3,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr4_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr4.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie4.Checked = false;
                r_Btn_Parr4.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_4, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele4.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_4].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque4.SelectedIndex,
                                    cBox_Serie4.SelectedIndex,
                                        cBox_Modele4.SelectedIndex,
                                            k.POMPE_4,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr5_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr5.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie5.Checked = false;
                r_Btn_Parr5.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_5, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele5.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_5].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque5.SelectedIndex,
                                    cBox_Serie5.SelectedIndex,
                                        cBox_Modele5.SelectedIndex,
                                            k.POMPE_5,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr6_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr6.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie6.Checked = false;
                r_Btn_Parr6.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_6, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele6.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_6].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque6.SelectedIndex,
                                    cBox_Serie6.SelectedIndex,
                                        cBox_Modele6.SelectedIndex,
                                            k.POMPE_6,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr7_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr7.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie7.Checked = false;
                r_Btn_Parr7.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_7, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele7.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_7].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque7.SelectedIndex,
                                    cBox_Serie7.SelectedIndex,
                                        cBox_Modele7.SelectedIndex,
                                            k.POMPE_7,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr8_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr8.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie8.Checked = false;
                r_Btn_Parr8.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_8, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele8.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_8].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque8.SelectedIndex,
                                    cBox_Serie8.SelectedIndex,
                                        cBox_Modele8.SelectedIndex,
                                            k.POMPE_8,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr9_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr9.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie9.Checked = false;
                r_Btn_Parr9.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_9, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele9.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_9].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque9.SelectedIndex,
                                    cBox_Serie9.SelectedIndex,
                                        cBox_Modele9.SelectedIndex,
                                            k.POMPE_9,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr10_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr10.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie10.Checked = false;
                r_Btn_Parr10.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_10, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele10.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_10].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque10.SelectedIndex,
                                    cBox_Serie10.SelectedIndex,
                                        cBox_Modele10.SelectedIndex,
                                            k.POMPE_10,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr11_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr11.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie11.Checked = false;
                r_Btn_Parr11.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_11, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele11.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_11].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque11.SelectedIndex,
                                    cBox_Serie11.SelectedIndex,
                                        cBox_Modele11.SelectedIndex,
                                            k.POMPE_11,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Parr12_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Parr12.Checked == false)
            {
                //Activation Parr et desactivation Serie
                r_Btn_Serie12.Checked = false;
                r_Btn_Parr12.Checked = true;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_12, k.DISPO_PARR);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele12.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_12].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque12.SelectedIndex,
                                    cBox_Serie12.SelectedIndex,
                                        cBox_Modele12.SelectedIndex,
                                            k.POMPE_12,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        //Radio bouton modifiant la disposition des pompes
        private void r_Btn_Serie1_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie1.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie1.Checked = true;
                r_Btn_Parr1.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_1, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele1.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_1].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque1.SelectedIndex,
                                    cBox_Serie1.SelectedIndex,
                                        cBox_Modele1.SelectedIndex,
                                            k.POMPE_1,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie2_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie2.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie2.Checked = true;
                r_Btn_Parr2.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_2, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele2.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_2].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque2.SelectedIndex,
                                    cBox_Serie2.SelectedIndex,
                                        cBox_Modele2.SelectedIndex,
                                            k.POMPE_2,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie3_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie3.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie3.Checked = true;
                r_Btn_Parr3.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_3, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele3.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_3].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque3.SelectedIndex,
                                    cBox_Serie3.SelectedIndex,
                                        cBox_Modele3.SelectedIndex,
                                            k.POMPE_3,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie4_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie4.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie4.Checked = true;
                r_Btn_Parr4.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_4, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele4.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_4].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque4.SelectedIndex,
                                    cBox_Serie4.SelectedIndex,
                                        cBox_Modele4.SelectedIndex,
                                            k.POMPE_4,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie5_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie5.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie5.Checked = true;
                r_Btn_Parr5.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_5, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele5.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_5].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque5.SelectedIndex,
                                    cBox_Serie5.SelectedIndex,
                                        cBox_Modele5.SelectedIndex,
                                            k.POMPE_5,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie6_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie6.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie6.Checked = true;
                r_Btn_Parr6.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_6, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele1.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_6].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque6.SelectedIndex,
                                    cBox_Serie6.SelectedIndex,
                                        cBox_Modele6.SelectedIndex,
                                            k.POMPE_6,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie7_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie7.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie7.Checked = true;
                r_Btn_Parr7.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_7, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele7.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_7].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque7.SelectedIndex,
                                    cBox_Serie7.SelectedIndex,
                                        cBox_Modele7.SelectedIndex,
                                            k.POMPE_7,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie8_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie8.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie8.Checked = true;
                r_Btn_Parr8.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_8, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele8.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_8].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque8.SelectedIndex,
                                    cBox_Serie8.SelectedIndex,
                                        cBox_Modele8.SelectedIndex,
                                            k.POMPE_8,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie9_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie9.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie9.Checked = true;
                r_Btn_Parr9.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_9, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele9.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_9].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque9.SelectedIndex,
                                    cBox_Serie9.SelectedIndex,
                                        cBox_Modele9.SelectedIndex,
                                            k.POMPE_9,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie10_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie10.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie10.Checked = true;
                r_Btn_Parr10.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_10, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele10.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_10].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque10.SelectedIndex,
                                    cBox_Serie10.SelectedIndex,
                                        cBox_Modele10.SelectedIndex,
                                            k.POMPE_10,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie11_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie11.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie11.Checked = true;
                r_Btn_Parr11.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_11, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele11.SelectedIndex != k.VIDE
                        && inputs.pompes[k.POMPE_11].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque11.SelectedIndex,
                                    cBox_Serie11.SelectedIndex,
                                        cBox_Modele11.SelectedIndex,
                                            k.POMPE_11,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        private void r_Btn_Serie12_Click(object sender, EventArgs e)
        {
            //Si le rbtn n'est pas coche
            if (r_Btn_Serie12.Checked == false)
            {
                //Activation Serie et desactivation Parr
                r_Btn_Serie12.Checked = true;
                r_Btn_Parr12.Checked = false;
                //Changement de la disposition dans le tableau inputs
                changement_disposition_pompe(k.POMPE_12, k.DISPO_SERIE);
                //Tracage de la pompe selon les nouveaux parametres si le nombre
                //pompe est plus haut que 1, puisque la disposition n'a aucun effet
                //s'il n'y a qu'une pompe
                if (cBox_Modele12.SelectedIndex != -k.VIDE
                        && inputs.pompes[k.POMPE_12].nb_pompe > k.NB_POMPE_PAR_DEFAUT)
                {
                    info_pompe(cBox_Marque12.SelectedIndex,
                                    cBox_Serie12.SelectedIndex,
                                        cBox_Modele12.SelectedIndex,
                                            k.POMPE_12,
                                                TDH_tot[k.NB_POINTS_TABLEUR]);
                }
            }
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs1_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_1);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs2_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_2);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs3_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_3);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs4_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_4);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs5_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_5);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs6_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_6);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs7_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_7);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs8_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_8);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs9_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_9);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs10_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_10);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs11_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_11);
        }
        //Bouton ouvrant le tableau de valeur des coordonnees des courbes de pompes
        private void btn_voir_valeurs12_Click(object sender, EventArgs e)
        {
            description_listview(k.POMPE_12);
        }

        //Bouton demandant la validation de la pompe
        private void btn_Validation_1_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_1);
        }
        private void btn_Validation_2_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_2);
        }
        private void btn_Validation_3_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_3);
        }
        private void btn_Validation_4_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_4);
        }
        private void btn_Validation_5_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_5);
        }
        private void btn_Validation_6_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_6);
        }
        private void btn_Validation_7_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_7);
        }
        private void btn_Validation_8_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_8);
        }
        private void btn_Validation_9_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_9);
        }
        private void btn_Validation_10_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_10);
        }
        private void btn_Validation_11_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_11);
        }
        private void btn_Validation_12_Click(object sender, EventArgs e)
        {
            //validation_tracage_pompe(k.POMPE_12);
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer1_Click(object sender, EventArgs e)
        {
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_1, k.SERIE_POMPE_1, k.SERIE_EFF_1, k.SERIE_PUISS_1);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer2_Click(object sender, EventArgs e)
        {
            if (cBox_Modele2.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_2, k.SERIE_POMPE_2, k.SERIE_EFF_2, k.SERIE_PUISS_2);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer3_Click(object sender, EventArgs e)
        {
            if (cBox_Modele3.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_3, k.SERIE_POMPE_3, k.SERIE_EFF_3, k.SERIE_PUISS_3);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer4_Click(object sender, EventArgs e)
        {
            if (cBox_Modele4.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_4, k.SERIE_POMPE_4, k.SERIE_EFF_4, k.SERIE_PUISS_4);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer5_Click(object sender, EventArgs e)
        {
            if (cBox_Modele5.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_5, k.SERIE_POMPE_5, k.SERIE_EFF_5, k.SERIE_PUISS_5);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer6_Click(object sender, EventArgs e)
        {
            if (cBox_Modele6.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_6, k.SERIE_POMPE_6, k.SERIE_EFF_6, k.SERIE_PUISS_6);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer7_Click(object sender, EventArgs e)
        {
            if (cBox_Modele7.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_7, k.SERIE_POMPE_7, k.SERIE_EFF_7, k.SERIE_PUISS_7);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer8_Click(object sender, EventArgs e)
        {
            if (cBox_Modele8.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_8, k.SERIE_POMPE_8, k.SERIE_EFF_8, k.SERIE_PUISS_8);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer9_Click(object sender, EventArgs e)
        {
            if (cBox_Modele9.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_9, k.SERIE_POMPE_9, k.SERIE_EFF_9, k.SERIE_PUISS_9);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer10_Click(object sender, EventArgs e)
        {
            if (cBox_Modele10.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_10, k.SERIE_POMPE_10, k.SERIE_EFF_10, k.SERIE_PUISS_10);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer11_Click(object sender, EventArgs e)
        {
            if (cBox_Modele11.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_11, k.SERIE_POMPE_11, k.SERIE_EFF_11, k.SERIE_PUISS_11);
            }
        }
        //Evenement provenant des boutons pour effacer une courbe sur le graphique
        private void btn_effacer12_Click(object sender, EventArgs e)
        {
            if (cBox_Modele12.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_12, k.SERIE_POMPE_12, k.SERIE_EFF_12, k.SERIE_PUISS_12);
            }
        }

        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs1_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_1.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_1);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs2_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_2.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_2);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs3_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_3.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_3);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs4_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_4.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_4);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs5_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_5.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_5);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs6_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_6.BringToFront();
            desactiver_affichage_points_graphique(k.POMPE_6);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs7_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_7.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_7);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs8_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_8.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_8);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs9_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_9.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_9);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs10_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_10.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_10);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs11_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_11.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_11);
        }
        //Evenement provenant du bouton retour dans la fenetre de tablede valeur
        private void btn_retour_valeurs12_Click(object sender, EventArgs e)
        {
            //Mise en evidence du pnl principal de selection de pompe
            pnl_pompe_12.BringToFront();
            //Desactivation des points coordonnees sur la courbe
            desactiver_affichage_points_graphique(k.POMPE_12);
        }

        //Changement de disposition de pompe
        private void changement_disposition_pompe(int num_pompe, bool dispo)
        {
            inputs.pompes[num_pompe].disposition = dispo;
        }

        #endregion


        private void nUD_Vit_P1_ValueChanged(object sender, EventArgs e)
        {
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P1.Value = (int)nUD_Vit_P1.Value;
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_1].vitesse = (int)nUD_Vit_P1.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque1.SelectedIndex,
                 cBox_Serie1.SelectedIndex,
                     cBox_Modele1.SelectedIndex,
                         k.POMPE_1,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P2_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_2].vitesse = (int)nUD_Vit_P2.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P2.Value = (int)nUD_Vit_P2.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele2.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque2.SelectedIndex,
                 cBox_Serie2.SelectedIndex,
                     cBox_Modele2.SelectedIndex,
                         k.POMPE_2,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P3_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_3].vitesse = (int)nUD_Vit_P3.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P3.Value = (int)nUD_Vit_P3.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele3.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque3.SelectedIndex,
                 cBox_Serie3.SelectedIndex,
                     cBox_Modele3.SelectedIndex,
                         k.POMPE_3,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P4_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_4].vitesse = (int)nUD_Vit_P4.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P4.Value = (int)nUD_Vit_P4.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele4.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque4.SelectedIndex,
                 cBox_Serie4.SelectedIndex,
                     cBox_Modele4.SelectedIndex,
                         k.POMPE_4,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P5_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_5].vitesse = (int)nUD_Vit_P5.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P5.Value = (int)nUD_Vit_P5.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele5.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque5.SelectedIndex,
                 cBox_Serie5.SelectedIndex,
                     cBox_Modele5.SelectedIndex,
                         k.POMPE_5,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P6_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_6].vitesse = (int)nUD_Vit_P6.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P6.Value = (int)nUD_Vit_P6.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele6.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque6.SelectedIndex,
                 cBox_Serie6.SelectedIndex,
                     cBox_Modele6.SelectedIndex,
                         k.POMPE_6,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P7_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_7].vitesse = (int)nUD_Vit_P7.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P7.Value = (int)nUD_Vit_P7.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele7.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque7.SelectedIndex,
                 cBox_Serie7.SelectedIndex,
                     cBox_Modele7.SelectedIndex,
                         k.POMPE_7,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P8_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_8].vitesse = (int)nUD_Vit_P8.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P8.Value = (int)nUD_Vit_P8.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele8.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque8.SelectedIndex,
                 cBox_Serie8.SelectedIndex,
                     cBox_Modele8.SelectedIndex,
                         k.POMPE_8,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P9_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_9].vitesse = (int)nUD_Vit_P9.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P9.Value = (int)nUD_Vit_P9.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele9.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque9.SelectedIndex,
                 cBox_Serie9.SelectedIndex,
                     cBox_Modele9.SelectedIndex,
                         k.POMPE_9,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P10_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_10].vitesse = (int)nUD_Vit_P10.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P10.Value = (int)nUD_Vit_P10.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele10.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque10.SelectedIndex,
                 cBox_Serie10.SelectedIndex,
                     cBox_Modele10.SelectedIndex,
                         k.POMPE_10,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P11_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_11].vitesse = (int)nUD_Vit_P11.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P11.Value = (int)nUD_Vit_P11.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele11.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque11.SelectedIndex,
                 cBox_Serie11.SelectedIndex,
                     cBox_Modele11.SelectedIndex,
                         k.POMPE_11,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void nUD_Vit_P12_ValueChanged(object sender, EventArgs e)
        {
            //Encapsulation de la vitesse dans le tableau Inputs
            inputs.pompes[k.POMPE_12].vitesse = (int)nUD_Vit_P12.Value;
            //Association de la valeur du Scroll selon la vitesse du nUD
            Scroll_Vit_P12.Value = (int)nUD_Vit_P12.Value;
            //Tracage de la courbe selon nouveaux parametres
            if (cBox_Modele12.SelectedIndex != k.VIDE)
            {
                info_pompe(cBox_Marque12.SelectedIndex,
                 cBox_Serie12.SelectedIndex,
                     cBox_Modele12.SelectedIndex,
                         k.POMPE_12,
                            TDH_tot[k.NB_POINTS_TABLEUR]);
            }
        }

        private void Scroll_Vit_P1_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P1.Value = Scroll_Vit_P1.Value;
        }
        private void Scroll_Vit_P2_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P2.Value = Scroll_Vit_P2.Value;
        }
        private void Scroll_Vit_P3_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P3.Value = Scroll_Vit_P3.Value;
        }
        private void Scroll_Vit_P4_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P4.Value = Scroll_Vit_P4.Value;
        }
        private void Scroll_Vit_P5_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P5.Value = Scroll_Vit_P5.Value;
        }
        private void Scroll_Vit_P6_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P6.Value = Scroll_Vit_P6.Value;
        }
        private void Scroll_Vit_P7_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P7.Value = Scroll_Vit_P7.Value;
        }
        private void Scroll_Vit_P8_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P8.Value = Scroll_Vit_P8.Value;
        }
        private void Scroll_Vit_P9_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P9.Value = Scroll_Vit_P9.Value;
        }
        private void Scroll_Vit_P10_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P10.Value = Scroll_Vit_P10.Value;
        }
        private void Scroll_Vit_P11_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P11.Value = Scroll_Vit_P11.Value;
        }
        private void Scroll_Vit_P12_Scroll(object sender, ScrollEventArgs e)
        {
            //La valeur du Scroll vitesse devient la valeur du nUD vitesse
            nUD_Vit_P12.Value = Scroll_Vit_P12.Value;
        }

        private void nUD_Interval_ValueChanged(object sender, EventArgs e)
        {
            graphique_1.ChartAreas[0].Axes[0].Interval =
                Math.Round(
                    inputs.action.debit[k.DEBIT_AXE_X]
                        / (int)nUD_Interval_X.Value, 0);
            graphique_1.ChartAreas[1].AxisX.Interval =
                                Math.Round(
                    inputs.action.debit[k.DEBIT_AXE_X]
                        / (int)nUD_Interval_X.Value, 0);
            graphique_1.ChartAreas[2].AxisX.Interval =
                    Math.Round(
        inputs.action.debit[k.DEBIT_AXE_X]
            / (int)nUD_Interval_X.Value, 0);
        }

        private void nUD_Pression_ValueChanged(object sender, EventArgs e)
        {
            graphique_1.ChartAreas[0].Axes[1].RoundAxisValues();
            graphique_1.ChartAreas[1].Axes[1].RoundAxisValues();
            graphique_1.ChartAreas[2].Axes[1].RoundAxisValues();
        }

        private void nUD_Interval_Y_ValueChanged(object sender, EventArgs e)
        {
            graphique_1.ChartAreas[0].Axes[1].RoundAxisValues();
            graphique_1.ChartAreas[1].Axes[1].RoundAxisValues();
            graphique_1.ChartAreas[2].Axes[1].RoundAxisValues();
        }

        private void btn_RAZ_inputs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cette operation remet toutes les entrees utilisateur a zero \n Continuer?",
                                    "REMISE A ZERO",
                                        MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question)
                                                == DialogResult.Yes)
            {
                RAZ_entrees_utilisateur();
            }
        }

        private void RAZ_entrees_utilisateur()
        {
            //RAZ des informations client
            tBox_client_Nom.Text = "";
            tBox_client_Projet.Text = "";
            tBox_client_Description.Text = "";
            tBox_client_DoneBy.Text = "";
            tBox_client_Revision.Text = "";
            //Remise du logiciel en francais
            rBtn_Francais.Checked = true;
            rBtn_English.Checked = false;
            traduction_francais();
            //Remise des unites de debit en USGPM
            rBtn_USGPM.Checked = true;
            rBtn_m3h.Checked = false;
            //Remise des unites de longueurs en pieds
            rBtn_Pieds.Checked = true;
            rBtn_Metres.Checked = false;
            //Remise des unites de pression en fthH20
            rBtn_ftH20.Checked = true;
            rBtn_mH20.Checked = false;
            rBtn_PSI.Checked = false;
            rBtn_Pascal.Checked = false;
            //Remise des unite au facteur 1 (par defaut)
            inputs.acceuil.unit_debit = k.USPGM;
            inputs.acceuil.unit_longueur = k.PIEDS;
            inputs.acceuil.unit_pression = k.FTH20;
            //Changment des unites en texte pour les valeurs par defaut
            changement_unites();
            changement_texte_unites();
            //RAZ du debit
            nUD_Debit.Value = 1000;
            //RAZ du nombre d'intervals du graphique
            nUD_Interval_X.Value = 10;
            //RAZ des debit de points d'action
            nUD_X_action1.Value = nUD_X_action1.Minimum;
            nUD_X_action2.Value = nUD_X_action2.Minimum;
            nUD_X_action3.Value = nUD_X_action3.Minimum;
            //RAZ des pressions de points d'action
            nUD_Y_action1.Value = nUD_Y_action1.Minimum;
            nUD_Y_action2.Value = nUD_Y_action2.Minimum;
            nUD_Y_action3.Value = nUD_Y_action3.Minimum;
            //Remise des points d'action en droite
            checkBox_Droite1.Checked = true;
            checkBox_Droite2.Checked = true;
            checkBox_Droite3.Checked = true;
            //RAZ des checbox points
            checkBox_Point1.Checked = false;
            checkBox_Point2.Checked = false;
            checkBox_Point3.Checked = false;
            //Remise du mode automatique de legende d'action
            checkBox_Legend_Auto1.Checked = true;
            checkBox_Legend_Auto2.Checked = true;
            checkBox_Legend_Auto3.Checked = true;
            //Remise de la legende automatique par defaut
            textBox_Action1.Text = "Point d'action 1";
            textBox_Action2.Text = "Point d'action 1";
            textBox_Action3.Text = "Point d'action 1";
            //Vider les cBox de materiel
            cBox_materiel1.SelectedIndex = k.VIDE;
            cBox_materiel2.SelectedIndex = k.VIDE;
            cBox_materiel3.SelectedIndex = k.VIDE;
            cBox_materiel4.SelectedIndex = k.VIDE;
            cBox_materiel5.SelectedIndex = k.VIDE;
            //Vider les cBox de type
            cBox_Type1.SelectedIndex = k.VIDE;
            cBox_Type2.SelectedIndex = k.VIDE;
            cBox_Type3.SelectedIndex = k.VIDE;
            cBox_Type4.SelectedIndex = k.VIDE;
            cBox_Type5.SelectedIndex = k.VIDE;
            //Remise du nombre de ligne parralelle a 1
            nUD_LignesParr1.Value = nUD_LignesParr1.Minimum;
            nUD_LignesParr2.Value = nUD_LignesParr2.Minimum;
            nUD_LignesParr3.Value = nUD_LignesParr3.Minimum;
            nUD_LignesParr4.Value = nUD_LignesParr4.Minimum;
            nUD_LignesParr5.Value = nUD_LignesParr5.Minimum;
            //RAZ des longueurs de section
            nUD_Long1.Value = nUD_Long1.Minimum;
            nUD_Long2.Value = nUD_Long2.Minimum;
            nUD_Long3.Value = nUD_Long3.Minimum;
            nUD_Long4.Value = nUD_Long4.Minimum;
            nUD_Long5.Value = nUD_Long5.Minimum;
            //RAZ de la hauteur statique
            nUD_Static1.Value = nUD_Static1.Minimum;
            nUD_Static2.Value = nUD_Static2.Minimum;
            nUD_Static3.Value = nUD_Static3.Minimum;
            nUD_Static4.Value = nUD_Static4.Minimum;
            nUD_Static5.Value = nUD_Static5.Minimum;
            //RAZ du facteur de securite
            nUD_Safety_Factor1.Value = nUD_Safety_Factor1.Minimum;
            nUD_Safety_Factor2.Value = nUD_Safety_Factor2.Minimum;
            nUD_Safety_Factor3.Value = nUD_Safety_Factor3.Minimum;
            nUD_Safety_Factor4.Value = nUD_Safety_Factor4.Minimum;
            nUD_Safety_Factor5.Value = nUD_Safety_Factor5.Minimum;
            //RAZ des fittings
            if (lbl_Fitting_A.Text != "0")
            {
                nUD_A_1.Value = 0;
                nUD_A_2.Value = 0;
                nUD_A_3.Value = 0;
                nUD_A_4.Value = 0;
                nUD_A_5.Value = 0;
                nUD_A_6.Value = 0;
                nUD_A_7.Value = 0;
                nUD_A_8.Value = 0;
                nUD_A_9.Value = 0;
                nUD_A_10.Value = 0;

                cBox_Fitting_A_1.SelectedIndex = k.VIDE;
                cBox_Fitting_A_2.SelectedIndex = k.VIDE;
                cBox_Fitting_A_3.SelectedIndex = k.VIDE;
                cBox_Fitting_A_4.SelectedIndex = k.VIDE;
                cBox_Fitting_A_5.SelectedIndex = k.VIDE;
                cBox_Fitting_A_6.SelectedIndex = k.VIDE;
                cBox_Fitting_A_7.SelectedIndex = k.VIDE;
                cBox_Fitting_A_8.SelectedIndex = k.VIDE;
                cBox_Fitting_A_9.SelectedIndex = k.VIDE;
                cBox_Fitting_A_10.SelectedIndex = k.VIDE;
            }
            if (lbl_Fitting_B.Text != "0")
            {
                nUD_B_1.Value = 0;
                nUD_B_2.Value = 0;
                nUD_B_3.Value = 0;
                nUD_B_4.Value = 0;
                nUD_B_5.Value = 0;
                nUD_B_6.Value = 0;
                nUD_B_7.Value = 0;
                nUD_B_8.Value = 0;
                nUD_B_9.Value = 0;
                nUD_B_10.Value = 0;

                cBox_Fitting_B_1.SelectedIndex = k.VIDE;
                cBox_Fitting_B_2.SelectedIndex = k.VIDE;
                cBox_Fitting_B_3.SelectedIndex = k.VIDE;
                cBox_Fitting_B_4.SelectedIndex = k.VIDE;
                cBox_Fitting_B_5.SelectedIndex = k.VIDE;
                cBox_Fitting_B_6.SelectedIndex = k.VIDE;
                cBox_Fitting_B_7.SelectedIndex = k.VIDE;
                cBox_Fitting_B_8.SelectedIndex = k.VIDE;
                cBox_Fitting_B_9.SelectedIndex = k.VIDE;
                cBox_Fitting_B_10.SelectedIndex = k.VIDE;
            }
            if (lbl_Fitting_C.Text != "0")
            {
                nUD_C_1.Value = 0;
                nUD_C_2.Value = 0;
                nUD_C_3.Value = 0;
                nUD_C_4.Value = 0;
                nUD_C_5.Value = 0;
                nUD_C_6.Value = 0;
                nUD_C_7.Value = 0;
                nUD_C_8.Value = 0;
                nUD_C_9.Value = 0;
                nUD_C_10.Value = 0;

                cBox_Fitting_C_1.SelectedIndex = k.VIDE;
                cBox_Fitting_C_2.SelectedIndex = k.VIDE;
                cBox_Fitting_C_3.SelectedIndex = k.VIDE;
                cBox_Fitting_C_4.SelectedIndex = k.VIDE;
                cBox_Fitting_C_5.SelectedIndex = k.VIDE;
                cBox_Fitting_C_6.SelectedIndex = k.VIDE;
                cBox_Fitting_C_7.SelectedIndex = k.VIDE;
                cBox_Fitting_C_8.SelectedIndex = k.VIDE;
                cBox_Fitting_C_9.SelectedIndex = k.VIDE;
                cBox_Fitting_C_10.SelectedIndex = k.VIDE;
            }
            if (lbl_Fitting_D.Text != "0")
            {
                nUD_D_1.Value = 0;
                nUD_D_2.Value = 0;
                nUD_D_3.Value = 0;
                nUD_D_4.Value = 0;
                nUD_D_5.Value = 0;
                nUD_D_6.Value = 0;
                nUD_D_7.Value = 0;
                nUD_D_8.Value = 0;
                nUD_D_9.Value = 0;
                nUD_D_10.Value = 0;

                cBox_Fitting_D_1.SelectedIndex = k.VIDE;
                cBox_Fitting_D_2.SelectedIndex = k.VIDE;
                cBox_Fitting_D_3.SelectedIndex = k.VIDE;
                cBox_Fitting_D_4.SelectedIndex = k.VIDE;
                cBox_Fitting_D_5.SelectedIndex = k.VIDE;
                cBox_Fitting_D_6.SelectedIndex = k.VIDE;
                cBox_Fitting_D_7.SelectedIndex = k.VIDE;
                cBox_Fitting_D_8.SelectedIndex = k.VIDE;
                cBox_Fitting_D_9.SelectedIndex = k.VIDE;
                cBox_Fitting_D_10.SelectedIndex = k.VIDE;
            }
            if (lbl_Fitting_E.Text != "0")
            {
                nUD_E_1.Value = 0;
                nUD_E_2.Value = 0;
                nUD_E_3.Value = 0;
                nUD_E_4.Value = 0;
                nUD_E_5.Value = 0;
                nUD_E_6.Value = 0;
                nUD_E_7.Value = 0;
                nUD_E_8.Value = 0;
                nUD_E_9.Value = 0;
                nUD_E_10.Value = 0;

                cBox_Fitting_E_1.SelectedIndex = k.VIDE;
                cBox_Fitting_E_2.SelectedIndex = k.VIDE;
                cBox_Fitting_E_3.SelectedIndex = k.VIDE;
                cBox_Fitting_E_4.SelectedIndex = k.VIDE;
                cBox_Fitting_E_5.SelectedIndex = k.VIDE;
                cBox_Fitting_E_6.SelectedIndex = k.VIDE;
                cBox_Fitting_E_7.SelectedIndex = k.VIDE;
                cBox_Fitting_E_8.SelectedIndex = k.VIDE;
                cBox_Fitting_E_9.SelectedIndex = k.VIDE;
                cBox_Fitting_E_10.SelectedIndex = k.VIDE;
            }
            //Effacer les series de pompes actives
            if (cBox_Modele1.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_1, k.SERIE_POMPE_1, k.SERIE_EFF_1, k.SERIE_PUISS_1);
            }
            if (cBox_Modele2.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_2, k.SERIE_POMPE_2, k.SERIE_EFF_2, k.SERIE_PUISS_2);
            }
            if (cBox_Modele3.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_3, k.SERIE_POMPE_3, k.SERIE_EFF_3, k.SERIE_PUISS_3);
            }
            if (cBox_Modele4.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_4, k.SERIE_POMPE_4, k.SERIE_EFF_4, k.SERIE_PUISS_4);
            }
            if (cBox_Modele5.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_5, k.SERIE_POMPE_5, k.SERIE_EFF_5, k.SERIE_PUISS_5);
            }
            if (cBox_Modele6.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_6, k.SERIE_POMPE_6, k.SERIE_EFF_6, k.SERIE_PUISS_6);
            }
            if (cBox_Modele7.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_7, k.SERIE_POMPE_7, k.SERIE_EFF_7, k.SERIE_PUISS_7);
            }
            if (cBox_Modele8.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_8, k.SERIE_POMPE_8, k.SERIE_EFF_8, k.SERIE_PUISS_8);
            }
            if (cBox_Modele9.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_9, k.SERIE_POMPE_9, k.SERIE_EFF_9, k.SERIE_PUISS_9);
            }
            if (cBox_Modele10.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_10, k.SERIE_POMPE_10, k.SERIE_EFF_10, k.SERIE_PUISS_10);
            }
            if (cBox_Modele11.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_11, k.SERIE_POMPE_11, k.SERIE_EFF_11, k.SERIE_PUISS_11);
            }
            if (cBox_Modele12.SelectedIndex != k.VIDE)
            {
                effacer_serie(k.POMPE_12, k.SERIE_POMPE_12, k.SERIE_EFF_12, k.SERIE_PUISS_12);
            }
        }

        private void btn_Edit_Mat1_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Materiel.BringToFront();

            lbl_Titre_Edition_Mat.Text = "Editon de Materiel - Section A";

            if (cBox_materiel1.SelectedIndex != k.VIDE)
            {
                tBox_Nom_Edit.Text = inputs.section[k.SECTION_A].materiel;
                nUD_Constante_Edit.Value = (int)inputs.section[k.SECTION_A].constante_hazen_williams;
            }
            else if (cBox_materiel1.SelectedIndex == k.VIDE)
            {
                tBox_Nom_Edit.Text = "Autre";
                nUD_Constante_Edit.Value = 100;
            }
        }

        private void btn_Edit_Mat_Cancel_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = true;

            pnl_Edition_Materiel.SendToBack();
        }

        private void btn_Edit_Mat_Ok_Click(object sender, EventArgs e)
        {
            //Concatenation du nouveau nom et de la constante avec un / entre les deux
            string materiel_edite = String.Concat(tBox_Nom_Edit.Text, " / ", nUD_Constante_Edit.Value);
            //Si le materiel est de la section A
            if (lbl_Titre_Edition_Mat.Text.EndsWith("A"))
            {
                //Si la section est deja existante, indication a l'utilisateur
                if (cBox_materiel1.Items.Contains(materiel_edite))
                {
                    MessageBox.Show("Deja existant");
                }
                //Si la selection n'existe pas, donc valide, fermeture de la 
                //fenetre et ajout au cBox
                else if (cBox_materiel1.Items.Contains(materiel_edite) == false)
                {
                    //Insertion au debut de liste
                    cBox_materiel1.Items.Insert(0, materiel_edite);
                    //Reactivation du panneau de selection
                    pnl_Scroll_Tuyauterie.Enabled = true;
                    //Retrait du panneau d'edition de materiel
                    pnl_Edition_Materiel.SendToBack();
                    //
                    cBox_materiel1.SelectedIndex = 0;
                }
            }

            if (lbl_Titre_Edition_Mat.Text.EndsWith("B"))
            {
                //Si la section est deja existante, indication a l'utilisateur
                if (cBox_materiel2.Items.Contains(materiel_edite))
                {
                    MessageBox.Show("Deja existant");
                }
                //Si la selection n'existe pas, donc valide, fermeture de la 
                //fenetre et ajout au cBox
                else if (cBox_materiel2.Items.Contains(materiel_edite) == false)
                {
                    //Insertion au debut de liste
                    cBox_materiel2.Items.Insert(0, materiel_edite);
                    //Reactivation du panneau de selection
                    pnl_Scroll_Tuyauterie.Enabled = true;
                    //Retrait du panneau d'edition de materiel
                    pnl_Edition_Materiel.SendToBack();
                    //
                    cBox_materiel2.SelectedIndex = 0;
                }
            }

            if (lbl_Titre_Edition_Mat.Text.EndsWith("C"))
            {
                //Si la section est deja existante, indication a l'utilisateur
                if (cBox_materiel3.Items.Contains(materiel_edite))
                {
                    MessageBox.Show("Deja existant");
                }
                //Si la selection n'existe pas, donc valide, fermeture de la 
                //fenetre et ajout au cBox
                else if (cBox_materiel3.Items.Contains(materiel_edite) == false)
                {
                    //Insertion au debut de liste
                    cBox_materiel3.Items.Insert(0, materiel_edite);
                    //Reactivation du panneau de selection
                    pnl_Scroll_Tuyauterie.Enabled = true;
                    //Retrait du panneau d'edition de materiel
                    pnl_Edition_Materiel.SendToBack();
                    //
                    cBox_materiel3.SelectedIndex = 0;
                }
            }

            if (lbl_Titre_Edition_Mat.Text.EndsWith("D"))
            {
                //Si la section est deja existante, indication a l'utilisateur
                if (cBox_materiel4.Items.Contains(materiel_edite))
                {
                    MessageBox.Show("Deja existant");
                }
                //Si la selection n'existe pas, donc valide, fermeture de la 
                //fenetre et ajout au cBox
                else if (cBox_materiel4.Items.Contains(materiel_edite) == false)
                {
                    //Insertion au debut de liste
                    cBox_materiel4.Items.Insert(0, materiel_edite);
                    //Reactivation du panneau de selection
                    pnl_Scroll_Tuyauterie.Enabled = true;
                    //Retrait du panneau d'edition de materiel
                    pnl_Edition_Materiel.SendToBack();
                    //
                    cBox_materiel4.SelectedIndex = 0;
                }
            }

            if (lbl_Titre_Edition_Mat.Text.EndsWith("E"))
            {
                //Si la section est deja existante, indication a l'utilisateur
                if (cBox_materiel5.Items.Contains(materiel_edite))
                {
                    MessageBox.Show("Deja existant");
                }
                //Si la selection n'existe pas, donc valide, fermeture de la 
                //fenetre et ajout au cBox
                else if (cBox_materiel5.Items.Contains(materiel_edite) == false)
                {
                    //Insertion au debut de liste
                    cBox_materiel5.Items.Insert(0, materiel_edite);
                    //Reactivation du panneau de selection
                    pnl_Scroll_Tuyauterie.Enabled = true;
                    //Retrait du panneau d'edition de materiel
                    pnl_Edition_Materiel.SendToBack();
                    //
                    cBox_materiel5.SelectedIndex = 0;
                }
            }
        }

        private void btn_Edit_Mat2_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Materiel.BringToFront();

            lbl_Titre_Edition_Mat.Text = "Editon de Materiel - Section B";

            if (cBox_materiel2.SelectedIndex != k.VIDE)
            {
                tBox_Nom_Edit.Text = inputs.section[k.SECTION_B].materiel;
                nUD_Constante_Edit.Value = (int)inputs.section[k.SECTION_B].constante_hazen_williams;
            }
            else if (cBox_materiel2.SelectedIndex == k.VIDE)
            {
                tBox_Nom_Edit.Text = "Autre";
                nUD_Constante_Edit.Value = 100;
            }
        }

        private void btn_Edit_Mat3_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Materiel.BringToFront();

            lbl_Titre_Edition_Mat.Text = "Editon de Materiel - Section C";

            if (cBox_materiel3.SelectedIndex != k.VIDE)
            {
                tBox_Nom_Edit.Text = inputs.section[k.SECTION_C].materiel;
                nUD_Constante_Edit.Value = (int)inputs.section[k.SECTION_C].constante_hazen_williams;
            }
            else if (cBox_materiel3.SelectedIndex == k.VIDE)
            {
                tBox_Nom_Edit.Text = "Autre";
                nUD_Constante_Edit.Value = 100;
            }
        }

        private void btn_Edit_Mat4_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Materiel.BringToFront();

            lbl_Titre_Edition_Mat.Text = "Editon de Materiel - Section D";

            if (cBox_materiel4.SelectedIndex != k.VIDE)
            {
                tBox_Nom_Edit.Text = inputs.section[k.SECTION_D].materiel;
                nUD_Constante_Edit.Value = (int)inputs.section[k.SECTION_D].constante_hazen_williams;
            }
            else if (cBox_materiel4.SelectedIndex == k.VIDE)
            {
                tBox_Nom_Edit.Text = "Autre";
                nUD_Constante_Edit.Value = 100;
            }
        }

        private void btn_Edit_Mat5_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Materiel.BringToFront();

            lbl_Titre_Edition_Mat.Text = "Editon de Materiel - Section E";

            if (cBox_materiel5.SelectedIndex != k.VIDE)
            {
                tBox_Nom_Edit.Text = inputs.section[k.SECTION_E].materiel;
                nUD_Constante_Edit.Value = (int)inputs.section[k.SECTION_E].constante_hazen_williams;
            }
            else if (cBox_materiel5.SelectedIndex == k.VIDE)
            {
                tBox_Nom_Edit.Text = "Autre";
                nUD_Constante_Edit.Value = 100;
            }
        }

        //Remise de la liste de materiel dans le cBox aux entrees par defaut
        private void btn_Reset_Mat1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cette action effacera toutes les nouvelles entrees de materiels. \n Voulez-vous continuer?",
                                    "SAUVEGARDE",
                                        MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question)
                                                == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Materiel(Materiel.nb, k.SECTION_A);
            }
        }

        private void btn_Reset_Mat2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cette action effacera toutes les nouvelles entrees de materiels. \n Voulez-vous continuer?",
                        "SAUVEGARDE",
                            MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)
                                    == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Materiel(Materiel.nb, k.SECTION_B);
            }
        }

        private void btn_Reset_Mat3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cette action effacera toutes les nouvelles entrees de materiels. \n Voulez-vous continuer?",
                        "SAUVEGARDE",
                            MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)
                                    == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Materiel(Materiel.nb, k.SECTION_C);
            }
        }

        private void btn_Reset_Mat4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cette action effacera toutes les nouvelles entrees de materiels. \n Voulez-vous continuer?",
                        "SAUVEGARDE",
                            MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)
                                    == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Materiel(Materiel.nb, k.SECTION_D);
            }
        }

        private void btn_Reset_Mat5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cette action effacera toutes les nouvelles entrees de materiels. \n Voulez-vous continuer?",
                        "SAUVEGARDE",
                            MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)
                                    == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Materiel(Materiel.nb, k.SECTION_E);
            }
        }


        private void btn_Edit_Pipe1_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Pipe.BringToFront();

            lbl_Titre_Edition_Pipe.Text = "Editon de tuyauterie - Section A";

            if (cBox_Type1.SelectedIndex != k.VIDE)
            {
                tBox_Type_Edit.Text = inputs.section[k.SECTION_A].pipe_type;
                tBox_Diametre_Edit.Text = inputs.section[k.SECTION_A].diametre_interne.ToString();
            }
            else if (cBox_Type1.SelectedIndex == k.VIDE)
            {
                tBox_Type_Edit.Text = "Autre";
                tBox_Diametre_Edit.Text = "1,000";
            }
        }

        private void btn_Edit_Pipe2_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Pipe.BringToFront();

            lbl_Titre_Edition_Pipe.Text = "Editon de tuyauterie - Section B";

            if (cBox_Type2.SelectedIndex != k.VIDE)
            {
                tBox_Type_Edit.Text = inputs.section[k.SECTION_B].pipe_type;
                tBox_Diametre_Edit.Text = inputs.section[k.SECTION_B].diametre_interne.ToString();
            }
            else if (cBox_Type2.SelectedIndex == k.VIDE)
            {
                tBox_Type_Edit.Text = "Autre";
                tBox_Diametre_Edit.Text = "1,000";
            }
        }

        private void btn_Edit_Pipe3_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Pipe.BringToFront();

            lbl_Titre_Edition_Pipe.Text = "Editon de tuyauterie - Section C";

            if (cBox_Type3.SelectedIndex != k.VIDE)
            {
                tBox_Type_Edit.Text = inputs.section[k.SECTION_C].pipe_type;
                tBox_Diametre_Edit.Text = inputs.section[k.SECTION_C].diametre_interne.ToString();
            }
            else if (cBox_Type3.SelectedIndex == k.VIDE)
            {
                tBox_Type_Edit.Text = "Autre";
                tBox_Diametre_Edit.Text = "1,000";
            }
        }

        private void btn_Edit_Pipe4_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Pipe.BringToFront();

            lbl_Titre_Edition_Pipe.Text = "Editon de tuyauterie - Section D";

            if (cBox_Type4.SelectedIndex != k.VIDE)
            {
                tBox_Type_Edit.Text = inputs.section[k.SECTION_D].pipe_type;
                tBox_Diametre_Edit.Text = inputs.section[k.SECTION_D].diametre_interne.ToString();
            }
            else if (cBox_Type4.SelectedIndex == k.VIDE)
            {
                tBox_Type_Edit.Text = "Autre";
                tBox_Diametre_Edit.Text = "1,000";
            }
        }

        private void btn_Edit_Pipe5_Click(object sender, EventArgs e)
        {
            pnl_Scroll_Tuyauterie.Enabled = false;

            pnl_Edition_Pipe.BringToFront();

            lbl_Titre_Edition_Pipe.Text = "Editon de tuyauterie - Section E";

            if (cBox_Type5.SelectedIndex != k.VIDE)
            {
                tBox_Type_Edit.Text = inputs.section[k.SECTION_E].pipe_type;
                tBox_Diametre_Edit.Text = inputs.section[k.SECTION_E].diametre_interne.ToString();
            }
            else if (cBox_Type5.SelectedIndex == k.VIDE)
            {
                tBox_Type_Edit.Text = "Autre";
                tBox_Diametre_Edit.Text = "1,000";
            }
        }



        private void btn_Edit_Pipe_Ok_Click(object sender, EventArgs e)
        {
            if (tBox_Diametre_Edit.Text != "" && tBox_Diametre_Edit.Text != "0")
            {
                //Concatenation du nouveau nom et de la constante avec un / entre les deux
                string pipe_edite = String.Concat(tBox_Type_Edit.Text, " / ", tBox_Diametre_Edit.Text);
                //Si le materiel est de la section A
                if (lbl_Titre_Edition_Pipe.Text.EndsWith("A"))
                {
                    //Si la section est deja existante, indication a l'utilisateur
                    if (cBox_Type1.Items.Contains(pipe_edite))
                    {
                        MessageBox.Show("Deja existant");
                    }
                    //Si la selection n'existe pas, donc valide, fermeture de la 
                    //fenetre et ajout au cBox
                    else if (cBox_Type1.Items.Contains(pipe_edite) == false)
                    {
                        //Insertion au debut de liste
                        cBox_Type1.Items.Insert(0, pipe_edite);
                        //Reactivation du panneau de selection
                        pnl_Scroll_Tuyauterie.Enabled = true;
                        //Retrait du panneau d'edition de materiel
                        pnl_Edition_Pipe.SendToBack();
                        //
                        cBox_Type1.SelectedIndex = 0;
                    }
                }
                //Si le materiel est de la section A
                if (lbl_Titre_Edition_Pipe.Text.EndsWith("B"))
                {
                    //Si la section est deja existante, indication a l'utilisateur
                    if (cBox_Type2.Items.Contains(pipe_edite))
                    {
                        MessageBox.Show("Deja existant");
                    }
                    //Si la selection n'existe pas, donc valide, fermeture de la 
                    //fenetre et ajout au cBox
                    else if (cBox_Type2.Items.Contains(pipe_edite) == false)
                    {
                        //Insertion au debut de liste
                        cBox_Type2.Items.Insert(0, pipe_edite);
                        //Reactivation du panneau de selection
                        pnl_Scroll_Tuyauterie.Enabled = true;
                        //Retrait du panneau d'edition de materiel
                        pnl_Edition_Pipe.SendToBack();
                        //
                        cBox_Type2.SelectedIndex = 0;
                    }
                }
                //Si le materiel est de la section A
                if (lbl_Titre_Edition_Pipe.Text.EndsWith("C"))
                {
                    //Si la section est deja existante, indication a l'utilisateur
                    if (cBox_Type3.Items.Contains(pipe_edite))
                    {
                        MessageBox.Show("Deja existant");
                    }
                    //Si la selection n'existe pas, donc valide, fermeture de la 
                    //fenetre et ajout au cBox
                    else if (cBox_Type3.Items.Contains(pipe_edite) == false)
                    {
                        //Insertion au debut de liste
                        cBox_Type3.Items.Insert(0, pipe_edite);
                        //Reactivation du panneau de selection
                        pnl_Scroll_Tuyauterie.Enabled = true;
                        //Retrait du panneau d'edition de materiel
                        pnl_Edition_Pipe.SendToBack();
                        //
                        cBox_Type3.SelectedIndex = 0;
                    }
                }
                //Si le materiel est de la section A
                if (lbl_Titre_Edition_Pipe.Text.EndsWith("D"))
                {
                    //Si la section est deja existante, indication a l'utilisateur
                    if (cBox_Type4.Items.Contains(pipe_edite))
                    {
                        MessageBox.Show("Deja existant");
                    }
                    //Si la selection n'existe pas, donc valide, fermeture de la 
                    //fenetre et ajout au cBox
                    else if (cBox_Type4.Items.Contains(pipe_edite) == false)
                    {
                        //Insertion au debut de liste
                        cBox_Type4.Items.Insert(0, pipe_edite);
                        //Reactivation du panneau de selection
                        pnl_Scroll_Tuyauterie.Enabled = true;
                        //Retrait du panneau d'edition de materiel
                        pnl_Edition_Pipe.SendToBack();
                        //
                        cBox_Type4.SelectedIndex = 0;
                    }
                }
                //Si le materiel est de la section A
                if (lbl_Titre_Edition_Pipe.Text.EndsWith("E"))
                {
                    //Si la section est deja existante, indication a l'utilisateur
                    if (cBox_Type5.Items.Contains(pipe_edite))
                    {
                        MessageBox.Show("Deja existant");
                    }
                    //Si la selection n'existe pas, donc valide, fermeture de la 
                    //fenetre et ajout au cBox
                    else if (cBox_Type5.Items.Contains(pipe_edite) == false)
                    {
                        //Insertion au debut de liste
                        cBox_Type5.Items.Insert(0, pipe_edite);
                        //Reactivation du panneau de selection
                        pnl_Scroll_Tuyauterie.Enabled = true;
                        //Retrait du panneau d'edition de materiel
                        pnl_Edition_Pipe.SendToBack();
                        //
                        cBox_Type5.SelectedIndex = 0;
                    }
                }
            }
            else if (tBox_Diametre_Edit.Text == "" || tBox_Diametre_Edit.Text == "0")
            {
                MessageBox.Show("Cette entree n'est valide \n Un dimametre doit etre entre");
            }
        }

        private void tBox_Diametre_Edit_Validated(object sender, EventArgs e)
        {
            //Variable tampon utile pour la fonction
            string[] buffer;
            //Si le textBox n'est pas vide
            if (tBox_Diametre_Edit.Text != "")
            {
                //Tentative de conversion du text en chiffre
                try
                {
                    //Separation du texte s'il y a un point
                    buffer = tBox_Diametre_Edit.Text.Split('.');
                    //S'il y a un point, le text est splite donc plus d'une case
                    //dans le tableau split
                    if (buffer.Length > 1)
                    {
                        if (buffer[1].Length > 5)
                        {
                            buffer[1] = buffer[1].Remove(5);
                        }

                        //Concatenation de la premiere case, suivie d'une vigule, suivie 
                        //de la deuxieme case
                        tBox_Diametre_Edit.Text = String.Concat(buffer[0], ",", buffer[1]);
                    }
                    //Texte vers un double, en fait, cette ligne sert a verifier
                    //si le format est valide pour le calcul.  Donc, si le texte
                    //contient une lettre => incompatible au transfert en double
                    double toto = Math.Round(double.Parse(tBox_Diametre_Edit.Text), 4);
                }
                //Si un caractere n'est pas valide pour le transfert en double
                //Avertissement d'invalidite
                catch
                {
                    MessageBox.Show("Entree non valide");
                }
            }
        }

        private void btn_Reset_Pipe1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Cette action effacera toutes les nouvelles entrees de tuyauterie. " +
                    "\n     Retour aux valeurs par defaut " +
                        "\n\n Voulez-vous continuer?",
             "REMISE AUX VALEURS PAR DEFAUT",
                 MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question)
                         == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Pipe(Pipes.nb, k.SECTION_A);
            }
        }

        private void btn_Reset_Pipe2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Cette action effacera toutes les nouvelles entrees de tuyauterie. " +
                    "\n     Retour aux valeurs par defaut " +
                        "\n\n Voulez-vous continuer?",
             "REMISE AUX VALEURS PAR DEFAUT",
                MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                        == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Pipe(Pipes.nb, k.SECTION_B);
            }
        }

        private void btn_Reset_Pipe3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Cette action effacera toutes les nouvelles entrees de tuyauterie. " +
                    "\n     Retour aux valeurs par defaut " +
                        "\n\n Voulez-vous continuer?",
            "REMISE AUX VALEURS PAR DEFAUT",
                 MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question)
                         == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Pipe(Pipes.nb, k.SECTION_C);
            }
        }

        private void btn_Reset_Pipe4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Cette action effacera toutes les nouvelles entrees de tuyauterie. " +
                    "\n     Retour aux valeurs par defaut " +
                        "\n\n Voulez-vous continuer?",
             "REMISE AUX VALEURS PAR DEFAUT",
                 MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question)
                         == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Pipe(Pipes.nb, k.SECTION_D);
            }
        }

        private void btn_Reset_Pipe5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Cette action effacera toutes les nouvelles entrees de tuyauterie. " +
                    "\n     Retour aux valeurs par defaut " +
                        "\n\n Voulez-vous continuer?",
             "REMISE AUX VALEURS PAR DEFAUT",
                 MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question)
                         == DialogResult.Yes)
            {
                //Demande la section pour savoir quel cBox remettre a zero
                reset_cBox_Pipe(Pipes.nb, k.SECTION_E);
            }
        }
        //Bouton cancel permettant de simplement annuler l'edition en cours
        private void btn_Edit_Pipe_Cancel_Click(object sender, EventArgs e)
        {
            //Retour au fonctionnement normal
            pnl_Scroll_Tuyauterie.Enabled = true;
            //Remise en arriere plan du menu d'edition
            pnl_Edition_Pipe.SendToBack();
        }

        //
        private void btn_Curseur_Click(object sender, EventArgs e)
        {
            //Si le curseur du graphique est disponible, on le retire
            if (graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.IsUserEnabled == true)
            {
                //Bloquage des curseurs en X des deux graphique et du Y au
                //principal
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                CursorX.IsUserEnabled = false;
                graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].
                                                CursorX.IsUserEnabled = false;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                CursorY.IsUserEnabled = false;
                graphique_1.ChartAreas[2].
                                CursorY.IsUserEnabled = false;
                //Positionnement des curseurs en position zero
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.Position = 0;
                graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorX.Position = 0;
                graphique_1.ChartAreas[2].CursorX.Position = 0;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.Position = 0;
                //Couleur des curseurs transparent pour retirer la visibilite
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.LineColor =
                                                            Color.Transparent;
                graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorX.LineColor =
                                                            Color.Transparent;
                graphique_1.ChartAreas[2].CursorX.LineColor =
                                            Color.Transparent;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.LineColor =
                                                            Color.Transparent;
                //
                btn_Curseur.Text = "I";

                btn_Curseur.BackColor = Color.SpringGreen;
            }
            //Si le curseur du graphique est non disponible, on reactive
            else if (graphique_1.ChartAreas[0].CursorX.IsUserEnabled == false)
            {
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                    CursorX.IsUserEnabled =
                                                                        true;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                    CursorY.IsUserEnabled =
                                                                        true;

                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.LineColor =
                                                                    Color.Red;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.LineColor =
                                                                    Color.Red;

                btn_Curseur.Text = "O";

                btn_Curseur.BackColor = Color.Firebrick;
            }
        }

        //Deplacement de la souris au dessus du graphique
        private void graphique_1_MouseMove(object sender, MouseEventArgs e)
        {
            //Si le curseur est en mode automatique
            if (checkBox_Curseur_Auto.Checked && capture_ecran == false)
            {
                //Si le curseur est modifiable, le curseur et le panneau des 
                //coordonnees se deplacent et suivent le pointeur de la souris
                if (graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                CursorX.IsUserEnabled == true)
                {
                    //Emplacement du panneau de coordonnees
                    pnl_curseur.Location =
                        new Point(e.X                               //X
                                    + graphique_1.Location.X
                                        + (k.BORDURE_PNL * k.PROPORTION_HUIT),
                                e.Y                                 //Y
                                    - (k.BORDURE_PNL * k.PROPORTION_HUIT));
                    //Emplacement du curseur sur le graphique en position(selon
                    // les axes) par rapport a la position(en pixel de la souris)
                    graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                CursorX.SetCursorPixelPosition
                                                    (new Point(e.X, e.Y), true);
                    graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                CursorY.SetCursorPixelPosition
                                                    (new Point(e.X, e.Y), true);

                    //graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].
                    //                            CursorX.SetCursorPixelPosition
                    //                                (new Point(e.X, e.Y), true);
                    graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].
                            CursorX.Position = (int)graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.Position;
                    graphique_1.ChartAreas[2].
        CursorX.Position = (int)graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.Position;
                    //Ecriture de la position (selon les axes) du curseur dans le
                    //panneau de coordonnees
                    lbl_curseur_X.Text =
                            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                    CursorX.Position.ToString();
                    lbl_curseur_Y.Text =
                            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                                    CursorY.Position.ToString();
                }
            }
        }

        //Evenement quand la souris quitte le graphique
        private void graphique_1_MouseLeave(object sender, EventArgs e)
        {
            //Si le Curseru est en mode automatique,
            if (checkBox_Curseur_Auto.Checked)
            {
                retirer_curseur();
            }
        }
        //Retrait du curseur et du panneau de coordonnees
        private void retirer_curseur()
        {
            //Positionne le curseur a 0, hors vision
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.Position = 0;
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.Position = 0;
            //Rend le curseur tranparent, hors vision
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.LineColor =
                                                            Color.Transparent;
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.LineColor =
                                                            Color.Transparent;
            //Retrait de la visibilite du panneau de coordonnees
            pnl_curseur.Visible = false;
            //Positionne le curseur a 0, hors vision
            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorX.Position = 0;
            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorY.Position = 0;
            //Rend le curseur tranparent, hors vision
            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorX.LineColor =
                                                            Color.Transparent;
            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorY.LineColor =
                                                            Color.Transparent;
            //Positionne le curseur a 0, hors vision
            graphique_1.ChartAreas[k.GRAPH_TROIS].CursorX.Position = 0;
            graphique_1.ChartAreas[k.GRAPH_TROIS].CursorY.Position = 0;
            //Rend le curseur tranparent, hors vision
            graphique_1.ChartAreas[k.GRAPH_TROIS].CursorX.LineColor =
                                                            Color.Transparent;
            graphique_1.ChartAreas[k.GRAPH_TROIS].CursorY.LineColor =
                                                            Color.Transparent;
        }
        //Activation du curseur graphique
        private void activer_curseur()
        {
            //Rend la selection du curseur accessible
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.IsUserEnabled =
                                                                        true;
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.IsUserEnabled =
                                                                        true;
            //Couleur du curseur rouge, pour la visibilite
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.LineColor =
                                                                    Color.Red;
            graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.LineColor =
                                                                    Color.Red;
            //Visibilite du curseur
            pnl_curseur.Visible = true;
            //Rend la selection du curseur accessible
            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorX.IsUserEnabled =
                                                                        true;
            //Couleur du curseur rouge, pour la visibilite
            graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorX.LineColor =
                                                                    Color.Red;
            //Rend la selection du curseur accessible
            graphique_1.ChartAreas[k.GRAPH_TROIS].CursorX.IsUserEnabled =
                                                                        true;
            //Couleur du curseur rouge, pour la visibilite
            graphique_1.ChartAreas[k.GRAPH_TROIS].CursorX.LineColor =
                                                                    Color.Red;
        }

        //Modification de la valeur manuelle voulue pour le curseur en X
        private void nUD_Curseur_Man_X_ValueChanged(object sender, EventArgs e)
        {
            //Redonne le focus au graphique, pour eviter la modification non
            //voulues de la valeur
            graphique_1.Focus();
            //Si le curseur graphique est en mode Manuel
            if (checkBox_Curseur_Man.Checked)
            {
                //Le curseur en X des deux graphique prennent la valeur entree
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                            CursorX.Position =
                                                (int)nUD_Curseur_Man_X.Value;
                graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].
                                            CursorX.Position =
                                                (int)nUD_Curseur_Man_X.Value;
                graphique_1.ChartAreas[2].
                            CursorX.Position =
                                (int)nUD_Curseur_Man_X.Value;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.IsUserEnabled = false;
            }
        }
        //Modification de la valeur manuelle voulue pour le curseur en Y
        private void nUD_Curseur_Man_Y_ValueChanged(object sender, EventArgs e)
        {
            //Redonne le focus au graphique, pour eviter la modification non
            //voulue de la valeur
            graphique_1.Focus();
            //Si le curseur graphique est en mode Manuel
            if (checkBox_Curseur_Man.Checked)
            {
                //Le curseur du graphique principal prend la valeur entree 
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                            CursorY.Position =
                                                (int)nUD_Curseur_Man_Y.Value;
            }
        }
        //Selection du mode automatique pour le curseur graphique
        private void checkBox_Curseur_Auto_Click(object sender, EventArgs e)
        {
            //Changement d'etat pour le checkbox
            if (checkBox_Curseur_Auto.Checked == false)
            {
                checkBox_Curseur_Auto.Checked = true;
                checkBox_Curseur_Semi_Auto.Checked = false;
                checkBox_Curseur_Man.Checked = false;
            }
        }
        //Selection du mode semi-automatique pour le curseur graphique
        private void checkBox_Curseur_Semi_Auto_Click(object sender, EventArgs e)
        {
            //Changement d'etat du checkbox
            if (checkBox_Curseur_Semi_Auto.Checked == false)
            {
                checkBox_Curseur_Auto.Checked = false;
                checkBox_Curseur_Semi_Auto.Checked = true;
                checkBox_Curseur_Man.Checked = false;
                //Activation de la modification du curseur graphique par click
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.IsUserEnabled
                                                                        = true;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.IsUserEnabled
                                                                        = true;
                //Remise de la couleur rouge pour le curseur
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.LineColor
                                                                    = Color.Red;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.LineColor
                                                                    = Color.Red;
                //Coordonnees remises a zero
                lbl_curseur_X.Text = "0";
                lbl_curseur_Y.Text = "0";
                //Visibilite du panneau d'affichage des coordonnees du curseur
                pnl_curseur.Visible = true;
            }
        }
        //Selection du mode Manuel du curseur graphique
        private void checkBox_Curseur_Man_Click(object sender, EventArgs e)
        {
            //Changement d'etat du mode du curseur
            if (checkBox_Curseur_Man.Checked == false)
            {
                checkBox_Curseur_Auto.Checked = false;
                checkBox_Curseur_Semi_Auto.Checked = false;
                checkBox_Curseur_Man.Checked = true;
                //Positionnement du curseur selon les coordonnees entrees dans
                //le numeric up/down
                //Positionnement X
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.Position =
                                                (int)nUD_Curseur_Man_X.Value;
                graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorX.Position =
                                                (int)nUD_Curseur_Man_X.Value;
                graphique_1.ChartAreas[2].CursorX.Position =
                                (int)nUD_Curseur_Man_X.Value;
                //Positionnement Y
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.Position =
                                                (int)nUD_Curseur_Man_Y.Value;
                //Retrait de la visibilite du panneau de coordonnees
                pnl_curseur.Visible = false;
                //Couleur rouge pour visibilite
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.LineColor
                                                                    = Color.Red;
                graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorX.LineColor
                                                                    = Color.Red;
                graphique_1.ChartAreas[2].CursorX.LineColor
                                                    = Color.Red;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.LineColor
                                                                    = Color.Red;

                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.IsUserEnabled = false;
                graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorY.IsUserEnabled = false;
            }
        }
        //Evenement declenche au moment ou la souris passe au dessus du graphique
        private void graphique_1_MouseHover(object sender, EventArgs e)
        {
            //Si l'affichage du curseur graphique est en mode automatique ET 
            //que le curseur est active ET que le curseur n'est pas deja affiche
            if (checkBox_Curseur_Auto.Checked
                    && btn_Curseur.Text == "O"
                        && pnl_curseur.Visible == false
                            && capture_ecran == false)
            {
                //Activation du curseur et positionnement automatique selon
                //l'emplacement de la souris
                activer_curseur();

                //for (int i = k.POMPE_1; i)
            }
        }
        //Evenement declenche suite a un clique de souris
        private void graphique_1_MouseUp(object sender, MouseEventArgs e)
        {
            //Si le mode d'affichage est en semi-automatique
            if (checkBox_Curseur_Semi_Auto.Checked)
            {
                //Le panneau de coordonnees est deplace a la souris
                pnl_curseur.Location =
                    new Point
                        (e.X + graphique_1.Location.X + (k.BORDURE_PNL * k.PROPORTION_HUIT),
                            e.Y - (k.BORDURE_PNL * k.PROPORTION_HUIT));
                //Affichage de la coordonnees en X du curseur
                lbl_curseur_X.Text =
                        graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                CursorX.Position.ToString();
                //Affichage de la coordonnees en Y du curseur
                lbl_curseur_Y.Text =
                        graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].
                                CursorY.Position.ToString();

                graphique_1.ChartAreas[k.GRAPH_SECONDAIRE].CursorX.Position =
                        graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.Position;
                graphique_1.ChartAreas[2].CursorX.Position =
        graphique_1.ChartAreas[k.GRAPH_PRINCIPAL].CursorX.Position;
            }
        }



        private void btn_Graph_Principal_Click(object sender, EventArgs e)
        {
            if (inputs.action.graphique_secondaire)
            {
                graphique_1.ChartAreas[0].Position.Height = 82;
                graphique_1.ChartAreas[1].Visible = false;
                graphique_1.ChartAreas[2].Visible = false;

                btn_Graph_Principal.Text = "Activer le graphique secondaire";

                inputs.action.graphique_secondaire = false;
            }
            else
            {
                graphique_1.ChartAreas[0].Position.Height = 60;
                graphique_1.ChartAreas[1].Visible = true;
                graphique_1.ChartAreas[2].Visible = true;

                btn_Graph_Principal.Text = "Afficher seulement le graphique principale";

                inputs.action.graphique_secondaire = true;
            }
        }

        private void Page_Principale_Move(object sender, EventArgs e)
        {
            timer_move.Enabled = false;

            timer_move.Enabled = true;
        }

        private void timer_move_Tick(object sender, EventArgs e)
        {

            timer_move.Enabled = false;

            for (int l = 0; l < nb_ecrans; l++)
            {
                if (this.Location.X > ecrans[l].Bounds.X - (k.BORDURE_PNL * 4) && this.Location.X < ecrans[l].Bounds.Right - (k.BORDURE_PNL * 4))
                {
                    this.Width = ecrans[l].Bounds.Width;

                    this.Height = ecrans[l].Bounds.Height;

                    this.SetDesktopLocation(ecrans[l].Bounds.X, 0);

                    int bordure = (ecrans[l].Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height);

                    dimensions[k.LARGEUR] = ecrans[l].WorkingArea.Width;
                    dimensions[k.HAUTEUR] = ecrans[l].WorkingArea.Height - bordure;

                    //Ajustement necessaire a l'interface
                    ajustement_ecran();
                    ajustement_pnl_materiel_type();
                    ajustement_boutons();
                    ajustment_pnl_equiv();
                    ajustement_pnl_edition();
                    ajustement_graphique_normalsize();
                    ajustement_images();
                    ajustement_lbl();
                }
            }
            WindowState = FormWindowState.Maximized;
        }


    }
}

