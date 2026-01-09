import { RefObject, SyntheticEvent, useEffect, useMemo, useState } from 'react';
import Box from '@mui/material/Box';
import {
  DataGrid,
  GRID_CHECKBOX_SELECTION_COL_DEF,
  GridColDef
} from '@mui/x-data-grid';
import { GridApiCommunity } from '@mui/x-data-grid/internals';
import DashboardMenu from 'components/common/DashboardMenu';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import { IconifyIcon } from 'components/base/IconifyIcon';
import AccountDialog from '../account/common/AccountDialog';
import { SnackbarCloseReason, TextField } from '@mui/material';
import api from "../../../service/api";
import ProSnackbar from 'layouts/main-layout/common/ProSnackbar';

interface UsersTableProps {
  apiRef: RefObject<GridApiCommunity | null>;
  filterButtonEl?: HTMLButtonElement | null;
};

type Author = {
  id: number;
  nome: string;
};

const AuthorsTable = ({ apiRef }: UsersTableProps) => {
  const [authors, setAuthors] = useState<Author[]>([]);
  const [id, setId] = useState(0);
  const [name, setName] = useState("");
  const [submitted, setSubmitted] = useState(false);
  const [index, setIndex] = useState(-1);
  const [open, setOpen] = useState(false);
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [error, setError] = useState(false);
  const [openUpdate, setOpenUpdate] = useState(false);
  const [openDelete, setOpenDelete] = useState(false);

  useEffect(() => {
    (async () => {
      const response = await api.get("author");
      setAuthors(response.data.data);
    })();
  }, []);

  
  const handleOpenUpdate = () => setOpenUpdate(!openUpdate);
  const handleOpenDelete = () => setOpenDelete(!openDelete);

  function handleOpen() {
    setSubmitted(false);
    setName("");
    setOpen(!open);
  }

  async function handleModalUpdate(index: number, id: number) {
    try{
      setIndex(index);
      setId(id);
      setOpenUpdate(true);
      const response = await api.get(`author/${id}`);
      if(response.status === 200){
        const { nome } = response.data.data;
        setName(nome);
      }
    }
    catch(error) {
      console.log(error)
    }
  }

  function handleModalDelete(index: number, id: number) {
    setIndex(index);
    setId(id);
    setOpenDelete(true);
  }

  async function saveAuthor() {
    try {
      setSubmitted(true);

      if (!name) {
        return;
      }

      const response = await api.post("author", {
        nome: name
      });
      const newAuthor = response.data.data;
      setAuthors(prev => [...prev, newAuthor]);
      setError(false);
      setOpen(false);
    }
    catch(error) {
      setError(true);
      console.log(error);
    }
    finally {
      if(name) setOpenSnackbar(true);
    }
  }

  async function updateAuthor() {
    try {
      setSubmitted(true);

      if (!name) {
        return;
      }
      
      const response = await api.put("author", {
        id,
        nome: name,
      });
      const updateAuthor = response.data.data;
      setAuthors(prev => [
        ...prev.slice(0, index),
        updateAuthor,
        ...prev.slice(index + 1),
      ]);
      setOpenUpdate(false);
    }
    catch(error) {
      console.log(error);
    }
  }

  async function deleteAuthor() {
    try {  
      const response = await api.delete(`author/${id}`);
      if(response.status === 200){
        setAuthors(prev => [
          ...prev.slice(0, index),
          ...prev.slice(index + 1),
        ]);
      }
    }
    catch(error) {
      console.log(error);
    }
    finally {
      setOpenDelete(false);
    }
  }

  const handleClose = (_event: SyntheticEvent, reason?: SnackbarCloseReason) => {
    if (reason === 'clickaway') return;
    setOpenSnackbar(false);
  };

  async function handleGenerateReport() {
    const response = await fetch("http://localhost:5237/api/report/author");
    const blob = await response.blob();

    const url = window.URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = "relatorio.pdf";
    a.click();

    window.URL.revokeObjectURL(url);
  }

  const columns: GridColDef<Author>[] = useMemo(
    () => [
      {
        ...GRID_CHECKBOX_SELECTION_COL_DEF,
        width: 64,
      },
      {
        field: 'nome',
        headerName: 'Nome',
        minWidth: 160,
        sortable: false,
        filterable: false,
        flex: 1,
      },
      {
        field: 'action',
        headerName: '',
        filterable: false,
        sortable: false,
        width: 60,
        align: 'right',
        headerAlign: 'right',
        renderCell: (params) => {
          const index = params.api.getRowIndexRelativeToVisibleRows(params.id);
          return (
            <DashboardMenu
              menuItems={[
                {
                  label: 'Editar',
                  onClick: () => handleModalUpdate(index, params.row.id)
                },
                {
                  label: 'Remover',
                  sx: { color: 'error.main' },
                  onClick: () => handleModalDelete(index, params.row.id)
                },
              ]}
            />
          )
        },
      },
    ],
    [],
  );

  return (
    <Box sx={{ width: 1 }}>
      <Stack
        sx={{
          columnGap: 1,
          rowGap: 2,
          justifyContent: 'space-between',
          alignItems: { xl: 'center' },
          flexWrap: { xs: 'wrap', md: 'nowrap' },
          mb: 4
        }}
      >
        <Button
          variant="contained"
          color="primary"
          startIcon={<IconifyIcon icon="material-symbols:add-rounded" />}
          sx={{ flexShrink: 0 }}
          onClick={handleOpen}
        >
          Autor
        </Button>
        <Button
          variant="contained"
          color="neutral"
          sx={{ flexShrink: 0 }}
          onClick={handleGenerateReport}
        >
          Relatório
        </Button>
      </Stack>
      <DataGrid
        rowHeight={64}
        rows={authors}
        apiRef={apiRef}
        columns={columns}
        hideFooter
      />

      <AccountDialog
        title="Novo autor"
        open={open}
        handleConfirm={saveAuthor}
        handleDialogClose={handleOpen}
        handleDiscard={handleOpen}
        sx={{ minWidth: 400, maxWidth: 600 }}
      >
        <Stack direction="column" spacing={1} p={0.125}>
          <TextField
            placeholder="Nome"
            label="Nome"
            required
            error={submitted && !name}
            helperText={submitted && !name ? "Campo obrigatório" : ""}
            onChange={e => setName(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 40,
              },
            }}
            fullWidth
          />
        </Stack>
      </AccountDialog>
      
      <AccountDialog
        title="Editar autor"
        open={openUpdate}
        handleConfirm={updateAuthor}
        handleDialogClose={handleOpenUpdate}
        handleDiscard={handleOpenUpdate}
        sx={{ minWidth: 400, maxWidth: 600 }}
      >
        <Stack direction="column" spacing={1} p={0.125}>
          <TextField
            placeholder="Nome"
            label="Nome"
            required
            error={submitted && !name}
            helperText={submitted && !name ? "Campo obrigatório" : ""}
            value={name}
            onChange={e => setName(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 40,
              },
            }}
            fullWidth
          />
        </Stack>
      </AccountDialog>
      
      <AccountDialog
        title="Remover autor"
        subtitle="Deseja confirmar a remoção do autor?"
        open={openDelete}
        handleConfirm={deleteAuthor}
        handleDialogClose={handleOpenDelete}
        handleDiscard={handleOpenDelete}
        typeDelete
        sx={{ minWidth: 400, maxWidth: 600 }}
      />
      <ProSnackbar open={openSnackbar} onClose={handleClose} error={error} />
    </Box>
  );
};

export default AuthorsTable;
