using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Curve__4._0
{
    class Materiel
    {
        public const int nb = 47;

        /*Ce module contient seulement la liste des materiel contenue dans les
         comboBox de materiel*/
        public static string[] obtention_materiel()
        {
            string[] liste_pipes = new string[] {
                        //10
                        "ABS - Acrylonite Butadiene Styrene / 130",
                        "Aluminium / 140",
                        "Asbesto Cement / 140",
                        "Asphalt Lining / 135",
                        "Boreline / 200",
                        "Brass / 135",
                        "Brick sewer / 95",
                        "Cast-Iron New unlined / 130",
                        "Cast-Iron 10 years old / 110",
                        "Cast-Iron 20 years old / 94",
                        //20
                        "Cast-Iron 30 years old / 82",
                        "Cast-Iron 40 years old / 74",
                        "Cast-Iron, asphalt coasted / 100",
                        "Cast-Iron, cement lined / 140",
                        "Cast-Iron, Bituminous lined / 140",
                        "Cast-Iron, sea coated / 120",
                        "Cast-Iron, Wought plain / 100",
                        "Cement lining / 135",
                        "Concrete / 120",
                        "Concretes lined, steel form / 140",
                        //30
                        "Concrete lined, wooden form / 120",
                        "Concrete old / 105",
                        "Copper / 135",
                        "Corrugated metal / 60",
                        "Ductile Iron Pipe / 140",
                        "Ductile Iron, ciment lined / 120",
                        "Fiber / 140",
                        "Fiber glass pipie - FRP / 150",
                        "Galvanized iron / 120",
                        "Glass / 130",
                        //40
                        "Lead / 135",
                        "Metal pipes - Very to extremely smooth / 135",
                        "Plastic / 140",
                        "Polyethylene,PE,PEH / 150",
                        "Poyvinyl chloride, PVC, CPVC / 150",
                        "Smooth pipes / 138",
                        "Steel new unlined / 145",
                        "Steel Corrugated / 60",
                        "Steel, welded and seamless / 100",
                        "Steel, interior riveted, no projecting rivets / 110",
                        //47
                        "Steel, projecting girth and horizontal rivets / 100",
                        "Steel, vitriedm spiral-riveted / 100",
                        "Tin / 130",
                        "Vitrified Clay / 110",
                        "Wrought iron, plain / 100",
                        "Wooden or Masonry pipe - Smooth / 120",
                        "Wood Stave / 115" };
            return liste_pipes;
        }
    }
}
