var addClass = function (elementsList, cssClass) {
    for (var i = 0; i < elementsList.length; ++i) {
        elementsList[i].classList.add(cssClass);
    }
};
var firstLevels = document.querySelectorAll('[role="first-level"]');
addClass(firstLevels, 'blue');
var secondLevels = document.querySelectorAll('[role="second-level"]');
addClass(secondLevels, 'red');
var _3thLevels = document.querySelectorAll('[role="_3th-level"]');
addClass(_3thLevels, 'lime');
