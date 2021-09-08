$(document).ready(function () {
    //debugger;

    window.onerror = function (msg, url, line) {
        ClientErrorLog(url, msg, line);
        return true;
    }

    /**
     * Load All Global Data from service. it improves overall performance.
     */
    searchfilter_setup();
    getStaticTables();

    gNewReportDialog = $("#frmNewReportDialog").dialog({
        autoOpen: false,
        closeOnEscape: false,
        resizable: false,
        show: "slide",
        hide: "slide",
        modal: true,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            $(".ui-widget-overlay").attr('style', 'background-color: #000; opacity:.5; z-index:0;');
        }
    });

    gReportFieldsDialog = $("#frmReportFieldsDialog").dialog({
        autoOpen: false,
        closeOnEscape: false,
        resizable: false,
        show: "slide",
        hide: "slide",
        modal: true,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            $(".ui-widget-overlay").attr('style', 'background-color: #000; opacity:.5; z-index:0;');
        }
    });

    $("#btnCreateNewReport").click(function () {
        //debugger;
        $http("../api/ReportBuilder/AddEmptyReport/", { userId: Global.get('user').user_id } )
            .then(function (reportId) {
                if (reportId > 0) {
                    InitialiseNewReportDialog(function () {
                        $("#hdn_ReportManagement_Id").val(reportId);
                        load_reporting_report_fields_grid({ reportId: 0 });
                        createFilterGrid();
                        gNewReportDialog.dialog("option", "title", "");
                        gNewReportDialog.dialog("option", "width", "70%");
                        gNewReportDialog.dialog("option", "position", ['top']);
                        gNewReportDialog.dialog("open");
                    });
                }
            });

        return false;
    });

    $("#ReportManagement_btnAdd").click(function () {
        var reportName = $("#ReportManagement_Report_Name").val();

        if (reportName === "") {
            swal("Mandatory Field Required", "Please enter report name", "error");
            return false;
        }
        else {
            $http("../api/ReportBuilder/GetReportFieldsByReportId", { reportId: $("#hdn_ReportManagement_Id").val() })
                .then(function (rows) {
                    Global.update('GetReportFieldsByReportId', rows);
                    if (rows.length == 0) {
                        swal("Mandatory Field Required", "Please select a field", "error");
                        return false;
                    }

                    Spin.start();

                    updateReportFilter(function (ret) {
                        if (ret) {
                            $http("../api/ReportBuilder/UpdateReport", { id: $("#hdn_ReportManagement_Id").val(), reportName: reportName, active: $("#ReportManagement_Active").prop("checked") })
                                .then(function (ret) {
                                    if (ret) {
                                        Spin.stop();
                                        generateReport($("#hdn_ReportManagement_Id").val());
                                        gNewReportDialog.dialog("close");
                                        // Refresh
                                        $('#Menu_Search_Refresh').trigger('click');
                                    }
                                });
                        }
                    }, true);
                });
        }
    });

    $("#ReportManagement_btnCancel").click(function () {
        updateReportFilter(function (ret) {
            if (ret) {
                gNewReportDialog.dialog("close");
            }
        });
        return false;
    });

    $("#ReportManagement_btnAddFields").click(function () {

        InitialiseReportFieldsDialog(function () {
            gReportFieldsDialog.dialog("option", "title", "");
            gReportFieldsDialog.dialog("option", "width", "40%");
            gReportFieldsDialog.dialog("option", "position", ['center']);
            gReportFieldsDialog.dialog("open");
            gNewReportDialog.dialog("close");
        });

        return false;
    });

    $("#ReportFields_btnCancel").click(function () {
        gReportFieldsDialog.dialog("close");
        gNewReportDialog.dialog("open");
        return false;
    });

    $("#Category_Id").change(function () {
        if ($("#Category_Id option:selected").val() > 0) {
            $('#Field_Input_Filter').val("");

            Q.fcall(function () {
                return load_reporting_fields_grid({ categoryId: $("#Category_Id option:selected").val() });
            })
                .then(function () {
                    $http("../api/ReportBuilder/GetReportFieldsByReportId", { reportId: $("#hdn_ReportManagement_Id").val() })
                        .then(function (result) {
                            Global.update('GetReportFieldsByReportId', result);

                            reportFieldsList = []
                            result.forEach(function (item) {
                                $(`#selectReportFields_checkbox-${item.Field_Id}`).prop('checked', true);
                                reportFieldsList.push(item.Field_Id);
                            });

                            Global.update('report_fields', reportFieldsList);
                        });
                });
        }
        return;
    });

    $("#ReportFields_btnAdd").click(function () {
        if (Global.get('report_fields').length > 0) {
            Q.fcall(function () {
                return addReportFields($("#hdn_ReportManagement_Id").val(), Global.get('report_fields'));
            })
                .then(function () {
                    return load_reporting_report_fields_grid({ reportId: $("#hdn_ReportManagement_Id").val() });
                })
                .then(function () {
                    return createFilterGrid();
                });
        }

        gReportFieldsDialog.dialog("close");
        gNewReportDialog.dialog("open");

        return false;
    });

    $("#ReportManagement_btnAddFilters").click(function () {
        updateReportFilter(function (ret) {
            if (ret) {
                $http("../api/ReportBuilder/AddEmptyReportFilter", { reportId: $("#hdn_ReportManagement_Id").val() })
                    .then(function (result) {
                        if (result) {
                            createFilterGrid();
                        }
                    });
            }
        });

        return false;
    });

    $("#ReportFilters_btnCancel").click(function () {
        gNewReportDialog.dialog("open");
        return false;
    });

    $("#ReportFilters_btnAddFilter").click(function () {
        $http("../api/ReportBuilder/AddEmptyReportFilter", { reportId: $("#hdn_ReportManagement_Id").val() })
            .then(function (result) {
                if (result) {
                    createFilterGrid();
                }
            });
        return false;
    });

    $("#ReportFilters_btnAdd").click(function () {
        updateReportFilter(function (ret) {
            console.log(ret);
            if (ret) {
                gNewReportDialog.dialog("open");
            }
        });

        return false;
    });

    // Q.allSettled([
    //     $http("../WebServices/utilService.asmx/GetCurrentUserInfo")
    // ])
    Q.fcall( function(){
        return [{value:  {
            user_id: 1 ,
            user_name: "Administrator" ,
            user_login: 'administrator' ,
            user_admin: 1 ,
            user_password: '' ,
            group_id: 0 ,
            email: 'web@spiderdevelopments.com.au' ,
            field_officer: false ,
            finance: false ,
            email_signature_location_id: 0
            }}];
    })
    .spread(function (user) {

        Global.register('last', []);
        //TODO: Global.update('user', user.value);
        Global.update('user', user.value);
        Global.register('grid', $("#MenuSearch_gridviewReportSearchResult"));
        Global.register('grid_report_fields', $("#ReportManagement_gridviewReportFieldsList"));
        Global.register('grid_fields', $("#ReportFields_gridviewReportFieldsList"));
        Global.register('grid_filters', $("#ReportFilters_gridviewReportFiltersList"));
    })
    .then(function () {
        //$http("../api/ReportBuilder/UpdateDataSourceFields");
        return load_contacts_grid({ reportName: '' }, 'INIT');
    })
    .then(function () {
        return load_reporting_report_fields_grid({ reportId: 0 }, 'INIT');
    })
    .then(function () {
        return load_reporting_fields_grid({ categoryId: 0 }, 'INIT');
    })
    .then(function () {
        select_suggestion("", "");
    });
});


