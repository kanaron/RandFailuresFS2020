﻿using SimConModels;
using SimConModels.SimVar;
using System;

namespace RandFailuresFS2020_WPF.Models
{
    public class FailListModel : BaseModel
    {
        private string? _failuresText;

        public string? FailuresText
        {
            get { return _failuresText; }
            set
            {
                _failuresText = value;
                NotifyPropertyChanged();
            }
        }

        public FailListModel()
        {
            FailuresText = "";
        }

        public void ShowFailures()
        {
            FailuresText = "";
            if (SimVarLists.GetSimVarLists().GetFailuresList() != null)
                foreach (var sv in SimVarLists.GetSimVarLists().GetFailuresList())
                {
                    string altTime;
                    if (sv.WhenFail == WHEN_FAIL.ALT)
                    {
                        altTime = "at " + sv.FailureAlt.ToString() + " ft";
                    }
                    else if (sv.WhenFail == WHEN_FAIL.TIME)
                    {
                        altTime = "after " + sv.FailureTime.ToString() + " seconds";
                    }
                    else if (sv.WhenFail == WHEN_FAIL.SPEED)
                    {
                        altTime = "at " + sv.FailureSpeed.ToString() + " kts";
                    }
                    else
                    {
                        altTime = "";
                    }

                    FailuresText += "Name: " + sv.SimVarName + " when will fail: " + sv.WhenFail + " " + altTime + Environment.NewLine;
                }
        }
    }
}
