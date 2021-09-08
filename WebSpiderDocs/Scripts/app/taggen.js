
(function( global, factory){

        factory(global, jQuery);


    })(window != 'undefined' ? window : this, function(global,_$){

        var jQuery = _$;

        var TgGn = {};

		function applyDataAttr($elem, data)
		{
			data = data || {};
			var keys = Object.keys(data);
			for(var key in keys)
			{
				$elem.attr('data-' + keys[key] , data[keys[key]]);
			}

			return $elem;
		}

        /**
         * Generate Select Tag.
         * @param {Object} config -
         *          {Array-Object} db - database
         *          {string} target - to be added
         *          {string} id - the field name of db for value
         *          {string} text - the field name of db for text
         *          {string} default- the value to select as default
		 *          {object} dataAttr - data id/value*
         * @return {jQuery} select jQuery element.
         */
        TgGn.Select = function (config){

            if( typeof config != 'object') {debugger;}

            if (!jQuery) console.error("GlobalVar.js is depend on jQuery. See mroe information https://jquery.com/");

            var $select =(config.target ? config.target : $("<select>")), $option = $("<option>");

            $select.empty();

            config.db.forEach(function(elm){
                var $o = $option.clone();

                if( typeof elm == "string" )
                {
                    var str = elm;
                    elm = {};
                    elm[config.id] = elm[config.text] = str;
                }

                $o.val(elm[config.id]).text(elm[config.text]).prop('selected',elm[config.id] == config.default);

                $select.append($o);

                jQuery.data($o.get(0),'source',elm);
            });

            if( !config.default )
            {
                var $o = $option.clone();
                    $o.val('').text("Please Select").prop('selected',true);

                $select.prepend($o);
            }

			config.data && applyDataAttr($select,config.data);

            return $select;
        }

        /**
         * Generate Select Tag.
         * @param {Object} config -
         *          {Array-Object} db - database
         *          {string} target - to be added
         *          {string} id - the field name of db for value
         *          {string} text - the field name of db for text
         *          {string} default- the value to select as default
         * @return {jQuery} select jQuery element.
         */
        TgGn.Text = function(config)
        {

            if( typeof config != 'object') {debugger;}

            if (!jQuery) console.error("GlobalVar.js is depend on jQuery. See mroe information https://jquery.com/");

            var $input =(config.target ? config.target : $("<input>"));

            $input.attr('name',config.id).attr('type','text').attr('id',config.id).val(config.value);

			config.data && applyDataAttr($input,config.data);

            return $input;
        }

        TgGn.CheckBox = function(config)
        {

            if( typeof config != 'object') {debugger;}

            if (!jQuery) console.error("GlobalVar.js is depend on jQuery. See mroe information https://jquery.com/");

            var $input =(config.target ? config.target : $("<input>"));

            $input.attr('name',config.id).attr('type','checkbox').attr('id',config.id);
            $input.addClass('hidden');

			config.data && applyDataAttr($input,config.data);

            return $input;
        }

        TgGn.Date = function(config)
        {

            if( typeof config != 'object') {debugger;}

            if (!jQuery) console.error("GlobalVar.js is depend on jQuery. See mroe information https://jquery.com/");

            var $input =(config.target ? config.target : $("<input>"));

            $input.attr('name',config.id).attr('type','date').attr('id',config.id).val(config.value).attr('placeholder','DD/MM/YYYY');

            if( !config.default )
            {
                $input.val((new Date()).toISOString().slice(0,10));
			}

			config.data && applyDataAttr($input,config.data);

            return $input;
        }

        global.TgGn = TgGn ;
    });
