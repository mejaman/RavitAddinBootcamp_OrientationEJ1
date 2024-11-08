using Autodesk.Revit.DB;
using System.ComponentModel;

namespace RavitAddinBootcamp_OrientationEJ1
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge01_old : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            Transaction t = new Transaction(doc);
            t.Start("I'm doing something in Revit");


            // Your Module 01 Challenge code goes here
            // Delete the TaskDialog below and add your code
            TaskDialog.Show("Module 01 Challenge", "Coming Soon!- Lets see");

            // declare my variables

            int myVariable250 = 250;
            int myStartingEleV = 0;
            int myFloorHeight = 15;
            double levelRemainder = 1;
            int fizzCount = 0;
            int buzzCount = 0;
            int fizzBuzzCount = 0;




            for (int i = 1; i <= 250; i++)
            {

                //create a level with i
                // create a transaction to lock the model


                // create a floor level 
                Level newLevel = Level.Create(doc, i);
                newLevel.Name = "My new level" + i;   //******Need to add level value (thinking to convert i value to string
            
                // check divisible

                levelRemainder = i % 3;
                if (levelRemainder == 0)
                {
                    //creat floor plan and name "FIZZ"
                    fizzCount++;
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                        }
                    }

                    // create a floor plan view
                    ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                    newFloorPlan.Name = "FIZZ - " + fizzCount;

                }


                levelRemainder = i % 5;
                if (levelRemainder == 0)
                {
                    //creat ceiling plan and name "BUZZ"

                    // create a floor plan view
                    buzzCount++;
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType ceilingPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                        {
                            ceilingPlanVFT = curVFT;
                        }
                    }

                    // create a ceiling plan view
                    ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
                    newCeilingPlan.Name = "BUZZ - " + buzzCount;

                }


                
                if ((i%3) == 0 && (i%5)==0)
                {
                    //creat sheet and name "FIZZBUZZ"
                    fizzBuzzCount++;
                    // get a title block type
                    FilteredElementCollector collector2 = new FilteredElementCollector(doc);
                    collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);
                    collector2.WhereElementIsElementType();

                    // create a sheet
                    ViewSheet newSheet = ViewSheet.Create(doc, collector2.FirstElementId());
                    newSheet.Name = "FIZZBUZZ_"+fizzBuzzCount;
                    newSheet.SheetNumber = "A10"+fizzBuzzCount;

                   

                }

                
            }

            //string myAllCount = fizzCount.ToString() + ", " + buzzCount.ToString() + ", " + fizzBuzzCount.ToString();

            TaskDialog.Show("Module 01 Challenge", "Fizzcount="+ fizzCount.ToString()+ ", Buzzcount="+ buzzCount.ToString() +", FizzBuzzcount =" + fizzBuzzCount.ToString());


            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge01";
            string buttonTitle = "Module\r01";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module01,
                Properties.Resources.Module01,
                "Module 01 Challenge");

            return myButtonData.Data;
        }
    }

}
