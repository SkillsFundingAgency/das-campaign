export class AnchorLinks {
  constructor(root = document) {
    this.root = root;
    this.onClick = this.onClick.bind(this);
  }

  init() {

    this.root.addEventListener('click', this.onClick);
  }

  onClick(event) {
    if (event.defaultPrevented) return;
    
    if (event.button !== 0 || event.metaKey || event.ctrlKey || event.shiftKey || event.altKey) return;

    const link = event.target.closest('a[href]');
    if (!link || link.hasAttribute('download')) return;

    const target = link.getAttribute('target');
    if (target && target !== '_self') return;

    const href = link.getAttribute('href');
    if (!href || href.charAt(0) !== '#') return;

    const destination = this.findDestination(href);
    if (!destination) return;

    event.preventDefault();
    this.jumpTo(destination);
    this.replaceUrl(href);
  }
  
  findDestination(href) {
    const fragment = href.slice(1);
    if (fragment === '' || fragment === 'top') return 'top';
    let id = fragment;
    try {
      id = decodeURIComponent(fragment);
    } catch {
     console.error(`Unable to find section with ID ${href}`); 
    }
    return document.getElementById(id) || document.getElementsByName(id)[0] || null;
  }

  jumpTo(destination) {
    if (destination === 'top') {
      window.scrollTo({top: 0, left: 0});
      return;
    }
    if (!destination.hasAttribute('tabindex')) {
      destination.setAttribute('tabindex', '-1');
    }
    destination.focus({preventScroll: true});
    destination.scrollIntoView();
  }

  replaceUrl(href) {
    if (!window.history || !window.history.replaceState) return;
    window.history.replaceState(window.history.state, '', href);
  }
}
