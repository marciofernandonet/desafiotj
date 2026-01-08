import { Link, SvgIconProps, Typography, typographyClasses } from '@mui/material';
import { rootPaths } from 'routes/paths';

interface LogoProps extends SvgIconProps {
  showName?: boolean;
}

const Logo = ({ showName = true }: LogoProps) => {
  return (
    <Link
      href={rootPaths.root}
      underline="none"
      sx={{
        display: 'flex',
        alignItems: 'center',
        '&:hover': {
          [`& .${typographyClasses.root}`]: {
            backgroundPosition: ({ direction }) => (direction === 'rtl' ? 'right' : 'left'),
          },
        },
      }}
    >
      {showName && (
        <Typography
          sx={{
            color: 'text.secondary',
            fontWeight: 'medium',
            fontSize: 29.5,
            lineHeight: 1,
            margin: 1,
            marginLeft: 0.625,
            
          }}
        >
          Livraria
        </Typography>
      )}
    </Link>
  );
};

export default Logo;
