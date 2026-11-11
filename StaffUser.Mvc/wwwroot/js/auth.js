// TimeZone auth helpers: show/hide password + password strength meter
(function () {
    document.querySelectorAll('.vx-pw-toggle').forEach(function (btn) {
        btn.addEventListener('click', function () {
            var input = document.getElementById(btn.dataset.target);
            if (!input) return;
            var show = input.type === 'password';
            input.type = show ? 'text' : 'password';
            var icon = btn.querySelector('i');
            if (icon) icon.className = show ? 'bi bi-eye-slash' : 'bi bi-eye';
        });
    });

    var names = ['', 'Weak', 'Fair', 'Good', 'Strong'];
    document.querySelectorAll('.vx-strength').forEach(function (meter) {
        var input = document.getElementById(meter.dataset.for);
        if (!input) return;
        var label = meter.querySelector('small');
        input.addEventListener('input', function () {
            var v = input.value;
            if (!v) { meter.classList.remove('on'); return; }
            var score = 0;
            if (v.length >= 8) score++;
            if (/[A-Za-z]/.test(v) && /\d/.test(v)) score++;
            if (/[^A-Za-z0-9]/.test(v)) score++;
            if (v.length >= 12) score++;
            score = score || 1;
            meter.dataset.score = score;
            label.textContent = names[score];
            meter.classList.add('on');
        });
    });
})();
