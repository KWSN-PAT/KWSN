﻿using System.Collections.Generic;
using PAT.Common.Classes.Expressions;
using PAT.Common.Classes.Expressions.ExpressionClass;
using PAT.Common.Classes.Ultility;
using PAT.Common.Classes.ModuleInterface;
using PAT.KWSN.Assertions;

namespace PAT.KWSN.LTS{
    public sealed class ConditionalChoiceAtomic : Process
    {
        public Process FirstProcess;
        public Process SecondProcess;
        public Expression ConditionalExpression;

        public ConditionalChoiceAtomic(Process firstProcess, Process secondProcess, Expression conditionExpression)
        {
            FirstProcess = firstProcess;
            SecondProcess = secondProcess;
            ConditionalExpression = conditionExpression;

            ProcessID = DataStore.DataManager.InitializeProcessID(FirstProcess.ProcessID + Constants.CONDITIONAL_CHOICE +
                                                                      conditionExpression.ExpressionID + Constants.CONDITIONAL_CHOICE +
                                                                      SecondProcess.ProcessID);
        }

        public override void MoveOneStep(Valuation GlobalEnv, List<Configuration> list)
        {
            ExpressionValue v = EvaluatorDenotational.Evaluate(ConditionalExpression, GlobalEnv);

            if ((v as BoolConstant).Value)
            {
                FirstProcess.MoveOneStep(GlobalEnv, list);
            }
            else
            {
                SecondProcess.MoveOneStep(GlobalEnv, list);    
            }            
        }


        public override void SyncOutput(Valuation GlobalEnv, List<ConfigurationWithChannelData> list)
        {
            System.Diagnostics.Debug.Assert(list.Count == 0);

            ExpressionValue v = EvaluatorDenotational.Evaluate(ConditionalExpression, GlobalEnv);

            if ((v as BoolConstant).Value)
            {
                FirstProcess.SyncOutput(GlobalEnv, list);
            }
            else
            {
                SecondProcess.SyncOutput(GlobalEnv, list);    
            }
            
        }

        public override void SyncInput(ConfigurationWithChannelData eStep, List<Configuration> list)
        {
            ExpressionValue v = EvaluatorDenotational.Evaluate(ConditionalExpression, eStep.GlobalEnv);

            if ((v as BoolConstant).Value)
            {
                FirstProcess.SyncInput(eStep, list);
            }
            else
            {
                SecondProcess.SyncInput(eStep, list);    
            }            
        }

        public override string ToString()
        {
            return "ifa " + ConditionalExpression + " {" + FirstProcess.ToString() + "} else {" + SecondProcess.ToString() + "}";
        }

        public override HashSet<string> GetAlphabets(Dictionary<string, string> visitedDefinitionRefs)
        {
            HashSet<string> list = SecondProcess.GetAlphabets(visitedDefinitionRefs);
            list.UnionWith(FirstProcess.GetAlphabets(visitedDefinitionRefs));
            return list;
        }

        public override List<string> GetGlobalVariables()
        {
            List<string> Variables = SecondProcess.GetGlobalVariables();
            Common.Classes.Ultility.Ultility.AddList(Variables, FirstProcess.GetGlobalVariables());
            Common.Classes.Ultility.Ultility.AddList(Variables, ConditionalExpression.GetVars());
            return Variables;
        }

        public override List<string> GetChannels()
        {
            List<string> channels = SecondProcess.GetChannels();
            Common.Classes.Ultility.Ultility.AddList(channels, FirstProcess.GetChannels());

            return channels;
        }

        public override Process ClearConstant(Dictionary<string, Expression> constMapping)
        {
            Expression newCon = ConditionalExpression.ClearConstant(constMapping);

            Process newFirstProc = FirstProcess.ClearConstant(constMapping);
            Process newSecondProc = SecondProcess.ClearConstant(constMapping);

            return new ConditionalChoiceAtomic(newFirstProc, newSecondProc, newCon);
        }

        public override bool MustBeAbstracted()
        {
            return FirstProcess.MustBeAbstracted() || SecondProcess.MustBeAbstracted();
        }

        public override bool IsBDDEncodable()
        {
            return FirstProcess.IsBDDEncodable() && SecondProcess.IsBDDEncodable();
        }
    }
}