jQuery(window).resize(function () {
    adapt_pagesize_changer('grid');
});

var searchpid;

function searchfilter_setup() {
    $('input:radio[id=MenuSearch_ReportByName]').prop('checked', true);

    $('#Menu_Search_Refresh').click(function (e, args) {
        args = args || {};

        //return
        args.promise = select_suggestion("", "");

        return false;
    });

    $("#MenuSearch_ReportName").click(function () {
        var radioOpt = $("input[name=MenuSearch]:checked").val();
        if (radioOpt !== "ReportName") {
            $("#MenuSearch_ReportByName").prop("checked", true);
        }
        return false;
    });

    $('#MenuSearch_ReportName').keyup(function (e) {
        // Return if you didn't type number, alphabet, and del key
        if (!Fn.IsTyped4NumOrChar(e) && !Fn.IsTyped4Del(e)) return;

        // Return if value length is less than 3 characters
        if (this.value.length < 3) return;

        clearInterval(searchpid);
        searchpid = setTimeout(function () {
            console.log(searchpid);
            var radioOpt = $("input[name=MenuSearch]:checked").val();

            if (radioOpt === "ReportName") {
                select_suggestion("", "");
            }
        }, 300);
    });

    // Change event
    $(".SearchField").change(function () {
        $('#Menu_Search_Refresh').trigger('click');
        return;
    });
}

