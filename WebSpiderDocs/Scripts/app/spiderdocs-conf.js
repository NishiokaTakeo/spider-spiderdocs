
(function (global, factory) {

    factory(global, {});


})(window !== 'undefined' ? window : this, function (global, _$) {

    var jQuery = _$;

    var SpiderDocsConf = {

        User: {
            id: 1,
            loginID: "Administrator",
            password: "Slayer6zed"
        },

        GetServerURL: function () {
            return `${window.location.protocol}//${window.location.host}/${window.location.pathname.split('/')[1]}/`;
        }
        , ID: function (v) {
            if (!v) return this.User.id;

            this.User.id = v;
            return this;
            //return 'administrator';
        }

        , LoginID: function (v) {
            if (!v) return this.User.loginID;

            this.User.loginID = v;
            return this;
            //return 'administrator';
        }

        , LoginPassword: function (v) {
            if (!v) return this.User.password;
            this.User.password = v;
            return this;
            //return 'Welcome1';
        }
        , DisableAuth: function (id) {
            id = id || 0;

            if (id > 0) {
                this.User.id = id;
            }

            return true;
        }
    }

    global.SpiderDocsConf = SpiderDocsConf;
});
