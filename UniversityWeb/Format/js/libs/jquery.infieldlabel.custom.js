/*
File has UA customizations for handling input context menu, event timing and event binding for simplified profiling/debugging
additional customizations added to hide label on script made changes to input values

Do not replace with stock updates.  UA customizations must be merged.
*/
/*
	* jquery.infieldlabel
	* A simple jQuery plugin for adding labels that sit over a form field and fade away when the fields are populated.
	* 
	* Copyright (c) 2009 - 2013 Doug Neiner <doug@dougneiner.com> (http://code.dougneiner.com)
	* Source: https://github.com/dcneiner/In-Field-Labels-jQuery-Plugin
	* Dual licensed MIT or GPL
	*   MIT (http://www.opensource.org/licenses/mit-license)
	*   GPL (http://www.opensource.org/licenses/gpl-license)
	*
	* @version 0.1.3
	*/
(function ($) {

	$.InFieldLabels = function (label, field, options) {
		// To avoid scope issues, use 'base' instead of 'this'
		// to reference this class from internal events and functions.
		var base = this;

		// Access to jQuery and DOM versions of each element
		base.$label = $(label);
		base.label = label;

		base.$field = $(field);
		base.field = field;

		base.$label.data("InFieldLabels", base);
		base.showing = true;

		function suppressDefault(e) {
			e.preventDefault();
			return false;
		}

		function resetLabelVisibility() {
			if (base.$field.val() !== "") {
				base.hideLabel();
			} else {
				base.showLabel();
			}
		}

		function labelMousedown(e) {
			if (e.which == 3) {
				base.hideLabel();
				base.$field.focus();
				return false;
			}
		}

		function fieldMouseup(e) {
			if (e.which == 3) {
				setTimeout(resetLabelVisibility, 8);
			}
		}

		base.init = function () {
			var initialSet;

			// Merge supplied options with default options
			base.options = $.extend({}, $.InFieldLabels.defaultOptions, options);

			// Check if the field is already filled in 
			// add a short delay to handle autocomplete
			setTimeout(resetLabelVisibility, 100);

			// suppress the contextmenu from labels?
			//base.$label.bind("contextmenu", suppressDefault);

			// Detect right click on label:
			// hide label on mousedown so mouseup will apply to field underneath.
			base.$label.mousedown(labelMousedown);

			base.$field.bind({
				"focus": base.fadeOnFocus,
				"blur": base.fieldBlur,
				"keydown.infieldlabel": base.hideOnChange,
				"paste": function () {
					// Since you can not paste an empty string we can assume
					// that the fieldis not empty and the label can be cleared.
					base.setOpacity(0.0);
				},
				"change": base.checkForEmpty,
				"onPropertyChange": base.checkForEmpty,
				"keyup.infieldlabel": base.checkForEmpty,
				"mouseup": fieldMouseup
			});

			if (base.options.pollDuration > 0) {
				initialSet = setInterval(function () {
					if (base.$field.val() !== "") {
						base.hideLabel();
						clearInterval(initialSet);
					}
				}, base.options.pollDuration);
			}
		};

		// If the label is currently showing
		// then fade it down to the amount
		// specified in the settings
		base.fadeOnFocus = function () {
			if (base.showing) {
				base.setOpacity(base.options.fadeOpacity);
			}
		};

		base.setOpacity = function (opacity) {
			hideCallback = null;
			if (!(opacity > 0.0)) {
				hideCallback = base.hideLabel;
			}
			base.$label.stop().animate({ opacity: opacity }, base.options.fadeDuration, "swing", hideCallback);
			//base.showing = (opacity > 0.0);
		};

		// Checks for empty as a fail safe
		// set blur to true when passing from
		// the blur event
		base.fieldBlur = function (event) {
			base.checkForEmpty(event, true);
		};

		base.checkForEmpty = function (event, blur) {
			if (base.$field.val() === "") {
				base.prepForShow();
				base.setOpacity(blur ? 1.0 : base.options.fadeOpacity);
			} else {
				base.setOpacity(0.0);
			}
		};

		base.hideLabel = function () {
			//base.$label.hide();
			base.$label.addClass(base.options.hiddenClass);
			base.showing = false;
		};

		base.showLabel = function () {
			//base.$label.show();
			base.$label;
			base.showing = true;
		};

		base.prepForShow = function () {
			if (!base.showing) {
				// Prepare for a animate in...
				//base.$label.css({ opacity: 0.0 }).show();
				base.$label.css({ opacity: 0.0 }).removeClass(base.options.hiddenClass);

				// Reattach the keydown event
				base.$field.bind('keydown.infieldlabel', function (e) {
					base.hideOnChange(e);
				});
			}
		};

		base.hideOnChange = function (e) {
			if (
				(e.keyCode === 16) || // Skip Shift
				(e.keyCode === 9) // Skip Tab
			) {
				return;
			}

			if (base.showing) {
				base.hideLabel();
			}

			// Remove keydown event to save on CPU processing
			base.$field.unbind('keydown.infieldlabel');
		};

		// Run the initialization method
		base.init();
	};

	$.InFieldLabels.defaultOptions = {
		fadeOpacity: 0.5, // Once a field has focus, how transparent should the label be
		fadeDuration: 300, // How long should it take to animate from 1.0 opacity to the fadeOpacity
		pollDuration: 0, // If set to a number greater than zero, this will poll until content is detected in a field
		hiddenClass: "visuallyhidden", // class name to apply to labels when hidden
		enabledInputTypes: ["text", "search", "tel", "url", "email", "password", "number", "textarea", "select"]
	};


	$.fn.inFieldLabels = function (options) {
		var allowed_types = options && options.enabledInputTypes || $.InFieldLabels.defaultOptions.enabledInputTypes;

		return this.each(function () {
			// Find input or textarea based on for= attribute
			// The for attribute on the label must contain the ID
			// of the input or textarea element
			var for_attr = $(this).attr('for'), field, restrict_type;
			if (!for_attr) {
				return; // Nothing to attach, since the for field wasn't used
			}

			// Find the referenced input or textarea element
			field = document.getElementById(for_attr);
			if (!field) {
				return; // No element found
			}

			// Restrict input type
			restrict_type = $.inArray(field.type, allowed_types);

			if (restrict_type === -1 && field.nodeName !== "TEXTAREA" && field.nodeName !== "SELECT") {
				return; // Again, nothing to attach
			}

			// Only create object for matched input types and textarea
			(new $.InFieldLabels(this, field, options));
		});
	};

}(jQuery));
