export interface RejectedFille extends File {
  lastModified: number;
  lastModifiedDate: Date;
  name: string;
  reason: 'type' | 'size' | 'no_multiple';
  size: number;
  type: string;
  webkitRelativePath: string;
}
