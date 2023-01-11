//Landing Gear Automation
//Developer: Auxilium Aerospace || Aeztus
const string version = "1.0";

//user configuration
const string nacelleHingeTag = "ldg gr"; //sets which hinge/rotor blocks are to be detected
const string nacellePistonTag = "ldg gr"; //sets which pistons are to be detected
const double pistonExtDet = 0.5; //sets how far the pistons have to protrude to be detected as 'extended'

//setup
List<IMyShipController> referenceList = new List<IMyShipController>();
List<IMyPistonBase> nacellePiston = new List<IMyPistonBase>();
List<IMyMotorAdvancedStator> nacelleHinge = new List<IMyMotorAdvancedStator>(); //hinges are identified as advanced rotors, idk why ask keen

void Main(string argument)
{
	GrabBlocks(); //populate the list
	int retractionCtPiston = 0;
	int retractionCtHinge = 0;
    try
    {
			Echo($"Auxilium Aerospace || Aeztus\nLanding Gear Automation\nVersion = v{version}\n");
            //Gets reference block that is under control 
			int activeNacellePiston = nacellePiston.Count(x => x.IsWorking);
            Echo($"Detected pistons: {activeNacellePiston}");
			
			int activeNacelleHinge = nacelleHinge.Count(x => x.IsWorking);
            Echo($"Detected hinges: {activeNacelleHinge}\n");
			
			bool needToRetract = false;
			
			//checks if each block are 'extended', todo: combine both in zip foreach function
			foreach(var block in nacellePiston)
			{
				var pistonInfo = block.DetailedInfo;
				//splits the string into an array by separating by the ':' character
				string[] pistInfoArr = (pistonInfo.Split(':'));

				// splits the resulting 0.0m into an array with single index of "0.0" by again splitting by character "m"
				string[] pistonDist = (pistInfoArr[1].Split('m'));
				double pistonDistD = double.Parse(pistonDist[0]);
				
				if(pistonDistD > pistonExtDet)
				{
					block.ApplyAction("Retract");
					needToRetract = true;
					retractionCtPiston++;
				}
			}
			if(needToRetract)
			{
				foreach(var block in nacelleHinge)
				{
					block.ApplyAction("Reverse");
					retractionCtHinge++;
				}
			}	
	}
	Echo($"Retracted Pistons: {retractionCtPiston}\nRetracted Hinges: {retractionCtHinge}\n----");
}
bool GrabBlocks()
{
	//checks the whole grid for blocks of the type, with the constraint that it contains the tag
	GridTerminalSystem.GetBlocksOfType(nacellePiston, x => x.CustomName.ToLower().Contains(nacellePistonTag.ToLower()));
	if (nacellePiston.Count == 0)
    {
        Echo(">> Error: No nacelle pistons found");
        return false;
    }
    GridTerminalSystem.GetBlocksOfType(nacelleHinge, x => x.CustomName.ToLower().Contains(nacelleHingeTag.ToLower()));
	if (nacelleHinge.Count == 0)
    {
        Echo(">> Error: No nacelle hinges found");
        return false;
    }
    return true;
}
			
			