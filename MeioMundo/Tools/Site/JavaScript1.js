var script = document.createElement('script');
script.innerHTML = 'var el = document.createElement(\"div\");el.innerHTML=\"My injected content\";document.body.appendChild(el);';
document.body.appendChild(script);