using System;

namespace TestApp.Services
{
    public partial class VarComm
	{
        [Serializable]
		public class UserLevelStruct
		{
			public byte UserLevel_BackupForm;

			public byte UserLevel_CheckParamForm;

			public byte UserLevel_CycleCounterForm;

			public byte UserLevel_EditSteps;

			public byte UserLevel_PrgOptParameterForm;

			public byte UserLevel_ProgramOverviewForm;

			public byte UserLevel_SpindleConstantsForm;

			public byte UserLevel_StatisticsLastResForm;

			public byte UserLevel_StepOverviewForm;

			public byte UserLevel_SystemConstantsForm;

			public byte UserLevel_TestIOForm;

			public byte UserLevel_TestMotorSensorForm;

			public byte UserLevel_VisualisationParamForm;

			public byte UserLevel_FourStepEditForm;

			public byte UserLevel_Maintenance;

			public byte UserLevel_HandStartForm;

			public byte UserLevel_BrowserForm;

			public byte UserLevel_Reserve1;

			public byte UserLevel_Reserve2;

			public byte UserLevel_Reserve3;

			public byte UserLevel_Reserve4;

			public byte UserLevel_Reserve5;

			public UserLevelStruct()
			{
				Initialize();
			}

			public void Initialize()
			{
				UserLevel_BackupForm = 3;
				UserLevel_CheckParamForm = 2;
				UserLevel_CycleCounterForm = 3;
				UserLevel_EditSteps = 2;
				UserLevel_PrgOptParameterForm = 3;
				UserLevel_ProgramOverviewForm = 2;
				UserLevel_SpindleConstantsForm = 3;
				UserLevel_StatisticsLastResForm = 1;
				UserLevel_StepOverviewForm = 2;
				UserLevel_SystemConstantsForm = 3;
				UserLevel_TestIOForm = 4;
				UserLevel_TestMotorSensorForm = 2;
				UserLevel_VisualisationParamForm = 3;
				UserLevel_FourStepEditForm = 3;
				UserLevel_Maintenance = 3;
				UserLevel_HandStartForm = 3;
				UserLevel_BrowserForm = 1;
				UserLevel_Reserve1 = 0;
				UserLevel_Reserve2 = 0;
				UserLevel_Reserve3 = 0;
				UserLevel_Reserve4 = 0;
				UserLevel_Reserve5 = 0;
			}
		}
	}
}