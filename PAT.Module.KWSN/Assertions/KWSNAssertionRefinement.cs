﻿using PAT.Common.Classes.Assertion;
using PAT.Common.Classes.LTS;
using PAT.Common.Classes.ModuleInterface;
using PAT.Common.Classes.SemanticModels.LTS.Assertion;
using PAT.KWSN.LTS;

namespace PAT.KWSN.Assertions
{
    public class KWSNAssertionRefinement : AssertionRefinement
    {
        private DefinitionRef ImplementationProcess;
        private DefinitionRef SpecificationProcess;

        public KWSNAssertionRefinement(DefinitionRef processDef, DefinitionRef target)
            : base()
        {
            ImplementationProcess = processDef;
            SpecificationProcess = target;
        }

        public override void Initialize(SpecificationBase spec)
        {
            //initialize the ModelCheckingOptions
            base.Initialize(spec);

            Assertion.Initialize(this, ImplementationProcess, SpecificationProcess, spec);
        }

        public override string StartingProcess
        {
            get { return ImplementationProcess.ToString(); }
        }

        public override string SpecProcess
        {
            get { return SpecificationProcess.ToString(); }
        }

        //todo: override ToString method if your assertion uses different syntax as PAT
        //public override string ToString()
        //{
        //		return "";
        //}        
    }
}