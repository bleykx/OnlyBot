﻿function SwitchTheme() {
    var element = document.body;
    element.dataset.bsTheme = element.dataset.bsTheme === 'dark' ? 'light' : 'dark';
}