function select_suggestion(checked, suggestion) {
    Spin.start();

    var criteria = {};

    criteria.reportName = $("#MenuSearch_ReportName").val().replace(/'/g, "#quote#");

    return load_contacts_grid(criteria);
}

function change_pagesize() {
    var
        args = { promise: {} },

        new_page_size = parseInt($('#pagerctr').val() || 999999999),

        filter = Global.get('grid').jsGrid('getFilter');

    Spin.start();

    Global.get('grid').jsGrid('option', "pageSize", new_page_size);

    // Apply
    $('#Menu_Search_Refresh').trigger('click', args);

}

function load_jsgrid_contacts_config() {
    return {
        editing: false,
        onRefreshed: function (args) {

            //Global.get('grid').jsGrid("option", "filtering", false);
            Global.get('grid').find(".jsgrid-grid-body").height(480);

            Global.get('grid').find(".jsgrid-header-cell").addClass("menuHeader");

            if (!Global.get('user').user_admin) {
                Global.get('grid').find('.jsgrid-control-field').addClass('hidden');
            }

        },

        controller: {
            loadData: function (criteria) {

                Global.get('grid').jsGrid('option', "pageIndex", 1);

                if (Fn.isNil(criteria)) criteria = new clsSearchCriteria();

                return is_filtering(criteria) ?

                    // Programaticaly search
                    Global.get('last').filter(function (student) {

                        return Object.keys(criteria).map(function (col) {

                            // True if criteria.NAME isn't typed or typed and that value includes student
                            return String(student[col]).has(criteria[col]) || extra_has(student, col, criteria[col]);
                        })

                            // AND filter that means all coloums are true.
                            .filter(function (hit) { return !hit; }).length === 0;
                    })
                    :
                    $http("../api/ReportBuilder/GetReports", criteria)
                        .then(function (rows) {

                            Global.update('last', rows);
                            var strRecordsFound = "";
                            if (rows.length === 0) {
                                strRecordsFound = "No Reports have been found.";
                            }
                            else if (rows.length === 1) {
                                strRecordsFound = "1 Report has been found.";
                            }
                            else {
                                strRecordsFound = rows.length + " Reports have been found.";
                            }
                            $("#lblReportsFound").html(strRecordsFound);

                            return rows;
                        });
            }
        },
        rowDoubleClick: function (args) {
            editReport(args.item.Id, args.item.Report_Name, args.item.Active);
        }
    };
}

function load_jsgrid_reporting_report_fields_config() {
    return {
        editing: false,
        onRefreshed: function (args) {
            //Global.get('grid').jsGrid("option", "filtering", false);
            Global.get('grid_report_fields').find(".jsgrid-grid-body").height(250);
            Global.get('grid_report_fields').find(".jsgrid-header-cell").addClass("menuHeader");

            if (!Global.get('user').user_admin) {
                Global.get('grid_report_fields').find('.jsgrid-control-field').addClass('hidden');
            }
        },
        controller: {
            loadData: function (criteria) {
                return $http("../api/ReportBuilder/GetReportFieldsByReportId", criteria)
                    .then(function (rows) {
                        Global.update('GetReportFieldsByReportId', rows);

                        return rows;
                    });
            }
        }
    };
}

function filterFields() {
    load_reporting_fields_grid({ categoryId: $("#Category_Id option:selected").val() });
}

function load_jsgrid_reporting_fields_config() {
    return {
        editing: false,
        onRefreshed: function (args) {
            //Global.get('grid').jsGrid("option", "filtering", false);
            Global.get('grid_fields').find(".jsgrid-grid-body").height(250);
            Global.get('grid_fields').find(".jsgrid-header-cell").addClass("menuHeader");

            if (!Global.get('user').user_admin) {
                Global.get('grid_fields').find('.jsgrid-control-field').addClass('hidden');
            }
        },
        controller: {
            loadData: function (criteria) {
                return $http("../api/ReportBuilder/GetFieldsByCategory", criteria)
                    .then(function (rows) {

                        var newArray = rows.filter(function (el) {
                            return $('#Field_Input_Filter').val() != "" ? el.Display_Name.toUpperCase().includes($('#Field_Input_Filter').val().toUpperCase()) : true;
                        });

                        return newArray;
                    });
            }
        }
    };
}

function extra_has(student, col, val) {

    var has =
    {
        'AddressState': function () {
            return String(student[col]).has(GetStateId(val.trim(), false));
        },

        'MobilePhone': function () {
            return String(student[col].trima()).has(val.trima());
        }
    };


    return !_.isFunction(has[col]) ? false : has[col]();
}

function is_filtering(criteria) {
    var
        request_coloums = Object.keys(criteria),

        view_coloums = contact_grid_field_config().map(function (r) { return r.name; }).filter(function (name) { return !Fn.isNil(name); });

    return _.intersection(request_coloums, view_coloums).length === request_coloums.length;
}

function load_contacts_grid(criteria, type) {
    Spin.start();

    return (type === 'INIT' ?
        Q.all([
            Q.fcall(function () { })
        ])

            .spread(function () {

                var conf = load_jsgrid_contacts_config();

                conf.fields = contact_grid_field_config();

                return init_jsgrid(conf, 'grid');

            })
            .then(function () {
                Global.get('grid').jsGrid("option", "filtering", false);
                Global.get('grid').find(".jsgrid-grid-body").height(480);

                Global.get('grid').find(".jsgrid-header-cell").addClass("menuHeader");

                $('#pagerctr-wrap').insertAfter('.jsgrid-pager-container');

                adapt_pagesize_changer('grid');

                return;
            })
        :
        Q.fcall(function () {
            return Global.get('grid').jsGrid("loadData", criteria);
        })
    )
        .then(function () {

            filter = Global.get('grid').jsGrid('getFilter');

            if ($('.jsgrid-filter-row ').is(':visible')) {
                Global.get('grid').jsGrid("loadData", filter);
            }
        })
        .catch(function (e) {
        })
        .fin(function () {
            Spin.stop();
        });

}

function load_reporting_report_fields_grid(criteria, type) {
    return (type === 'INIT' ?
        Q.all([
            Q.fcall(function () { })
        ])
            .spread(function () {
                var conf = load_jsgrid_reporting_report_fields_config();
                conf.fields = reporting_report_fields_grid_config();

                return init_jsgrid(conf, 'grid_report_fields');
            })
            .then(function () {
                Global.get('grid_report_fields').jsGrid("option", "filtering", false);
                Global.get('grid_report_fields').find(".jsgrid-grid-body").height(250);
                Global.get('grid_report_fields').find(".jsgrid-header-cell").addClass("menuHeader");

                return;
            })
        :
        Q.fcall(function () {
            return Global.get('grid_report_fields').jsGrid("loadData", criteria);
        }))
        .then(function () {
            filter = Global.get('grid_report_fields').jsGrid('getFilter');

            if ($('.jsgrid-filter-row ').is(':visible')) {
                Global.get('grid_report_fields').jsGrid("loadData", filter);
            }
        })
        .catch(function (e) {
        })
        .fin(function () {
            $(`#img_down-${$('.img_down_class').length}`).hide();
        });
}

function load_reporting_fields_grid(criteria, type) {
    return (type === 'INIT' ?
        Q.all([
            Q.fcall(function () { })
        ])
            .spread(function () {
                var conf = load_jsgrid_reporting_fields_config();
                conf.fields = reporting_fields_grid_config();

                return init_jsgrid(conf, 'grid_fields');
            })
            .then(function () {
                Global.get('grid_fields').jsGrid("option", "filtering", false);
                Global.get('grid_fields').find(".jsgrid-grid-body").height(250);
                Global.get('grid_fields').find(".jsgrid-header-cell").addClass("menuHeader");

                return;
            })
        :
        Q.fcall(function () {
            return Global.get('grid_fields').jsGrid("loadData", criteria);
        }))
        .then(function () {
            filter = Global.get('grid_fields').jsGrid('getFilter');

            if ($('.jsgrid-filter-row ').is(':visible')) {
                Global.get('grid_fields').jsGrid("loadData", filter);
            }
        })
        .catch(function (e) {
        })
        .fin(function () {
        });
}

function adapt_pagesize_changer(grid) {
    $('.jsgrid-pager-container').css('max-width', Global.get(grid).width() - $('#pagerctr-wrap').width());
}

function contact_grid_field_config() {
    var conf = [
        {
            name: "Id",
            title: "Report Id",
            type: "text",
            editing: false,
            width: "5%",
            visible: false
        },
        {
            name: "Report_Name",
            title: "Report Name",
            type: "text",
            editing: false,
            width: "80%",
            visible: true
        },
        {
            name: "Created_Date",
            title: "Created Date",
            type: "date",
            editing: false,
            width: "200px"
        },
        {
            name: "Generate",
            type: "text",
            width: "90px",
            align: "center",
            sorting: false,
            editing: false,
            filtering: false,
            css: "edit",
            itemTemplate: function (value, item) {
                return $("<img>").attr("src", "../content/images/report2.png").css({ height: 32, width: 32 }).on("click", function () {
                    generateReport(item.Id);
                });
            }
        },
        {
            name: "Edit",
            type: "text",
            width: "60px",
            align: "center",
            sorting: false,
            editing: false,
            filtering: false,
            css: "edit",
            itemTemplate: function (value, item) {
                return $("<img>").attr("src", "../content/images/edit.png").css({ height: 32, width: 32 }).on("click", function () {
                    editReport(item.Id, item.Report_Name, item.Active);
                });
            }
        },
        {
            name: "Delete",
            align: "center",
            width: "60px",
            sorting: false,
            editing: false,
            itemTemplate: function (val, item) {
                return $("<img>").attr("src", "../content/images/remove.png").css({ height: 32, width: 32 }).on("click", function () {
                    return swal({
                        title: "",
                        text: "Are you sure to delete the report?",
                        icon: "info",
                        buttons: ["No", "Yes"]
                    })
                        .then(function (yes) { if (yes) { deleteReport(item.Id); } return false; });
                });
            }
        }
    ];

    return conf;
}

function reporting_report_fields_grid_config() {
    var conf = [
        {
            name: "Id",
            title: "Field Id",
            type: "text",
            editing: false,
            width: "0px",
            visible: false
        },
        {
            name: "",
            align: "center",
            width: "1px",
            sorting: false,
            editing: false,
            itemTemplate: function (val, item) {
                var html = "<table style='margin:0'>";
                html += "<tr><td>";
                html += `<img src="../content/images/up.png" style="height: 20px; width: 20px;" ${item.Sort == 1 ? 'hidden' : ''} onclick='moveReportFieldUp(${item.Id}, -1, ${item.Report_Id});'>`;
                html += "</td></tr>";
                html += "<tr><td>";
                html += `<img src="../content/images/down.png" id='img_down-${item.Sort}' class='img_down_class' style="height: 20px; width: 20px;" onclick='moveReportFieldUp(${item.Id}, 1, ${item.Report_Id});'>`;
                html += "</td></tr>";
                html += "</table>";

                return html;
            }
        },
        {
            name: "Field.Display_Name",
            title: "Fields",
            type: "text",
            //align: "center",
            editing: false,
            width: "18%",
            visible: true,
            sorting: false
        },
        {
            name: "Display_Name",
            title: "Title",
            type: "text",
            width: "18%",
            //align: "center",
            sorting: false,
            editing: false,
            filtering: false,
            itemTemplate: function (value, item) {
                var $result = jsGrid.fields.control.prototype.itemTemplate.apply(this, arguments);
                var $customButton;

                var htmlSelect = `<input type='text' class="form-control" id="field_Title-${item.Id}" value='${item.Display_Name.trim()}' onfocusout="updateReportFieldTitle(${item.Id}, this.value, ${item.Report_Id})"/>`;
                $customButton = $(htmlSelect);

                return $result.add($customButton);
            }
        },
        {
            name: "",
            align: "center",
            width: "5px",
            sorting: false,
            editing: false,
            itemTemplate: function (val, item) {
                return $("<img>").attr("src", "../content/images/remove2.png").css({ height: 32, width: 32 }).on("click", function () {
                    deleteReportField(item.Id, item.Report_Id);
                });
            }
        }
    ];

    return conf;
}

function reporting_fields_grid_config() {
    var conf = [
        {
            name: "Id",
            title: "Field Id",
            type: "text",
            editing: false,
            width: "5%",
            visible: false
        },
        {
            name: "Display_Name",
            title: "Fields",
            type: "text",
            editing: false,
            width: "80%",
            visible: true
        },
        {
            name: "",
            type: "text",
            width: "10%",
            align: "center",
            sorting: false,
            editing: false,
            filtering: false,
            itemTemplate: function (value, item) {
                var checked = '';
                if (Global.get('report_fields').length > 0) {
                    if (Global.get('report_fields').indexOf(item.Id) > -1) {
                        checked = 'checked';
                    }
                }
                var $result = jsGrid.fields.control.prototype.itemTemplate.apply(this, arguments);
                var htmlSelect = `<input type='checkbox' class='reportFieldsCheckboxClass' id='selectReportFields_checkbox-${item.Id}' ${checked}/>`;
                var $customButton = $(htmlSelect)
                    .on("click", function (e) {
                        if (this.checked) {
                            Global.get('report_fields').push(parseInt(this.id.split('-')[1]));
                        }
                        else {
                            Global.get('report_fields').splice(Global.get('report_fields').indexOf(parseInt(this.id.split('-')[1])), 1);
                        }

                        e.stopPropagation();
                    });

                return $result.add($customButton);
            }
        }
    ];

    return conf;
}

function _load_grid_conf() {

    return {
        width: "100%",
        height: "100%",
        editing: true,
        sorting: true,
        paging: true,
        autoload: false,
        pageSize: parseInt($('#pagerctr').val() || 999999999),
        filtering: true,
        deleteItem: function (item) {
            //return swal({
            //    title: "",
            //    text: "Are you sure?",
            //    icon: "info",
            //    buttons: ["No", "Yes"],
            //})
            //    .then(function (yes) {
            //        if (yes == true) {
            //            if (item.UniqueStudentId > 0) {
            //                return; //TODO DELETE FUNCTION
            //                $http("../WebServices/searchService.asmx/DeleteStudent", { StudentId: item.UniqueStudentId })
            //                    .then(function (result) {
            //                        $('#Menu_Search_Refresh').trigger('click');
            //                        return false;
            //                    });
            //            }
            //        }
            //        return false;
            //    });
        },
    }
}

function init_jsgrid(config, grid) {
    // Conbine common config and a config belong to ctrlId
    config = $.extend({}, _load_grid_conf(), config);

    return Q.fcall(function () {
        return Global.get(grid).jsGrid(config).addClass("WordWrap");
    });
}

function createFieldsCategoryDropDown() {
    return $http("../api/ReportBuilder/GetFieldsCategory")
        .then(function (json) {
            var $selectCategory = $(".ddlFieldsCategory");
            $selectCategory.empty();
            $selectCategory
                .find('option')
                .remove()
                .end()
                ;
            var $option = $("<option/>").attr("value", 0).text("Select a category");
            $selectCategory.append($option);

            for (var j = 0; j < json.length; j++) {
                $option = $("<option/>").attr("value", json[j]["Id"]).text(json[j]["Display_Name"]);
                $selectCategory.append($option);
            }
        });
}

function createFieldOrderDropDown() {
    var $selectCategory = $(".ddlFieldOrder");
    $selectCategory.empty();
    $selectCategory
        .find('option')
        .remove()
        .end();

    for (var j = 1; j <= $selectCategory.length; j++) {
        var $option = $("<option/>").attr("value", j).text(j);
        $selectCategory.append($option);
    }
}

function populateReportFilter() {
    $(".filterIdClass").map(function () {
        var Id = this.id.split('-')[1];

        $(`#Filter_Field-${Id}`).val($(this).attr('field_id'));
        $(`#Filter_Comparator-${Id}`).val($(this).attr('comparator_id'));
        $(`#Filter_Value_1-${Id}`).val($(this).attr('value_1'));
        $(`#Filter_Order-${Id}`).val($(this).attr('Filter_Order'));
    });
}

function InitialiseNewReportDialog(callback) {
    $("#hdn_ReportManagement_Id").val("");
    $("#ReportManagement_Report_Name").val("");
    $("#ReportManagement_Active").prop("checked", true);
    //load_reporting_report_fields_grid({ reportId: 0 });
    callback();
}

function InitialiseReportFieldsDialog(callback) {
    createFieldsCategoryDropDown();
    $("#Category_Id").val(0);
    load_reporting_fields_grid({ categoryId: 0 }).then(function() {
        callback();
    });

}

function getStaticTables() {
    Q.fcall(function () {
        //$http("../api/ReportBuilder/GetComparators")
        $http("../api/reportbuilder/GetComparators")
            .then(function (rows) {
                Global.update('reporting.Comparators', rows);
            });
    })
        .then(function () {
            $http("../api/ReportBuilder/GetReportDropdownFields")
                .then(function (rows) {
                    Global.update('reporting.Dropdown_Fields', rows);
                });
        });
}

function createFilterGrid() {
    var reportId = $("#hdn_ReportManagement_Id").val();

    $http("../api/ReportBuilder/GetReportFiltersByReportId", { reportId: reportId })
        .then(function (rows) {

            var json = Global.get('GetReportFieldsByReportId');
            var jsonComparator = Global.get('reporting.Comparators');
            var jsonDropdownFields = Global.get('reporting.Dropdown_Fields');

            var grid = $('#ReportFilters_gridviewReportFiltersListCustom');

            var html = "<table style='width:100%;border:1px solid #e9e9e9;'>";

            var count = 0;
            var htmlElement;
            rows.forEach(function (item) {
                count++;

                if (count > 1) {
                    htmlElement = `<select class="form-control ddlFilterConditional" id="Filter_Conditional-${item.Id}" style='width:15%; margin:auto;' onchange='SaveAndReloadFilterGrid()'>`;
                    htmlElement += `<option value='AND' ${item.Conditional === "AND" ? 'selected' : ''}>AND</option>`;
                    htmlElement += `<option value='OR' ${item.Conditional === "OR" ? 'selected' : ''}>OR</option>`;
                    htmlElement += "</select>";

                    html += "<tr>";
                    html += `<td colspan='10' style='text-align:center;background-color:#d9edf7'>${htmlElement}</td><td></td>`;
                    html += "</tr>";
                }

                html += "<tr>";

                var clickEvent = `onclick='FilterGroupClick(this, ${rows.length});'`;
                htmlElement = `<span class='tooltiptext'>Group</span>`;
                htmlElement += `<input type='button' class='form-control filterIdClass' id="Filter_Id-${item.Id}" Field_Id='${item.Field_Id}' Comparator_Id='${item.Comparator_Id}' `;
                htmlElement += `Value_1='${item.Value_1}' Filter_Order='${item.Filter_Order}' Filter_Group='${item.Filter_Group}' value='${item.Filter_Group}' ${clickEvent}/>`;

                html += `<td class='tooltipCustom' style='width:5%;'>${htmlElement}</td>`;

                //FIELD
                htmlElement = `<select class="form-control ddlFilterFields" id="Filter_Field-${item.Id}" onchange='FilterFieldOnChange(this)'>`;
                htmlElement += "<option value='0'>Select a Field</option>";

                for (var j = 0; j < json.length; j++) {
                    htmlElement += `<option ${json[j]["Field"]["Id"] == item.Field_Id ? 'selected' : ''} value='${json[j]["Field"]["Id"]}' field_type_id='${json[j]["Field"]["Field_Type_Id"]}'>${json[j]["Field"]["Display_Name"].trim()}</option>`;
                }
                htmlElement += "</select>";

                html += `<td style='width:30%;'>${htmlElement}</td>`;

                //COMPARATOR
                htmlElement = `<select class="form-control ddlFilterComparator" id="Filter_Comparator-${item.Id}" onchange='FilterFieldOnChange(this)' >`;

                if (item.Field_Id == 0) {
                    htmlElement += "<option value='0'>Select a Comparator</option>";
                }

                var between = false;

                for (var j = 0; j < jsonComparator.length; j++) {
                    if (jsonComparator[j]["Field_Types"].includes(item.Field.Field_Type_Id)) {
                        htmlElement += `<option ${jsonComparator[j]["Id"] == item.Comparator_Id ? 'selected' : ''} value='${jsonComparator[j]["Id"]}' >${jsonComparator[j]["Display_Value"].trim()}</option>`;
                        if (item.Comparator_Id == 7 && jsonComparator[j]["Id"] == 7) { between = true; }
                    }
                }
                htmlElement += "</select>";

                html += `<td style='width:20%;'>${htmlElement}</td>`;

                //VALUE
                htmlElement = "<table style='width:100%'><tr><td>";

                var aux = jsonDropdownFields.find(function (item2) { return item2.Field_Id == item.Field_Id });

                if (aux) {
                    htmlElement += `<select class="form-control ddlDropdownFields" id="Filter_Value_1-${item.Id}" ' >`;

                    for (var j = 0; j < aux.List.length; j++) {
                        htmlElement += `<option ${aux.List[j]["Item1"] == item.Value_1 ? 'selected' : ''} value='${aux.List[j]["Item1"]}' '>${aux.List[j]["Item2"].trim()}</option>`;
                    }
                    htmlElement += "</select>";
                }
                else {
                    var inputType = `type='${item.Field.Field_Type_Id == 3 ? "date" : item.Field.Field_Type_Id == 2 ? "number" : item.Field.Field_Type_Id == 4 ? "button" : "text"}'`;
                    var inputValue = `value='${item.Value_1}'`;

                    if (item.Field.Field_Type_Id == 4) {
                        inputValue = `value=${item.Value_1 == "" ? 'TRUE' : item.Value_1} onclick='this.value= (this.value == "TRUE" ? "FALSE" : "TRUE");'`;
                    }

                    htmlElement += `<input class="form-control" id="Filter_Value_1-${item.Id}" Field_Type_Id='${item.Field.Field_Type_Id}' ${inputType} ${inputValue} />`;

                    if (between) {//between
                        inputValue = `value='${item.Value_2}'`;

                        htmlElement += "<td>AND</td>";
                        htmlElement += "<td>";
                        htmlElement += `<input class="form-control" id="Filter_Value_2-${item.Id}" Field_Type_Id='${item.Field.Field_Type_Id}' ${inputType} ${inputValue} />`;
                        htmlElement += "</td>";
                    }
                }

                htmlElement += "</td></tr></table>";

                html += `<td style='width:45%;text-align:center;'>${htmlElement}</td>`;

                //DELETE BUTTON
                htmlElement = `<img src="../content/images/remove2.png" style="height: 32px; width: 32px;" onclick='deleteReportFilter(${item.Id})'>`;

                html += `<td>${htmlElement}</td>`;

                html += "</tr>";
            });

            html += "</table>";

            grid.html(html);

            ShowFilterPreview(reportId);
        });
}

function ShowFilterPreview(reportId) {
    if (!reportId) {
        reportId = $("#hdn_ReportManagement_Id").val();
    }

    updateReportFilter(function (ret) {
        if (ret) {
            $http("../api/ReportBuilder/GetFilterPreview", { reportId: reportId })
                .then(function (result) {
                    console.log(result);
                    $('#ReportPreviewDiv').html(`<h4>${result}</h4>`);
                });
        }
    });
}

function FilterGroupClick(field, filterLen) {
    if (field.value >= filterLen - 1) {
        field.value = 1;
    }
    else {
        field.value = parseInt(field.value) + 1;
    }
    SaveAndReloadFilterGrid();
}

function FilterFieldOnChange(t) {
    $(`#Filter_Value_1-${t.id.split('-')[1]}`).val("");
    $(`#Filter_Value_2-${t.id.split('-')[1]}`).val("");
    SaveAndReloadFilterGrid()
}

function SaveAndReloadFilterGrid() {
    updateReportFilter(function (ret) {
        if (ret) {
            createFilterGrid();
        }
    });
}

function editReport(reportId, reportName, active) {
    InitialiseNewReportDialog(function () {
        $("#hdn_ReportManagement_Id").val(reportId);
        $("#ReportManagement_Report_Name").val(reportName);
        $("#ReportManagement_Active").prop("checked", active);
        Q.fcall(function () {
            return load_reporting_report_fields_grid({ reportId: reportId });
        })
            .then(function () {
                $http("../api/ReportBuilder/GetReportFieldsByReportId", { reportId: reportId })
                    .then(function (json) {
                        Global.update('GetReportFieldsByReportId', json);
                    })
            })
            .then(function () {
                return createFilterGrid();
            })
            .then(function () {
                gNewReportDialog.dialog("option", "title", "");
                gNewReportDialog.dialog("option", "width", "70%");
                gNewReportDialog.dialog("option", "position", ['top']);
                gNewReportDialog.dialog("open");
            });
    });
}

function deleteReport(reportId) {
    $http("../api/ReportBuilder/DeleteReport", { reportId: reportId })
        .then(function (ret) {
            if (ret) {
                $('#Menu_Search_Refresh').trigger('click');
            }
        });
}

function deleteReportFilter(id) {
    $http("../api/ReportBuilder/DeleteReportFilter", { id: id })
        .then(function (ret) {
            if (ret) {
                createFilterGrid();
            }
        });
}

function deleteReportField(id, reportId) {
    $http("../api/ReportBuilder/DeleteReportField", { id: id })
        .then(function (ret) {
            if (ret) {
                Q.fcall(function () {
                    return load_reporting_report_fields_grid({ reportId: reportId });
                });
            }
        });
}

function moveReportFieldUp(id, sortChange, reportId) {
    $http("../api/ReportBuilder/MoveReportFieldSort", { id: id, sortChange: sortChange })
        .then(function (ret) {
            if (ret) {
                Q.fcall(function () {
                    return load_reporting_report_fields_grid({ reportId: reportId });
                });
            }
        });
}

function updateReportFieldTitle(id, title, reportId) {
    $http("../api/ReportBuilder/UpdateReportFieldTitle", { id: id, title: title })
        .then(function (ret) {
            if (ret) {
                Q.fcall(function () {
                    return load_reporting_report_fields_grid({ reportId: reportId });
                });
            }
        });
}

function updateReportFilter(callback, finalUpdate = false) {
    var params = [];

    $(".filterIdClass").map(function (item) {
        var filter = {};
        filter.Id = this.id.split('-')[1];
        filter.Field_Id = $(`#Filter_Field-${filter.Id}`).val();
        filter.Comparator_Id = $(`#Filter_Comparator-${filter.Id}`).val();
        filter.Value_1 = $(`#Filter_Value_1-${filter.Id}`).val() ?? '';
        filter.Value_2 = $(`#Filter_Value_2-${filter.Id}`).val() ?? '';
        filter.Filter_Order = this.attributes["filter_order"].value;
        filter.Filter_Group = this.value;
        filter.Conditional = $(`#Filter_Conditional-${filter.Id}`).val() ?? '';
        params.push(filter);
    });

    $http("../api/ReportBuilder/UpdateReportFilter", { filterList: params, finalUpdate: finalUpdate })
        .then(function (ret) {
            callback(ret);
        });
}

function generateReport(reportId) {
    Spin.start();
    $http("../api/ReportBuilder/CreateDynamicReport", { reportId: reportId })
        .then(function (ret) {
            var url = ret;
            var filename = url.substring(url.lastIndexOf('/') + 1);
            downloadURI(url, filename);
            Spin.stop();
            return false;
        });
}

function addReportFields(reportId, fieldList) {
    return $http("../api/ReportBuilder/AddReportFields", { reportId: reportId, fieldList: fieldList });
}

