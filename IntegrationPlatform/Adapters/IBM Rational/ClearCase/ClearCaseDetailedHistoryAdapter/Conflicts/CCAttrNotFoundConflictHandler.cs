﻿// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.TeamFoundation.Migration.Toolkit;

namespace Microsoft.TeamFoundation.Migration.ClearCaseDetailedHistoryAdapter
{
    class CCAttrTypeNotFoundConflictHandler : IConflictHandler
    {
        #region IConflictHandler Members

        public bool CanResolve(MigrationConflict conflict, ConflictResolutionRule rule)
        {
            return ConflictTypeHandled.ScopeInterpreter.IsInScope(conflict.ScopeHint, rule.ApplicabilityScope);
        }

        public ConflictResolutionResult Resolve(IServiceContainer serviceContainer, MigrationConflict conflict, ConflictResolutionRule rule, out List<MigrationAction> actions)
        {
            // Add a resolution action to attempt to add the name?  Or should we just try that first, and give AttrType cannot be created?
            actions = null;
            if (rule.ActionRefNameGuid.Equals(new CCAttrTypeNotFoundSkipAction().ReferenceName))
            {
                return new ConflictResolutionResult(true, ConflictResolutionType.SkipConflictedChangeAction);
            }
            else if (rule.ActionRefNameGuid.Equals(new CCAttrTypeNotFoundRetryAction().ReferenceName))
            {
                return new ConflictResolutionResult(true, ConflictResolutionType.Retry);
            }
            else
            {
                return new ConflictResolutionResult(false, ConflictResolutionType.UnknownResolutionAction);
            }
        }

        public ConflictType ConflictTypeHandled
        {
            get;
            set;
        }

        #endregion

    }
}
