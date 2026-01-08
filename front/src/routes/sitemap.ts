import { HTMLAttributeAnchorTarget } from 'react';
import { SxProps } from '@mui/material';
import paths, { rootPaths } from './paths';

export interface SubMenuItem {
  name: string;
  pathName: string;
  key?: string;
  selectionPrefix?: string;
  path?: string;
  target?: HTMLAttributeAnchorTarget;
  active?: boolean;
  icon?: string;
  iconSx?: SxProps;
  items?: SubMenuItem[];
}

export interface MenuItem {
  id: string;
  key?: string; // used for the locale
  subheader?: string;
  icon: string;
  target?: HTMLAttributeAnchorTarget;
  iconSx?: SxProps;
  items: SubMenuItem[];
}

const sitemap: MenuItem[] = [
  {
    id: 'pages',
    icon: 'material-symbols:view-quilt-outline',
    items: [
      {
        name: 'Livros',
        path: rootPaths.root,
        pathName: 'livros',
        active: true,
      },
      {
        name: 'Autores',
        path: paths.autores,
        pathName: 'autores',
        active: true,
      },
      {
        name: 'Assuntos',
        path: paths.assuntos,
        pathName: 'assuntos',
        active: true,
      }
    ],
  },
];

export default sitemap;
