using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class SyncCode
    {
        //The constant C_NEW_POS.
        public const int C_NEW_POS = 11;

        //The constant C_NEW_POS_RESPONSE.
        public const int C_NEW_POS_RESPONSE = 12;

        //The constant C_NEW_ORDER.
        public const int C_NEW_ORDER = 101;

        //The constant C_CANCEL_ORDER.
        public const int C_CANCEL_ORDER = 102;

        //The constant C_HOLD_ORDER.
        public const int C_HOLD_ORDER = 103;

        //The constant C_RELEASE_ORDER.
        public const int C_RELEASE_ORDER = 104;

        //The constant C_PAY_ORDER.
        public const int C_PAY_ORDER = 105;

        //The constant C_CUSTOMER_UPDATE_ORDER.
        public const int C_CUSTOMER_UPDATE_ORDER = 106;

        //The constant C_REFUND_ORDER.
        public const int C_REFUND_ORDER = 107;

        //The constant C_SPLIT_ORDER.
        public const int C_SPLIT_ORDER = 108;

        //The constant C_ORDERS_REQUEST.
        public const int C_ORDERS_REQUEST = 109;

        //The constant C_MENU_REQUEST.
        public const int C_MENU_REQUEST = 110;

        //The constant C_MENU.
        public const int C_MENU = 111;

        //The constant C_MODIFIER_REQUEST.
        public const int C_MODIFIER_REQUEST = 112;

        //The constant C_MODIFIER.
        public const int C_MODIFIER = 113;

        //The constant C_INGREDIENT_REQUEST.
        public const int C_INGREDIENT_REQUEST = 114;

        //The constant C_INGREDIENT.
        public const int C_INGREDIENT = 115;

        //The constant C_KDS_LOGIN.
        public const int C_KDS_LOGIN = 201;

        //The constant C_BATCH_CODE.
        public const int C_BATCH_CODE = 202;

        //The constant C_NEGATIVE_CODE.
        public const int C_NEGATIVE_CODE = 203;

        //The constant C_BATCH_CODE_TO_OTHER_POS.
        public const int C_BATCH_CODE_TO_OTHER_POS = 204;

        //The constant C_UPDATE_ITEM_STATUS.
        public const int C_UPDATE_ITEM_STATUS = 301;

        //The constant C_UPDATE_ITEM_DISCOUNT.
        public const int C_UPDATE_ITEM_DISCOUNT = 302;

        //The constant C_UPDATE_ITEM_COMP_SPOIL.
        public const int C_UPDATE_ITEM_COMP_SPOIL = 303;

        //The constant C_UPDATE_ITEM_HOLD_RELEASE.
        public const int C_UPDATE_ITEM_HOLD_RELEASE = 304;

        //The constant C_TABLE_TRANSFER.
        public const int C_TABLE_TRANSFER = 401;

        //The constant C_TABLE_MERGE.
        public const int C_TABLE_MERGE = 402;

        //The constant C_TABLE_RELEASE.
        public const int C_TABLE_RELEASE = 403;

        //The constant C_TABLE_RESERVATION.
        public const int C_TABLE_RESERVATION = 404;

        //The constant C_TABLE_RESERVATION_REMOVE.
        public const int C_TABLE_RESERVATION_REMOVE = 405;

        //TODO
        //The constant C_TABLE_EMPLOYEE_TRANSFER.
        public const int C_TABLE_EMPLOYEE_TRANSFER = 406;

        //The constant C_STAFF_ATTENDANCE.
        public const int C_STAFF_ATTENDANCE = 501;

        //The constant C_STAFF_ATTENDANCE_REQUEST.
        public const int C_STAFF_ATTENDANCE_REQUEST = 502;

        //The constant C_ADD_DEVICE.
        public const int C_ADD_DEVICE = 601;

        //The constant C_REMOVE_DEVICE.
        public const int C_REMOVE_DEVICE = 602;

        //The constant C_ADD_DEVICE_RESPONSE.
        public const int C_ADD_DEVICE_RESPONSE = 603;

        //The constant C_ADD_DEVICE_REQUEST.
        public const int C_ADD_DEVICE_REQUEST = 604;

        //The constant C_SEND_ACKNOWLEDGEMENT_FOR_MESSAGE
        public const int C_SEND_MESSAGE_ACKNOWLEDGEMENT = 605;

        //The constant C_ADD_CATEGORY.
        public const int C_ADD_CATEGORY = 701;

        //The constant C_UPDATE_CATEGORY.
        public const int C_UPDATE_CATEGORY = 702;

        //The constant C_ADD_PRODUCT.
        public const int C_ADD_PRODUCT = 703;

        //The constant C_UPDATE_PRODUCT.
        public const int C_UPDATE_PRODUCT = 704;

        /**
        * The constant C_UPDATE_ITEM.
        */
        public const int C_UPDATE_ITEM = 705;

        /**
       * The constant C_UPDATE_ORDER_RUNNING.
       */
        public const int C_UPDATE_ORDER_RUNNING = 706;

        /**
         * The constant C_ADD_STAFF.
         */
        public const int C_ADD_STAFF = 801;

        /**
         * The constant C_ADD_STOCK.
         */
        public const int C_ADD_STOCK = 802;

        /**
         * The constant C_ADD_VENDOR.
         */
        public const int C_ADD_VENDOR = 803;

        /**
         * The constant C_ADD_INVENTORY.
         */
        public const int C_ADD_INVENTORY = 804;

        /**
         * The constant C_ADD_STOCK_ITEM.
         */
        public const int C_ADD_STOCK_ITEM = 805;

        /**
         * The constant C_REMOVE_STOCK.
         */
        public const int C_REMOVE_STOCK = 806;

        /**
         * The constant C_REMOVE_STOCK_ITEM.
         */
        public const int C_REMOVE_STOCK_ITEM = 807;

        /**
         * The constant C_REMOVE_INVENTORY.
         */
        public const int C_REMOVE_INVENTORY = 808;

        /**
         * The constant C_KITCHEN_ROUTING.
         */
        public const int C_KITCHEN_ROUTING = 901;

        /**
         * The constant C_KITCHEN_ROUTING_REQUEST.
         */
        public const int C_KITCHEN_ROUTING_REQUEST = 902;

        /**
         * The constant C_PRINTER_ROUTING.
         */
        public const int C_PRINTER_ROUTING = 1001;

        /**
         * The constant C_PRINTER_ROUTING_REQUEST.
         */
        public const int C_PRINTER_ROUTING_REQUEST = 1002;

        /**
         * The constant C_TABLE_ROUTING.
         */
        public const int C_TABLE_ROUTING = 1008;

        /**
         * The constant C_TABLE_ROUTING_REQUEST.
         */
        public const int C_TABLE_ROUTING_REQUEST = 1009;

        /**
         * The constant C_RECIPE_REQUEST.
         */
        public const int C_RECIPE_REQUEST = 1003;

        /**
         * The constant C_RECIPE.
         */
        public const int C_RECIPE = 1004;

        /**
         * The constant C_BUMPED_ITEM.
         */
        public const int C_BUMPED_ITEM = 1005;

        /**
         * The constant C_RE_SYNC.
         */
        public const int C_RE_SYNC = 1006;

        /**
         * The constant C_RE_SEND_ORDER.
         */
        public const int C_RE_SEND_ORDER = 1007;
    }

}
