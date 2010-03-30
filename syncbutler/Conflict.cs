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
        protected bool leftOverwriteRight;
        protected bool rightOverwriteLeft;

        //private enum StatusOptions {Resolved, Unresolved, Resolving}

        /// <summary>
        /// Possible actions for conflict resolution
        /// </summary>
        public enum Action { CopyToLeft, DeleteLeft, Merge, CopyToRight, DeleteRight, Ignore, Unknown };

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
            leftOverwriteRight = (this.autoResolveAction == Conflict.Action.CopyToRight || this.autoResolveAction == Conflict.Action.DeleteRight);
            rightOverwriteLeft = !leftOverwriteRight;
        }

        public string OffendingPath
        {
            get
            {
                if (left != null)
                {
                    if (left.EntityPath().Length != 0)
                    {
                        return left.EntityPath();
                    }
                }
                else if (right != null)
                {
                    if (right.EntityPath().Length != 0)
                    {
                        return right.EntityPath();
                    }
                }
                throw new NullReferenceException("Non Existance EntityPath");
            }
        }

        public bool LeftOverwriteRight
        {
            get
            {
                return leftOverwriteRight;
            }
            set
            {
                leftOverwriteRight = value;
                rightOverwriteLeft = !value;

            }
        }

        public bool RightOverwriteLeft
        {
            get
            {
                return rightOverwriteLeft;
            }
            set
            {
                rightOverwriteLeft = value;
                leftOverwriteRight = !value;
            }
        }

        public bool IgnoreConflict
        {
            get
            {
                return left.Ignored();
            }
            set
            {
                left.Ignored(value);
            }
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

        public Partnership GetPartnership()
        {
            return left.GetParentPartnership();
        }

        /// <summary>
        /// Sets the status monitor on the syncables
        /// </summary>
        /// <param name="monitor"></param>
        public void SetStatusMonitor(SyncableStatusMonitor monitor)
        {
            left.SetStatusMonitor(monitor);
            right.SetStatusMonitor(monitor);
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
        /// Attempts to resolve a conflict based on the recommended action.
        /// </summary>
        /// <returns></returns>
        public Resolved Resolve()
        {
            
            if (leftOverwriteRight && rightOverwriteLeft)
                throw new NotSupportedException("Merging not supported currently");

            if (leftOverwriteRight)
            {
                if (!left.Exists())
                {
                    Resolve(Action.DeleteRight);
                    return new Resolved(left,right,Resolved.ActionDone.DeleteRight);
                }
                else
                {
                    Resolve(Action.CopyToRight);
                    return new Resolved(left,right,Resolved.ActionDone.CopyFromLeft);
                }
            }
            else if (rightOverwriteLeft)
            {
                if (!right.Exists())
                {
                    Resolve(Action.DeleteLeft);
                    return new Resolved(left, right, Resolved.ActionDone.DeleteLeft);
                }
                else
                {
                    Resolve(Action.CopyToLeft);
                    return new Resolved(left, right, Resolved.ActionDone.CopyFromRight);
                }
            }

            throw new InvalidActionException();
        }

        /// <summary>
        /// Attempts to resolve a conflict based on a specified user action.
        /// </summary>
        /// <returns>true if the conflict was successfully resolved, false otherwise.</returns>
        /// <exception cref="ArgumentException">This exception is generated when an invalid user action is passed into the method.</exception>
        public void Resolve(Action user)
        {
            switch (user) {
                case Action.CopyToLeft : 
                    right.CopyTo(left);
                    //right.UpdateStoredChecksum();
                    break;
                case Action.DeleteLeft : 
                    left.Delete(true);
                    //left.RemoveStoredChecksum();
                    break;
                case Action.Merge:
                    left.Merge(right);
                    //left.UpdateStoredChecksum();
                    break;
                case Action.CopyToRight:
                    left.CopyTo(right);
                    //left.UpdateStoredChecksum();
                    break;
                case Action.DeleteRight:
                    right.Delete(true);
                    //right.RemoveStoredChecksum();
                    break;
                case Action.Ignore:
                    break;
                default:
                    throw new System.ArgumentException("Invalid User Action");

            }
        }

        public override String ToString()
        {
            return left.EntityPath() + "\n" + this.autoResolveAction + "";
        }
    }
}