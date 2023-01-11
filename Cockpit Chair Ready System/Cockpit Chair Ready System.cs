//Cockpit Chair Ready System
//Developer: Auxilium Aerospace || Aeztus
const string version = "0.1";
/*Changelog (YY/MM/DD | 21/08/21):
- Initial version
*/

//user configuration
const int rotorDetectUpperLimit = 88;
const int rotorDetectLowerLimit = 2;
	
const string sittingTag = "Sit (Main)";
const string exitTag = "Exit (Main)";

const string rotorPrefix = "Rotor - ";
const string tbPrefix = "Timer Block - ";
const string controlPrefix = "Control Seat - ";
const string controlSuffix = " - Main";


void Main(string seatTitle)
{
	var tempTitle = seatTitle;
	string blockTitle = tempTitle.ToString();
	Echo($"Passed down arg: {blockTitle}");
		
	string rotorName = rotorPrefix + blockTitle;
	string timerBlockSitName = tbPrefix + blockTitle + " - " + sittingTag;
	string timerBlockExitName = tbPrefix + blockTitle + " - " + exitTag;
	string controlChairName = controlPrefix + blockTitle + controlSuffix;
	
	var rotorHandled = GridTerminalSystem.GetBlockWithName(rotorName) as IMyMotorStator;
	if(rotorHandled == null)
	{
		Echo($"{rotorName} not found");
	}
	var tbSitHandled = GridTerminalSystem.GetBlockWithName(timerBlockSitName) as IMyTimerBlock;
	if(tbSitHandled == null)
	{
		Echo($"{timerBlockSitName} not found");
	}
	var tbExitHandled = GridTerminalSystem.GetBlockWithName(timerBlockExitName) as IMyTimerBlock;
	if(rotorHandled == null)
	{
		Echo($"{timerBlockExitName} not found");
	}
	var chairHandled = GridTerminalSystem.GetBlockWithName(controlChairName) as IMyShipController;
	if(chairHandled == null)
	{
		Echo($"{controlChairName} not found");
	}
	
	int rotorAngle = 180/ (22 / 7) * ((int) rotorHandled.Angle);
	Echo($"Angle: {rotorAngle}");

	if(chairHandled.IsUnderControl && rotorAngle > rotorDetectLowerLimit)
	{
		tbSitHandled.ApplyAction("TriggerNow");
	}
	else if(!chairHandled.IsUnderControl && rotorAngle < rotorDetectUpperLimit)
	{
		tbExitHandled.ApplyAction("TriggerNow");
	}
}