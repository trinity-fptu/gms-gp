

namespace Application.Exceptions
{
    public static class ExceptionMessage
    {
        #region General message
        public const string NOT_FOUND = "Item not found";
        public const string NOT_ALLOW = "You are not allow to perform this action";
        public const string ENTITY_RETRIEVED_ERROR = "Error when retrieve items";
        public const string ENTITY_CREATE_ERROR = "Error when create new item";
        public const string ENTITY_UPDATE_ERROR = "Error when update this item";
        public const string ENTITY_DELETE_ERROR = "Error when delete this item";
        public const string INVALID_INFORMATION = "Invalid information";
        public const string REQUEST_NOT_APPROVED = "Request have not been approved";
        public const string REQUEST_APPROVED = "Request have been approved, cannot modify";
        public const string NOT_ALL_DELIVERED = "Delivery stage is not all delivered";
        public const string INVALID_APPROVAL_STATUS = "Invalid approval status";
        public const string REPORT_NOT_PENDING = "Report is not pending, cannot modify or delete";
        public const string UPDATE_RESOLVE_STATUS = "Error when update resolve status";
        public const string USER_UNAUTHORIZE= "User unauthorize";
        public const string FILE_EMPTY = "Empty input file";
        public const string FILE_INVALID = "Invalid input file";
        public const string FILE_INVALID_INPUT = "Invalid input information";
        public const string TASK_ASSIGNED = "Task is already assigned";

        #endregion

        #region Delivery stage message
        public const string DELIVERYSTAGE_NOTEXIST = "Delivery stage not exist";
        public const string DELIVERYSTAGE_NOTAVAILABLE = "Delivery stage status is not available to create request";
        #endregion

        #region request
        public const string REQUESTSTATUS_NOTAVAILABLE = "Request status is not available for update/delete";
        public const string REQUEST_NOTEXIST = "Request not exist";
        public const string REQUEST_NOTAPPROVE = "Request has not been approved ";
        public const string REQUESTFORM_ALREADYCREATED = "Request has already created a form ";
        #endregion

        #region User message
        public const string USER_LOGIN_ERROR = "Invalid email or password";
        public const string USER_NOT_FOUND = "User is not found or inactive";
        public const string USER_NOT_ALLOWED = "User is not allowed to perform this action";
        #endregion

        #region warehouseformmaterial
        public const string PROCESSED_WAREHOUSEFORMMATERIAL = "Warehouse form material is already processed";
        #endregion
    }
}
