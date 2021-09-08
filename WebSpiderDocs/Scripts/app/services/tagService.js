app.service('tagService', function ($http,$q,jQuery)
{
	var that = this;

	//var TgGn = {};

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
	this.Select = function (config){

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

			$o.val(elm[config.id]).text(elm[config.text]).prop('selected',elm[config.id] == config.value);

			$select.append($o);

			jQuery.data($o.get(0),'source',elm);
		});

		if( !config.value )
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
	this.Text = function(config)
	{

		if( typeof config != 'object') {debugger;}

		if (!jQuery) console.error("GlobalVar.js is depend on jQuery. See mroe information https://jquery.com/");

		var $input =(config.target ? config.target : $("<input>"));

		$input.attr('name',config.id).attr('type','text').attr('id',config.id).val(config.value);

		config.data && applyDataAttr($input,config.data);

		return $input;
	}

	this.CheckBox = function(config)
	{

		if( typeof config != 'object') {debugger;}

		if (!jQuery) console.error("GlobalVar.js is depend on jQuery. See mroe information https://jquery.com/");

		var $input =(config.target ? config.target : $("<input>"));

		$input.attr('name',config.id).attr('type','checkbox').attr('id',config.id);
		$input.addClass('hidden');

		config.data && applyDataAttr($input,config.data);

		return $input;
	}

	this.Date = function(config)
	{

		if( typeof config != 'object') {debugger;}

		if (!jQuery) console.error("GlobalVar.js is depend on jQuery. See mroe information https://jquery.com/");

		var $input =(config.target ? config.target : $("<input>"));

		$input.attr('name',config.id).attr('type','date').attr('id',config.id).val(config.value).attr('placeholder','DD/MM/YYYY');

		if( !config.value )
		{
			$input.val((new Date()).toISOString().slice(0,10));
		}

		config.data && applyDataAttr($input,config.data);

		return $input;
	}

	this.genAttrs = function changeattrs(applyTo, onTypeIds, typeMapAttrs, attrs, comboitems,values)
	{
		values = values || [];

		if( ! onTypeIds ) return;

		if( ! angular.isArray(onTypeIds)) onTypeIds = [onTypeIds];

		$(applyTo).empty();

		var typeids = onTypeIds;
		var valiconf = { rules : {} , messages:{},};

		if(false == typeids)
		{
			return;
		}

		// actual gen logic;
		var map = _.uniqBy(typeMapAttrs.filter( x => x.id_attribute < 10000).map( x => {

			return { id_attribute: x.id_attribute, value:(values.find(v=> v.id == x.id_attribute) ||{}).atbValue }
		}),'id_attribute')

		registry_default_validation(valiconf);

		map.forEach(generate);

		return;

		function findAttr(id)
		{
			return attrs.find(function(a){ return a.id == id}) || {};
		}

		function generateTage(attr, config)
		{
			var $wrap = {}
			;

			switch(attr.id_type)
			{
				case 1: //1	Text Box
					var $element = that.Text(config);

					$wrap  = wrapInContainer(attr.id_type, $element, config.id,attr.name)

				break;

				case 2: //2	Check Box
					 $wrap = $('<div/>');
					 break;
					var $element = that.CheckBox(config);

					$wrap  = wrapInContainer(attr.id_type, $element, config.id,attr.name)
					break;

					case 3: //3	Date

					var $element = that.Date(config);
					$wrap  = wrapInContainer(attr.id_type, $element, config.id,attr.name)

					//  $wrap = $element;

				break;

				case 4:	//4	Combo Box
				case 8:	//8	FixedCombo
				case 10://10 ComboSingleSelect
				case 11://11 FixedComboSingleSelect

					var db = comboitems.filter(function(itm){
						return itm.id_atb == attr.id;
					});

					var $element = that.Select({
						db:db,
						id:'id',
						text:'text',
						data: config.data,
						value:config.value
					});

					$element.attr("name",config.id);

					$wrap  = wrapInContainer(attr.id_type, $element, config.id,attr.name)

				break;

				case 5:
				case 6:
				case 9:
					return;
					break;


			}

			return $wrap;
		}

		function wrapInContainer(id_type , $element,id,name)
		{
			var key2Append = '', $cntinr  = '';

			switch(id_type)
			{
				case 1: //1	Text Box

					var wrapper = `
					<div class="form-field ">
						<label for="{0}" class="form-field__label">{1}</label>
						<div class="js-key">

						</div>
					</div>`.format(id,name );

					$cntinr = angular.element(wrapper);

				break;

				case 2: //2	Check Box

					var wrapper = `
									<div class="form-field form-field--checkbox">
										<label for="{0}" class="form-field__label" style="margin-top: 3.3rem;"><span>{1}</span></label>
										<div class="js-key">

										</div>
									</div>`.format(id,name);

					$cntinr = angular.element(wrapper);

				break;

				case 3: //3	Date
				var wrapper = `
									<div class="form-field">
										<label for="{0}" class="form-field__label">{1}</label>
										<div class="form-field__date">
											<div class="js-key">

											</div>
										</div>
									</div>`.format(id,name);

					$cntinr = angular.element(wrapper);
				break;

				case 4:	//4	Combo Box
				case 8:	//8	FixedCombo
				case 10://10 ComboSingleSelect
				case 11://11 FixedComboSingleSelect

					var wrapper =`
					<div class="form-field ">
						<label for="{0}" class="form-field__label">{1}</label>
						<div class="form-field__select form-field__select--blue js-key">

						</div>
					</div>`.format(id,name );

					$cntinr = angular.element(wrapper);


				break;

				case 5:
				case 6:
				case 9:
					return;
					break;
			}

			$cntinr.find('.js-key').append($element);

			return $cntinr;
		}



		function generate(by)
		{
			//var $tmplt = $('#template #field');
			var $attr_palce = angular.element(applyTo);
			var attr = findAttr(by.id_attribute);

			var id = attr.name.replace(/\s/g,"");

			var config = {
				id : id,
				data:
				{
					'id': attr.id,
					'atbValueForUI': attr.atbValueForUI,
					'id_type':attr.id_type
				},
				// default:by.default,
				value:by.value
			};

			var $element = generateTage( attr, config ) ;

			if( !$element )
			{
				return;
			}

			var $cntinr = $element;

			if( attr.required == 1 )
			{
				registry_validationconf(valiconf,config.id,attr.name,attr.id_type);
			}

			$attr_palce.append($cntinr);
		}


		function deploy_validation(valiconf)
		{
			debugger
			var _conf = $.extend(true,{},valiconf,{
				errorPlacement: function (error, element) {
					error.appendTo(element.closest('[data-role="fieldcontainer"]').find('label'));
				},
				submitHandler: function (form) {

						save_taken_photo().then(function(){
							consolewrite("Done");

							jQuery.mobile.changePage("#done", { transition: "flip" });
						});

					return false;
				}
			});

			if( !Fn.isNil(TKPT_VALIDATOR.settings))
			{
				TKPT_VALIDATOR.settings = $.extend(true,{},TKPT_VALIDATOR.settings,_conf);

				return;
			}
			else
			{
				TKPT_VALIDATOR =$('#take-photo #form1').validate($.extend(true,{},_conf));
			}
		}

		function registry_default_validation(valiconf)
		{
			registry_validationconf(valiconf , 'folders' , 'Folder' , 4 );
			registry_validationconf(valiconf , 'types' , 'Document Type' , 4 );
			registry_validationconf(valiconf , 'path' , '' , -1 );
		}

		function registry_validationconf(conf,id,name,type)
		{

			var msg ='Please Fill this field.';

			conf.rules[id] = {};
			conf.rules[id].required = true;
			conf.messages[id] = {};

			switch(type)
			{
				case 1: //1	Text Box
					msg = "Please {0} {1}.".format( "enter" , name);
				break;

				case 4: //1	Text Box
					msg = "Please {0} {1}.".format( "enter" , name);
				break;

				case -1: //1	Text Box
					msg = "Please Take a photo.";
				break;

				default:
					msg = "Please {0} {1}.".format( "select" , name);
				break;
			}

			conf.messages[id].required = msg;
		}
	}

	this.parseAttributeCriterias = function parseAttributeCriterias(target)
	{
		function onoff2bit(val)
		{
			return val === '1' ? 1:0;
		}

		var values = angular.element(target).find('input, select option:selected') //angular.element(`${target} input, ${target} select option:selected`)

			.toArray()

			.map( x =>
				{

					var $top =  angular.element(x);

					if($top.get(0).nodeName.toLowerCase() == 'option')
					{
						$top = $top.parent();
					}

					var id = $top.attr('data-id'),name = $top.attr('name'),id_type = $top.attr('data-id_type'), val = angular.element(x).val(), atbValueForUI =  angular.element(x).text();

					return {
								id: parseInt(id),

								name: name,

								id_type: parseInt(id_type),

								atbValueForUI: atbValueForUI,

								atbValue: ['2'].includes(id_type) ? onoff2bit(val) : val,

								atbValue_str: ['2'].includes(id_type) ? onoff2bit(val) : val
					}
				});

		return values;
		//return values.filter( x => x.Values.atbValue );
	}



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

});