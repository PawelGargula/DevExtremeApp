const currentLinkItem = document.querySelector(`a[href$="${location.pathname}${location.search}"]`);

currentLinkItem.classList.add('active');