function startClick(){
	var isDone = true;
	$("#clickbtn").append("<p id='counterHolder'></p>")
	$("#clickbtn").unbind("click").off( "click", "**" ).on("click", function(){
		var totalClicks = $("#clicks").val();
		$("#counterHolder").text("Total Clicks: " + totalClicks);
		console.log("Current Total Clicks: " + totalClicks);
		if(totalClicks >= 2000){
			console.log("Skipping click");
			return;	
		}
		
	    $("#clickbtn").prop("disabled", true);
	    $(".spinner-container").show()
	    var z = setTimeout(function(){
	        $("#clickbtn").prop("disabled", false);
	        $(".spinner-container").hide();
	    }, 400);
		var formData = $("#clickform").serialize();
	    $.post("/site/receive-click", formData)
	    .done(function(data) {
	        if(data.result == "failed") {
	            $("#clickbtn").attr("disabled", "disabled");
	            swal(
	              data.error,
	              data.message,
	              'error'
	            );
	            clearTimeout(z);
	            $(".spinner-container").hide();
	            remaining = data.remainingtime;
	        } else if(data.result == "finished") {
	            $("#clickbtn").prop("disabled", true);
	            clicks = data.clicks;
	            remaining = data.remainingtime;
	            reward = data.reward;
	            swal({
	                title: 'Congratulations!',
	                type: 'success',
	                html: data.message,
	            });
	            clearTimeout(z);
	            $(".spinner-container").hide();
	        } else {
	            clicks = data.clicks;
	            remaining = data.remainingtime;
	            reward = data.reward;
	        }
			isDone = true;
	        progress = (clicks / 2000) * 100;
	        $(".progress .progress-bar").css({width: progress +"%"});
	        $("#clicks").val(clicks);
	        if(clicks >= limit) {
	           clearTimeout(z);
	           $("#clickbtn").prop("disabled", true);
	           $(".spinner-container").hide();
	           return false;
	        }
			$("#remainingtime").text(secondsToHms(remaining));
	        console.log("Total Clicks: " + clicks + " | Total Progress: " + progress);
	    })
		.fail(function(data){
			console.log("Failed Service: " + JSON.stringify(data));
			isDone = true;
		});
	});
	
	var interval = setInterval(function(){
		if(isDone === false)
			return;
		
		isDone = false;
		simulate(document.getElementById("clickbtn"), "click");
	}, 500);
};

function simulate(element, eventName)
{
    var options = extend(defaultOptions, arguments[2] || {});
    var oEvent, eventType = null;

    for (var name in eventMatchers)
    {
        if (eventMatchers[name].test(eventName)) { eventType = name; break; }
    }

    if (!eventType)
        throw new SyntaxError('Only HTMLEvents and MouseEvents interfaces are supported');

    if (document.createEvent)
    {
        oEvent = document.createEvent(eventType);
        if (eventType == 'HTMLEvents')
        {
            oEvent.initEvent(eventName, options.bubbles, options.cancelable);
        }
        else
        {
            oEvent.initMouseEvent(eventName, options.bubbles, options.cancelable, document.defaultView,
            options.button, options.pointerX, options.pointerY, options.pointerX, options.pointerY,
            options.ctrlKey, options.altKey, options.shiftKey, options.metaKey, options.button, element);
        }
        element.dispatchEvent(oEvent);
    }
    else
    {
        options.clientX = options.pointerX;
        options.clientY = options.pointerY;
        var evt = document.createEventObject();
        oEvent = extend(evt, options);
        element.fireEvent('on' + eventName, oEvent);
    }
    return element;
}

function extend(destination, source) {
    for (var property in source)
      destination[property] = source[property];
    return destination;
}

var eventMatchers = {
    'HTMLEvents': /^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|scroll)$/,
    'MouseEvents': /^(?:click|dblclick|mouse(?:down|up|over|move|out))$/
}
var defaultOptions = {
    pointerX: 0,
    pointerY: 0,
    button: 0,
    ctrlKey: false,
    altKey: false,
    shiftKey: false,
    metaKey: false,
    bubbles: true,
    cancelable: true
}
