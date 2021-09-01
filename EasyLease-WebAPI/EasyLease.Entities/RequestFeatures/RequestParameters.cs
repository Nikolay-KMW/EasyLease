using System;
using EasyLease.Entities.AppSettingsModels;

namespace EasyLease.Entities.RequestFeatures {
    public class RequestParameters {
        private readonly int maxPageSize;
        private int _pageSize;
        private int _pageNumber = 1;

        public RequestParameters() {
            maxPageSize = GeneralSettings.MaxPageSize;
            _pageSize = GeneralSettings.PageSize;
        }

        // In cases like this: ?limit=10&offset=20
        //----------------------------------------------
        public int Limit {
            get {
                return PageSize;
            }
            set {
                PageSize = value;
            }
        }

        public int Offset {
            get {
                return PageOffset;
            }
            set {
                PageNumber = (value / _pageSize) + 1;
            }
        }

        // In cases like this: ?pageNumber=3&pageSize=10
        //----------------------------------------------
        public int PageSize {
            get {
                return _pageSize;
            }
            set {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public int PageNumber {
            get {
                return _pageNumber;
            }
            set {
                _pageNumber = value <= 0 ? 1 : value;
            }
        }

        public int PageOffset {
            get {
                return (_pageNumber - 1) * _pageSize;
            }
        }
    }
}