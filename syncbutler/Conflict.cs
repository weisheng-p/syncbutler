﻿using System;
using System.Collections.Generic;
using SyncButler.Exceptions;

namespace SyncButler
{
    /// <summary>
    /// Represents a conflict detected in the sync. The state can be
    /// CopyToLeft, DeleteLeft, Merge, CopyToRight, DeleteRight, Unknown.
    /// </summary>
    public class Conflict
    {
        protected internal ISyncable left;
        protected internal ISyncable right;
        protected Action autoResolveAction;
        protected Action suggestedAction;

        //private enum StatusOptions {Resolved, Unresolved, Resolving}

        /// <summary>
        /// Possible actions for conflict resolution
        /// </summary>
        public enum Action { CopyToLeft, DeleteLeft, Merge, CopyToRight, DeleteRight, Unknown };

        /// <summary>
        /// Constructor used to instantiate a Conflict object.
        /// </summary>
        /// <param name="left">The left ISyncable</param>
        /// <param name="right">The other (right) ISyncable</param>
        /// <param name="autoResolveAction">The Action to be performed, or Unknown if this conflict cannot be automatically resolved.</param>
        public Conflict(ISyncable left, ISyncable right, Action autoResolveAction)
        {
            this.left = left;
            this.right = right;
            this.autoResolveAction = autoResolveAction;
            this.suggestedAction = Action.Unknown;
        }

        /// <summary>
        /// Constructor used to instantiate a Conflict object.
        /// </summary>
        /// <param name="left">The left ISyncable</param>
        /// <param name="right">The other (right) ISyncable</param>
        /// <param name="autoResolveAction">The Action to be performed, or Unknown if this conflict cannot be automatically resolved.</param>
        /// <param name="suggestedAction">If the conflict cannot be automatically resolved, then this should contain a suggested action.</param>
        public Conflict(ISyncable left, ISyncable right, Action autoResolveAction, Action suggestedAction) : this(left, right, autoResolveAction)
        {
            this.suggestedAction = suggestedAction;
        }

        /// <summary>
        /// Gets/Sets the suggested action for this conflict.
        /// This property will contain a suggested action when the system fails to automatically resolve a conflict.
        /// It acts as a hint for the UI to prompt the user with a default action.
        /// </summary>
        public Action SuggestedAction
        {
            get
            {
                return this.suggestedAction;
            }
            set
            {
                this.suggestedAction = value;
            }
        }

        /// <summary>
        /// Gets/Sets the action to be performed when this conflict can be resolved automatically.
        /// It will return unknown if the system could not find a solution automatically.
        /// </summary>
        public Action AutoResolveAction
        {
            get
            {
                return autoResolveAction;
            }
            set
            {
                this.autoResolveAction = value;
            }
        }

        /// <summary>
        /// Attempts to rsolve a conflict based on th recommended action.
        /// </summary>
        /// <returns></returns>
        public Error Resolve()
        {
            if (this.autoResolveAction == Action.Unknown) throw new InvalidActionException();
            return this.Resolve(this.autoResolveAction);
        }

        /// <summary>
        /// Attempts to resolve a conflict based on a specified user action.
        /// </summary>
        /// <returns>true if the conflict was successfully resolved, false otherwise.</returns>
        /// <exception cref="ArgumentException">This exception is generated when an invalid user action is passed into the method.</exception>
        public Error Resolve(Action user)
        {
            Error ret;

            switch (user) {
                case Action.CopyToLeft : 
                    {
                        ret = right.CopyTo(left);
                        if (ret == Error.NoError) right.UpdateStoredChecksum();
                        break;
                    }
                case Action.DeleteLeft : 
                    {
                        ret = left.Delete();
                        if (ret == Error.NoError) left.RemoveStoredChecksum();
                        break;
                    }
                case Action.Merge:
                    {
                        ret = left.Merge(right);
                        if (ret == Error.NoError) left.UpdateStoredChecksum();
                        break;
                    }
                case Action.CopyToRight:
                    {
                        ret = left.CopyTo(right);
                        if (ret == Error.NoError) left.UpdateStoredChecksum();
                        break;
                    }
                case Action.DeleteRight:
                    {
                        ret = right.Delete();
                        if (ret == Error.NoError) right.RemoveStoredChecksum();
                        break;
                    }
                default:
                    {
                        throw new System.ArgumentException("Invalid User Action");
                        //return false;
                    }

            }

            return ret;
        }

        public override String ToString()
        {
            return left.EntityPath() + "\n" + this.autoResolveAction + "";
        }
    }
}