export class TabLink {
  nameTab: string;
  link: string;
  isVisible: boolean;

  constructor(nameTab: string, link: string, isVisible: boolean) {
    this.nameTab = nameTab;
    this.link = link;
    this.isVisible = isVisible;
  }
}
